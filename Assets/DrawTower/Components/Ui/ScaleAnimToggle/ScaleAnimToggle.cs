using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using R3;
using UnityEngine;

namespace DrawTower.Ui
{
	public class ScaleAnimToggle : BaseUi
	{
		[SerializeField]
		private CustomToggle _toggle;
		
		private Tween _tween;
		
		private CompositeDisposable _disposables = new();
		
		private void Start()
		{
		    SetupEvents();
		}
		
		public async UniTask ShowAndAnimAsync(CancellationToken cancellationToken)
        {
            Clear();
            Show();
            SetupEvents();
            
            _tween = _toggle.transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack);
            await _tween.ToUniTask(TweenCancelBehaviour.KillAndCancelAwait, cancellationToken);
        }
        
        public void SetIsOn(bool isOn) => _toggle.isOn = isOn;
        
        private void SetupEvents()
        {
            _disposables.Clear();
            
            _toggle.OnPointerDownAsObservable()
                .Subscribe(_ => AnimatePress())
                .AddTo(_disposables);
            
            _toggle.OnPointerUpAsObservable()
                .Subscribe(_ => AnimateRelease())
                .AddTo(_disposables);
        }
		
		private void AnimatePress()
        {
            _tween?.Kill();
            _tween = _toggle.transform.DOScale(0.8f, 0.1f).SetEase(Ease.OutQuad);
        }

        private void AnimateRelease()
        {
            _tween?.Kill();
            _tween = _toggle.transform.DOScale(1f, 0.15f).SetEase(Ease.OutBack);
        }

        public virtual void Clear()
        {
            _tween?.Kill();
            _toggle.transform.localScale = Vector3.zero;
        }
        
        protected override void OnDestroy()
		{
			_tween?.Kill();
			_disposables.Dispose();
			base.OnDestroy();
		}
		
		public Observable<bool> OnValueChangedAsObservable() => _toggle.onValueChanged.AsObservable();
	}
}
