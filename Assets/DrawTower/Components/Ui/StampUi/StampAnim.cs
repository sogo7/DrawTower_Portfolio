using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace DrawTower.Ui
{
	public class StampAnim : MonoBehaviour
	{
		[SerializeField]
		private CanvasGroup _canvasGroup;
		
		[SerializeField]
		private RectTransform _textImageRoot;
		
		private Sequence _seq;
		
		private float _duration = 0.2f;
		
		private float _initScale = 0.8f;
		
		public async UniTask ShowAndAnimAsync(CancellationToken ct)
		{
			Clear();
			_seq = DOTween.Sequence()
				.Append(gameObject.transform.DOScale(1f, _duration).SetEase(Ease.OutBack))
				.Join(_canvasGroup.DOFade(1f, _duration).SetEase(Ease.OutBack))
				.Append(_textImageRoot.transform.DOScale(1f, _duration).SetEase(Ease.OutBack))
				.AppendInterval(1f)
				.Append(gameObject.transform.DOScale(_initScale, _duration).SetEase(Ease.OutBack))
				.Join(_canvasGroup.DOFade(0f, _duration).SetEase(Ease.OutBack));
            await _seq.ToUniTask(TweenCancelBehaviour.KillAndCancelAwait, ct);
		}
		
		private void Clear()
        {
            _seq?.Kill();
            _canvasGroup.alpha = 0;
            gameObject.transform.localScale = new Vector3(_initScale, _initScale, _initScale);
            _textImageRoot.transform.localScale = Vector3.zero;
        }
	}
}