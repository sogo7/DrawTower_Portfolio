using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace DrawTower.Ui
{
	public class Countdown : BaseUi, ICountdown
	{
		[SerializeField]
		private Image[] _images;

		private Sequence _seq;

		public async UniTask StartCountdownAsync(CountdownType type, CancellationToken ct)
		{
			var index = (int)type;
    		var image = _images[index];

			image.transform.localScale = Vector3.zero;

			_seq = DOTween.Sequence()
				.Append(image.transform.DOScale(1f, 0.2f).SetEase(Ease.OutBounce))
				.AppendInterval(0.6f)
				.Append(image.transform.DOScale(0f, 0.2f).SetEase(Ease.OutBounce));

			await _seq.ToUniTask(TweenCancelBehaviour.KillAndCancelAwait, ct);
		}
		
		protected override void OnDestroy()
		{
			_seq?.Kill();
			base.OnDestroy();
		}
	}
	
	public enum CountdownType
	{
		Three,
		Two,
		One,
		Start
	}

}
