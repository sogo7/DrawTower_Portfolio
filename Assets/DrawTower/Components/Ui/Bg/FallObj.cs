using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace DrawTower.Ui
{
	public class FallObj : MonoBehaviour
	{
		[SerializeField]
		private Sprite[] _sprites = new Sprite[4];
		
		[SerializeField]
		private Image _fallImage;
		
		private Sequence _seq;

		private float _moveDuration = 10f;
		private float _moveDurationRandomRange = 5f;
		private float _rotateDuration = 6f;
		private float _curveOffset = 340f;
		private float _swayAmplitude = 20f;
		private float _swayPeriod = 2.5f;
		private float _minHorizontalDistance = 100f;
		
		public async UniTask BgAnimAsync(RectTransform[] points, CancellationToken ct)
		{
			Clear();
			var rect = (RectTransform)transform;
			while (!ct.IsCancellationRequested)
			{
				SetRandomSprite();
				GetDiagonalRoute(points, out var start, out var end);
				var moveDuration = GetRandomMoveDuration();
				var rotateAmount = -360f * (moveDuration / _rotateDuration);
				var mid = Vector2.Lerp(start, end, 0.5f);
				var curveSign = Random.value < 0.5f ? -1f : 1f;
				var control1 = GetCurveControlPoint(start, mid, 0.35f, curveSign);
				var control2 = GetCurveControlPoint(start, mid, 0.7f, curveSign);
				var control3 = GetCurveControlPoint(mid, end, 0.35f, -curveSign);
				var control4 = GetCurveControlPoint(mid, end, 0.7f, -curveSign);

				rect.anchoredPosition = start;
				rect.localEulerAngles = new Vector3(0f, 0f, Random.Range(0f, 360f));
				_seq = DOTween.Sequence()
					.Append(DOVirtual.Float(0f, 1f, moveDuration, t =>
					{
						if (t < 0.5f)
						{
							var localT = t * 2f;
							rect.anchoredPosition = GetBezierPoint(start, control1, control2, mid, localT);
						}
						else
						{
							var localT = (t - 0.5f) * 2f;
							rect.anchoredPosition = GetBezierPoint(mid, control3, control4, end, localT);
						}
					}).SetEase(Ease.InQuad))
					.Join(rect.DOLocalRotate(new Vector3(0f, 0f, rotateAmount), moveDuration, RotateMode.FastBeyond360)
						.SetEase(Ease.Linear)
						.SetRelative());

				var swayRect = _fallImage != null ? _fallImage.rectTransform : null;
				if (swayRect != null && swayRect != rect && _swayAmplitude > 0f && _swayPeriod > 0f)
				{
					var swayLoops = Mathf.Max(1, Mathf.RoundToInt(moveDuration / _swayPeriod));
					await _seq.Join(swayRect.DOAnchorPosX(_swayAmplitude, _swayPeriod)
						.SetEase(Ease.InOutSine)
						.SetRelative()
						.SetLoops(swayLoops, LoopType.Yoyo));
				}

				await _seq.ToUniTask(TweenCancelBehaviour.KillAndCancelAwait, ct);
			}
		}

        public void Clear()
		{
			_seq?.Kill();
		}

		private void SetRandomSprite()
		{
			SetRandomScale();
			if (_fallImage != null && _sprites != null && _sprites.Length > 0)
				_fallImage.sprite = _sprites[Random.Range(0, _sprites.Length)];
		}

		private void SetRandomScale()
		{
			if (_fallImage == null)
				return;

			var scale = Random.Range(0.8f, 1f);
			_fallImage.transform.localScale = new Vector3(scale, scale, 1f);
		}

		private float GetRandomMoveDuration()
		{
			if (_moveDurationRandomRange <= 0f)
				return _moveDuration;

			var min = Mathf.Max(0.1f, _moveDuration - _moveDurationRandomRange);
			var max = Mathf.Max(min, _moveDuration + _moveDurationRandomRange);
			return Random.Range(min, max);
		}

		private void GetDiagonalRoute(RectTransform[] points, out Vector2 start, out Vector2 end)
		{
			var useFirst = Random.Range(0, 2) == 0;
			start = useFirst ? points[0].anchoredPosition : points[1].anchoredPosition;
			end = useFirst ? points[3].anchoredPosition : points[2].anchoredPosition;

			if (Mathf.Abs(start.x - end.x) < _minHorizontalDistance)
			{
				start = !useFirst ? points[0].anchoredPosition : points[1].anchoredPosition;
				end = !useFirst ? points[3].anchoredPosition : points[2].anchoredPosition;
			}
		}

		private Vector2 GetCurveControlPoint(Vector2 start, Vector2 end, float t, float sign)
		{
			if (_curveOffset <= 0f)
				return Vector2.Lerp(start, end, t);

			var dir = (end - start).normalized;
			var perp = new Vector2(-dir.y, dir.x);
			var offset = Random.Range(_curveOffset * 0.8f, _curveOffset * 1.5f) * sign;
			return Vector2.Lerp(start, end, t) + perp * offset;
		}

		private Vector2 GetBezierPoint(Vector2 start, Vector2 control1, Vector2 control2, Vector2 end, float t)
		{
			var u = 1f - t;
			return (u * u * u * start)
				+ (3f * u * u * t * control1)
				+ (3f * u * t * t * control2)
				+ (t * t * t * end);
		}
	}
}
