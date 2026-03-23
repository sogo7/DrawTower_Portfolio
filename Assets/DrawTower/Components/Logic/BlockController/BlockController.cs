using System.Threading;
using Cysharp.Threading.Tasks;
using R3;
using DrawTower.Token;
using UnityEngine;
using System;
using UnityEngine.EventSystems;
using DG.Tweening;

namespace DrawTower.Logic
{
    public class BlockController : AsyncTokenMonoBehaviour, IBlockController
    {
        private GameObject _newBlock;
        private int _fingerId = -1;
        private float _sensitivity = 10f;
        private float _fallThreshold = -10f; 
        private readonly CompositeDisposable _disposables = new();
        private IDisposable _inputSubscription;
        private Tween _tween;
        private bool _isRotateMoving = false;
        private UniTaskCompletionSource _tcs;
        
        private readonly Subject<Unit> _onBlockLanded = new();
        public Observable<Unit> OnBlockLanded() => _onBlockLanded;

        /// <summary>
        /// 入力監視を有効にし、完了（落下静止）まで待機できる
        /// </summary>
        public async UniTask StartControllerAsync(GameObject block, CancellationToken ct)
        {
            _tcs = new UniTaskCompletionSource();
            _newBlock = block;

            _inputSubscription = Observable.EveryUpdate()
                .Subscribe(_ => HandleDragAsync(ct).Forget())
                .AddTo(_disposables);

            // 完了を待機
            await _tcs.Task.AttachExternalCancellation(ct);
        }
        
        /// <summary>
        /// 入力監視を停止する
        /// </summary>
        public void StopController()
        {
            _inputSubscription?.Dispose();
            _inputSubscription = null;
        }

        private async UniTask HandleDragAsync(CancellationToken ct)
        {
            if (Input.touchCount == 0)
            {
                return;
            }

            if (_fingerId == -1)
            {
                for (int i = 0; i < Input.touchCount; i++)
                {
                    var t = Input.GetTouch(i);
                    if (t.phase == TouchPhase.Began && !IsPointerOverUi(t.fingerId))
                    {
                        _fingerId = t.fingerId;
                        break;
                    }
                }
            }

            if (_fingerId == -1 || !TryGetTouch(_fingerId, out var touch))
            {
                return;
            }

            switch (touch.phase)
            {
                case TouchPhase.Moved:
                    if (IsPointerOverUi(touch.fingerId)) return;
                    if (_newBlock != null)
                    {
                        Vector2 delta = touch.deltaPosition * _sensitivity * 0.001f;
                        _newBlock.transform.position += new Vector3(delta.x, 0f, 0f);
                    }
                    break;

                case TouchPhase.Ended:
                case TouchPhase.Canceled:
                    if (IsPointerOverUi(touch.fingerId)) return;
                    _fingerId = -1;
                    StopController();
                    DropBlockAsync(_newBlock, ct).Forget();
                    _newBlock = null;
                    break;
            }
        }

        private static bool TryGetTouch(int fingerId, out Touch touch)
        {
            foreach (var t in Input.touches)
            {
                if (t.fingerId == fingerId)
                {
                    touch = t;
                    return true;
                }
            }
            touch = default;
            return false;
        }

        private static bool IsPointerOverUi(int pointerId)
        {
            var eventSystem = EventSystem.current;
            if (eventSystem == null) return false;
            return eventSystem.IsPointerOverGameObject(pointerId);
        }
        
        public async UniTask RotateSnapAsync(CancellationToken ct)
        {
            if (_newBlock == null || _isRotateMoving) return;
            _tween?.Kill();
            _isRotateMoving = true;
            
            _tween = _newBlock.transform
                .DORotate(new Vector3(0f, 0f, -45f), 0.3f, RotateMode.WorldAxisAdd)
                .SetEase(Ease.OutCubic);
                
            await _tween.ToUniTask(TweenCancelBehaviour.KillAndCancelAwait, ct);
			
			_isRotateMoving = false;
        }
        
        private async UniTask DropBlockAsync(GameObject block, CancellationToken ct)
        {
            if (block == null) return;

            var rb = block.GetComponent<Rigidbody>();
            rb.isKinematic = false;
            rb.useGravity = true;

            var fallThreshold = _fallThreshold;
            var hasStartedFalling = false;
            var hasLanded = false;

            await UniTask.WaitUntil(() =>
            {
                if (rb == null || block == null) return true;

                var y = block.transform.position.y;
                var speed = rb.linearVelocity.magnitude;
                var angular = rb.angularVelocity.magnitude;

                // 落下が始まったかを確認 
                if (speed > 1f)
                    hasStartedFalling = true;

                // 落ちすぎたら終了
                if (y < fallThreshold)
                {
                    return true;
                }
                
                // 着陸したかどうか
                if (hasStartedFalling && !hasLanded && speed < 1f)
                {
                    hasLanded = true;
                    _onBlockLanded.OnNext(Unit.Default);
                }
                
                // 落下が始まったあと、静止を検出
                if (hasStartedFalling && speed < 0.05f && angular < 0.05f)
                {
                    return true;
                }

                return false;
            }, cancellationToken: ct);

            _tcs?.TrySetResult();
        }
        
        protected override void OnDestroy()
        {
            base.OnDestroy();
            StopController();
            _disposables.Dispose();
        }
    }
}
