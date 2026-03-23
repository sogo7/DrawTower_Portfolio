using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace DrawTower.Ui
{
	public class Cloud : MonoBehaviour
	{		
		private Sequence _seq;

		private float _moveDuration = 60f;

		private float _scaleDuration = 0.8f;

		private Vector3 _targetScale = new Vector3(1.08f, 1.05f, 1f);
		
		public async UniTask BgAnimAsync(RectTransform[] points, CancellationToken ct)
		{
			Clear();
			var rect = (RectTransform)transform;
			rect.anchoredPosition = points[0].anchoredPosition;
			rect.localScale = Vector3.one;
			_seq = DOTween.Sequence()
				.Append(rect.DOAnchorPos(points[1].anchoredPosition, _moveDuration)
					.SetEase(Ease.InOutSine)
					.SetLoops(-1, LoopType.Restart))
				.Join(rect.DOScale(_targetScale, _scaleDuration)
					.SetEase(Ease.InOutSine)
					.SetLoops(-1, LoopType.Yoyo));

			await _seq.ToUniTask(TweenCancelBehaviour.KillAndCancelAwait, ct);
		}

        public void Clear()
		{
			_seq?.Kill();
		}
	}
}
