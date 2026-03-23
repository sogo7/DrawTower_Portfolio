using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using R3;
using UnityEngine;

namespace DrawTower.Ui
{
	public class ScaleAnimBtn : BaseUi
	{
		[SerializeField]
	    protected CustomBtn _btn;
		
		private Tween _tween;
		
		private CompositeDisposable _disposables = new();
		
		private void Start()
		{
		    _btn.OnPointerDownAsObservable()
                .Subscribe(_ => AnimatePress())
                .AddTo(_disposables);
            
            _btn.OnPointerUpAsObservable()
                .Subscribe(_ => AnimateRelease())
                .AddTo(_disposables);
		}
		
		public async UniTask ShowAndAnimAsync(CancellationToken ct)
        {
            Clear();
            Show();

            _tween = _btn.transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack);
            await _tween.ToUniTask(TweenCancelBehaviour.KillAndCancelAwait, ct);
        }
        
        public async UniTask HideAndAnimAsync(CancellationToken ct)
        {
            _tween = _btn.transform.DOScale(0f, 0.5f).SetEase(Ease.OutBack);
            await _tween.ToUniTask(TweenCancelBehaviour.KillAndCancelAwait, ct);
            Hide();
        }
		
		private void AnimatePress()
        {
            _tween?.Kill();
            _tween = _btn.transform.DOScale(0.8f, 0.1f).SetEase(Ease.OutQuad);
        }

        private void AnimateRelease()
        {
            _tween?.Kill();
            _tween = _btn.transform.DOScale(1f, 0.15f).SetEase(Ease.OutBack);
        }

        public virtual void Clear()
        {
            _tween?.Kill();
            _btn.transform.localScale = Vector3.zero;
        }
        
        public CustomBtn GetCustomBtn() => _btn;
        
        protected override void OnDestroy()
		{
			_tween?.Kill();
			_disposables.Dispose();
			base.OnDestroy();
		}
		
		public Observable<Unit> OnClickAsObservable() => _btn.OnClickAsObservable();
	}
}
