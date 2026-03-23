using System.Collections.Generic;
using R3;
using UnityEngine;
using DrawTower.Token;
using System;
using Cysharp.Threading.Tasks;
using UnityEngine.EventSystems;

namespace DrawTower.Logic
{
    public class DrawLine : AsyncTokenMonoBehaviour, IDrawLine
    {
        [SerializeField] 
        private Material _crayonMaterial;
        
        private LineRenderer _lr;
        private List<Vector3> _points = new();
        private float _lineWidth = 3f;
        private Color _lineColor;
        private RectTransform _targetArea;

        private readonly CompositeDisposable _disposables = new();
        private IDisposable _inputSubscription;
        
        private UniTaskCompletionSource<IReadOnlyList<Vector3>> _drawCompletionSource;
        
        private bool _isDrawing = false;
		private bool _isPointerOverUi = false;
		private readonly List<RaycastResult> _uiRaycastResults = new();
        public bool IsDrawing() => _isDrawing && !_isPointerOverUi;

        /// <summary>
        /// 描画に必要な設定をする
        /// </summary>
        public void Setup(RectTransform area, Color color)
        {
            _targetArea = area;
            _lineColor = color;
        }
        
        /// <summary>
        /// 描画完了まで非同期で待機（3点未満なら再試行）
        /// </summary>
        public async UniTask<IReadOnlyList<Vector3>> StartDrawingAsync()
        {
            StopDrawing();

            while (true)
            {
                _drawCompletionSource = new UniTaskCompletionSource<IReadOnlyList<Vector3>>();

                StartDrawing();

                // ユーザーが描き終わるまで待機
                var result = await _drawCompletionSource.Task;

                // 条件を満たさない場合は再試行
                if (IsSimpleLine(result))
                {
                    Debug.Log("線っぽい描画のため再描画待ち...");
                    
					if (_lr != null)
					{
						Destroy(_lr.gameObject);
						_lr = null;
					}
                    continue;
                }

                return result;
            }
        }
        
        /// <summary>
		/// 現在の描画を強制停止する
		/// </summary>
        public void Cancel()
		{
			StopDrawing();

			_isDrawing = false;

			if (_drawCompletionSource != null && !_drawCompletionSource.Task.Status.IsCompleted())
			{
				_drawCompletionSource.TrySetCanceled();
			}

			if (_lr != null)
			{
				Destroy(_lr.gameObject);
				_lr = null;
			}
		}
		
		/// <summary>
        /// 受信した線データを UI に再描画する（同期用）
        /// </summary>
        public void DrawReceivedLine(IReadOnlyList<Vector3> points)
        {
            if (_targetArea == null || points == null || points.Count == 0)
                return;
                
            if (_lr != null)
			{
				Destroy(_lr.gameObject);
				_lr = null;
			}
			
            _lr = CreateLineRenderer("ReceivedLineUI");         
            _lr.positionCount = points.Count;
            
            for (int i = 0; i < points.Count; i++)
            {
                _lr.SetPosition(i, points[i]);
            }
        }

        /// <summary>
        /// 入力監視を有効にする
        /// </summary>
        private void StartDrawing()
        {
            // すでに購読中ならスキップ
            if (_inputSubscription != null)
                return;

            _inputSubscription = Observable.EveryUpdate()
                .Subscribe(_ =>
                {
                    if (Input.touchCount > 0)
                    {
                        var touch = Input.GetTouch(0);
                        
                        if (touch.phase == TouchPhase.Began)
                        {
                            _isPointerOverUi = IsPointerOverUi(touch.position);
                            if (_isPointerOverUi)
                                return;
                            
                            StartDraw(_targetArea, touch.position);
                            return;
                        }                           
                        
                        if (!_isDrawing)
                            return;
                        
                        if (touch.phase == TouchPhase.Moved)
                        {
                            _isPointerOverUi = IsPointerOverUi(touch.position);
                            if (_isPointerOverUi)
                                return;
                            
                            ContinueDraw(touch.position);
                        }                           
                        else if (touch.phase == TouchPhase.Ended)
                        {
                            _isPointerOverUi = false;
                            EndDraw();
                        }                           
                    }
                })
                .AddTo(_disposables);
        }

        /// <summary>
        /// 入力監視を停止する
        /// </summary>
        private void StopDrawing()
        {
            _inputSubscription?.Dispose();
            _inputSubscription = null;
        }

        private void StartDraw(RectTransform area, Vector2 screenPos)
        {
            if (area == null || !IsInsideDrawArea(area, screenPos))
                return;
				
			if (_lr != null)
			{
				Destroy(_lr.gameObject);
				_lr = null;
			}

            _isDrawing = true;
            _points.Clear();
            _points.Capacity = Mathf.Max(_points.Capacity, 128);

            _lr = CreateLineRenderer("DrawnLineUI");
        }
        
        /// <summary>
        /// 共通の LineRenderer 作成メソッド（StartDraw / Receive 用）
        /// </summary>
        private LineRenderer CreateLineRenderer(string name)
        {
            var go = new GameObject(name);
            go.transform.SetParent(_targetArea, false);
            go.transform.localPosition = new Vector3(0, 0, -0.1f);

            var lr = go.AddComponent<LineRenderer>();
            lr.sharedMaterial = _crayonMaterial;
            lr.material.color = _lineColor;
            lr.startWidth = _lineWidth;
            lr.endWidth = _lineWidth;

            lr.positionCount = 0;
            lr.numCapVertices = 2;
            lr.numCornerVertices = 2;
            lr.useWorldSpace = false;
            lr.textureMode = LineTextureMode.Tile;

            return lr;
        }


        private void ContinueDraw(Vector2 screenPos)
        {
            if (!_isDrawing || _targetArea == null) return;
            if (!IsInsideDrawArea(_targetArea, screenPos)) return;

            if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    _targetArea, screenPos, Camera.main, out var localPos))
            {
                _points.Add(localPos);
                _lr.positionCount = _points.Count;
                _lr.SetPosition(_points.Count - 1, localPos);
            }
        }

        private void EndDraw()
        {
            if (!_isDrawing) return;
            _isDrawing = false;

            StopDrawing();
            _drawCompletionSource?.TrySetResult(_points);
        }

        private bool IsInsideDrawArea(RectTransform area, Vector2 screenPos)
        {
            return RectTransformUtility.RectangleContainsScreenPoint(area, screenPos, Camera.main);
        }
        
        /// <summary>
		/// 描いた線が単なる線かどうか判定する（閉じてない or 曲がってない）
		/// </summary>
		private bool IsSimpleLine(IReadOnlyList<Vector3> points)
		{
			if (points.Count < 5)
				return true;

			float totalLength = 0f;
			for (int i = 1; i < points.Count; i++)
				totalLength += Vector3.Distance(points[i - 1], points[i]);

			float startToEnd = Vector3.Distance(points[0], points[^1]);
			float closureRatio = startToEnd / totalLength;

			// 始点と終点が十分近い
			bool isClosedEnough = closureRatio < 0.05f;

			// 振れ幅を見る
			float minY = float.MaxValue;
			float maxY = float.MinValue;
			float minX = float.MaxValue;
			float maxX = float.MinValue;

			foreach (var p in points)
			{
				if (p.y < minY) minY = p.y;
				if (p.y > maxY) maxY = p.y;
				if (p.x < minX) minX = p.x;
				if (p.x > maxX) maxX = p.x;
			}

			float heightRange = maxY - minY;
			float widthRange = maxX - minX;

			// 縦にも横にもあまり動いていない場合は「線」とみなす
			bool isFlatHorizontally = heightRange < (widthRange * 0.1f);
			bool isFlatVertically = widthRange < (heightRange * 0.1f);

			if (isClosedEnough && !isFlatHorizontally && !isFlatVertically)
				return false; // 図形

			return true; // 線
		}
		
		private bool IsPointerOverUi(Vector2 screenPos)
        {
            var eventSystem = EventSystem.current;
            if (eventSystem == null) return false;

			_uiRaycastResults.Clear();
			var eventData = new PointerEventData(eventSystem)
			{
				position = screenPos
			};
			eventSystem.RaycastAll(eventData, _uiRaycastResults);
			return _uiRaycastResults.Count > 0;
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            Cancel();
            _disposables.Dispose();
        }
    }
}
