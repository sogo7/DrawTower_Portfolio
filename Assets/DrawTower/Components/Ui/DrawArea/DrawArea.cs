using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace DrawTower.Ui
{
	public class DrawArea : BaseUi, IDrawArea
	{
		[SerializeField]
		private Image _image;
		
		[SerializeField]
		private RectTransform _area;

		private Sequence _seq;

		/// <summary>
		/// 画面上部へ移動＋縮小
		/// </summary>
		public async UniTask TopAnimAsync(CancellationToken ct)
		{
			_seq?.Kill();

			_seq = DOTween.Sequence()
				.Append(_image.rectTransform.DOAnchorPos(new Vector2(0f, -550f), 0.5f).SetEase(Ease.OutCubic))// 上部に移動
				.Join(_image.transform.DOScale(0.6f, 0.5f).SetEase(Ease.OutBack));// 縮小

			await _seq.ToUniTask(TweenCancelBehaviour.KillAndCancelAwait, ct);
		}

		/// <summary>
		/// 画面中央へ移動＋拡大
		/// </summary>
		public async UniTask CenterAnimAsync(CancellationToken ct)
		{
			_seq?.Kill();
			
			var height = ((RectTransform)gameObject.transform).rect.height;

			_seq = DOTween.Sequence()
				.Append(_image.rectTransform.DOAnchorPos(new Vector2(0f, -height / 2), 0.5f).SetEase(Ease.OutCubic))// 中央に移動
				.Join(_image.transform.DOScale(0.8f, 0.5f).SetEase(Ease.OutBack));// 拡大

			await _seq.ToUniTask(TweenCancelBehaviour.KillAndCancelAwait, ct);
		}

		public RectTransform GetArea() => _area;

		protected override void OnDestroy()
		{
			_seq?.Kill();
			base.OnDestroy();
		}
	}
}
