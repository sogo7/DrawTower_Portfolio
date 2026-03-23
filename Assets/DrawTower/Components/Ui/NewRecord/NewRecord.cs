using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace DrawTower.Ui
{
	public class NewRecord : BaseUi, INewRecord
	{
		[SerializeField]
		private Image _image;

		private Tween _tween;

		public async UniTask ShowAnimAsync(CancellationToken ct)
		{
			_image.transform.localScale = Vector3.zero;		
			_tween = _image.transform.DOScale(1f, 0.3f).SetEase(Ease.OutBounce);
			await _tween.ToUniTask(TweenCancelBehaviour.KillAndCancelAwait, ct);
		}
		
		protected override void OnDestroy()
		{
			_tween?.Kill();
			base.OnDestroy();
		}
	}
}
