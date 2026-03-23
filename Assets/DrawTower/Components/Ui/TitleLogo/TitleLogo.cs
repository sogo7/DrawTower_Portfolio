using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace DrawTower.Ui
{
	public class TitleLogo : BaseUi, ITitleLogo
	{
		[SerializeField]
		private RectTransform _rect;
		
		private Tween _tween;

		private const float TITLE_LOGO_START_POS_Y = 600f;
		
		private const float TITLE_LOGO_END_POS_Y = -600f;
		
		public async UniTask ShowAndAnimAsync(CancellationToken cancellationToken)
		{
			Clear();
			Show();
			_tween = _rect.DOAnchorPosY(TITLE_LOGO_END_POS_Y, 1.5f);
			await _tween.ToUniTask(TweenCancelBehaviour.KillAndCancelAwait, cancellationToken);
		}

        public void Clear()
		{
		    _tween?.Kill();
		    _rect.anchoredPosition = new Vector2(0, TITLE_LOGO_START_POS_Y);
		}
		
		protected override void OnDestroy()
		{
			_tween?.Kill();
			base.OnDestroy();
		}
	}
}
