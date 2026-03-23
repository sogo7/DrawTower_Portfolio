using System.Linq;
using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using R3;
using UnityEngine;

namespace DrawTower.Ui
{
    public class StampSelectUi : BaseUi, IStampSelectUi
    {
		[SerializeField]
		private ScaleAnimBtn _openBtn;
		
		[SerializeField]
		private RectTransform _selectArea;
		
		[SerializeField]
		private ScaleAnimBtn[] _selectBtns;
		
		private Tween _tween;
		
		public async UniTask ShowOpenBtnAsync(CancellationToken ct)
        {
			Show();
            await _openBtn.ShowAndAnimAsync(ct);
        }
        
        public async UniTask ShowSelectAreaAsync(CancellationToken ct)
        {
			Clear();
            _tween = _selectArea.DOAnchorPosX(100f, 0.3f).SetEase(Ease.OutBack);
            await _tween.ToUniTask(TweenCancelBehaviour.KillAndCancelAwait, ct);
        }
        
        public async UniTask HideSelectAreaAsync(CancellationToken ct)
        {
			Clear();
            _tween = _selectArea.DOAnchorPosX(-100f, 0.3f).SetEase(Ease.OutBack);
            await _tween.ToUniTask(TweenCancelBehaviour.KillAndCancelAwait, ct);
        }
		
        public Observable<Unit> OnClickOpenBtnAsObservable() => _openBtn.OnClickAsObservable();

        public Observable<StampType> OnClickSelectBtnAsObservable()
		{
			return Observable.Merge(_selectBtns.Select((btn, index) =>
				btn.OnClickAsObservable().Select(_ => (StampType)index)
			));
		}
		
		private void Clear()
		{
		    _tween?.Kill();
		}
    }
}
