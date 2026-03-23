using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DrawTower.Ui
{
	public class Copyright : MonoBehaviour, ICopyright
	{
		[SerializeField]
		private Button _closeBtn;
		
		[SerializeField]
		private TextMeshProUGUI _text;
		
		private Tween _tween;
		
		public async UniTask ShowAndAnimAsync(CancellationToken ct)
        {
			gameObject.transform.localScale = Vector3.zero;
			gameObject.SetActive(true);

            _tween = gameObject.transform.DOScale(1f, 0.5f).SetEase(Ease.OutBack);
            await _tween.ToUniTask(TweenCancelBehaviour.KillAndCancelAwait, ct);
        }
        
        public async UniTask HideAndAnimAsync(CancellationToken ct)
        {
        	 _tween = gameObject.transform.DOScale(0f, 0.25f).SetEase(Ease.OutQuart);
            await _tween.ToUniTask(TweenCancelBehaviour.KillAndCancelAwait, ct);
            
            gameObject.SetActive(false);
        }
        
        public void SetText(string text) => _text.text = text;
        
        public bool IsLoaded() => !string.IsNullOrEmpty(_text.text);
        
		public Observable<Unit> OnClickCloseBtnAsObservable() => _closeBtn.OnClickAsObservable();
		
		private void OnDestroy()
		{
			_tween?.Kill();
		}
	}
}

