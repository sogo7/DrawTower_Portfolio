using System.Threading;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using R3;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DrawTower.Ui
{
	public class Dialog : MonoBehaviour, IDialog
	{
		[SerializeField]
		private DialogDataSO _dialogDataSO;
		
		[SerializeField]
		private RectTransform _bg;
		
		[SerializeField]
		private Button _closeBtn;
		
		[SerializeField]
		private GameObject _yesNoArea;
		
		[SerializeField]
		private CustomBtn _yesBtn;
		
		[SerializeField]
		private CustomBtn _noBtn;
		
		[SerializeField]
		private GameObject _actionArea;
		
		[SerializeField]
		private CustomBtn _actionBtn;
		
		[SerializeField]
		private TextMeshProUGUI _text;
		
		[SerializeField]
		private TextMeshProUGUI _actionBtnText;
		
		private Tween _tween;
		
		private DialogType _dialogType;
		        
        public async UniTask ShowAndAnimAsync(DialogType dialogType, CancellationToken ct, string text = null)
        {
			_dialogType = dialogType;
			var data = _dialogDataSO.GetDialogDataByType(dialogType);
			_text.text = string.IsNullOrWhiteSpace(text) ? data.text : text;
			
			var isYesNo = data.dialogMode == DialogMode.YesNo;
			var isAction = data.dialogMode == DialogMode.Action;
			var isClose = data.dialogMode == DialogMode.Close;
			
			_yesNoArea.SetActive(isYesNo);
			_actionArea.SetActive(isAction);
			_actionBtnText.text = data.actionBtnText;
			_closeBtn.interactable = isClose;

			_bg.localScale = Vector3.zero;
			gameObject.SetActive(true);

            _tween = _bg.DOScale(1f, 0.5f).SetEase(Ease.OutBack);
            await _tween.ToUniTask(TweenCancelBehaviour.KillAndCancelAwait, ct);
        }
        
        public async UniTask HideAndAnimAsync(CancellationToken ct)
        {
        	_tween = _bg.DOScale(0f, 0.5f).SetEase(Ease.OutBack);
            await _tween.ToUniTask(TweenCancelBehaviour.KillAndCancelAwait, ct);
            
            gameObject.SetActive(false);
        }
        
        public DialogType GetDialogType() => _dialogType;

		public Observable<Unit> OnClickCloseBtnAsObservable() => _closeBtn.OnClickAsObservable();
		public Observable<Unit> OnClickYesBtnAsObservable() => _yesBtn.OnClickAsObservable();
		public Observable<Unit> OnClickNoBtnAsObservable() => _noBtn.OnClickAsObservable();
		public Observable<Unit> OnClickActionBtnAsObservable() => _actionBtn.OnClickAsObservable();
		
		private void OnDestroy()
		{
			_tween?.Kill();
		}
	}
}
