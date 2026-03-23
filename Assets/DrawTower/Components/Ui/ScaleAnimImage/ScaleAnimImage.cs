using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace DrawTower.Ui
{
	public class ScaleAnimImage : BaseUi
	{
        [SerializeField]
		private Image _image;

		private Sequence _seq;

        [SerializeField]
		private float _displayDuration = 0.6f;

		public async UniTask ShowAndAnimAsync(CancellationToken ct)
		{
			_seq?.Kill();
			_image.transform.localScale = Vector3.zero;
			Show();
			
			_seq = DOTween.Sequence()
				.Append(_image.transform.DOScale(1f, 0.2f).SetEase(Ease.OutBounce))
				.AppendInterval(_displayDuration)
				.Append(_image.transform.DOScale(0f, 0.2f).SetEase(Ease.OutBounce));

			await _seq.ToUniTask(TweenCancelBehaviour.KillAndCancelAwait, ct);
			Hide();
		}
		
		protected override void OnDestroy()
		{
			_seq?.Kill();
			base.OnDestroy();
		}
	}
}
