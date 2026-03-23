using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using R3;
using UnityEngine;
using UnityEngine.UI;

namespace DrawTower.Ui
{
	public class StartBtn : BaseUi, IStartBtn
	{
		[SerializeField]
		private Button _btn;
		
		private Sequence _sequence;
		
		public async UniTask ShowAndAnimAsync(CancellationToken cancellationToken)
        {
            _btn.interactable = true;
            _btn.transform.localScale = Vector3.zero;
            
            Show();

            _sequence?.Kill();
            _sequence = DOTween.Sequence()
				.Append(_btn.transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack))
				.Append(_btn.transform.DOScale(1.2f, 0.5f)
					.SetEase(Ease.InOutSine)
					.SetLoops(-1, LoopType.Yoyo));

            await _sequence.ToUniTask(TweenCancelBehaviour.KillAndCancelAwait, cancellationToken);
        }

        public void Clear()
        {
            _sequence?.Kill();
            _btn.interactable = false;
            _btn.transform.localScale = Vector3.zero;
        }
		
		public Observable<Unit> OnClickAsObservable() => _btn.OnClickAsObservable();
	}
}
