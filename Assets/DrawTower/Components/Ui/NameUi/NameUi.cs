using UnityEngine;
using DG.Tweening;
using TMPro;
using R3;
using Cysharp.Threading.Tasks;
using System.Threading;

namespace DrawTower.Ui
{
	public class NameUi : MonoBehaviour, INameUi
	{
		[SerializeField]
		private RectTransform _rect;
		
		[SerializeField]
		private TextMeshProUGUI _alert;
		
		[SerializeField] 
		private TMP_InputField _nameInputField;
		
		[SerializeField] 
		private ScaleAnimBtn _applyBtn;
		
		private Tween _tween;	
		
		public async UniTask ShowAndAnimAsync(CancellationToken ct)
        {
			_applyBtn.GetCustomBtn().interactable = true;
			gameObject.SetActive(true);
			
            _tween = _rect.DOScale(1f, 0.5f).SetEase(Ease.OutBack);
            await _tween.ToUniTask(TweenCancelBehaviour.KillAndCancelAwait, ct);
            
            SetEditMode(true);
        }
        
        public async UniTask HideAndAnimAsync(CancellationToken ct)
        {
			_applyBtn.GetCustomBtn().interactable = false;
			
        	_tween = _rect.DOScale(0f, 0.5f).SetEase(Ease.OutBack);
            await _tween.ToUniTask(TweenCancelBehaviour.KillAndCancelAwait, ct);
            
            gameObject.SetActive(false);
        }
		
		public string GetName() => _nameInputField.text;
		
		public bool IsNameLengthInvalid(string name)
		{
			var isInvalid = name.Length < 3 || name.Length > 7;
		    if (isInvalid)
				SetAlert("名前は3〜7文字で入力してください");
			
			return isInvalid;
		}
		
		public void ShowNgWordAlert()
		{
			SetAlert("不適切な名前です");
		}
		
		public void SetEditMode(bool isEdit)
		{
			_nameInputField.interactable = isEdit;
			_nameInputField.readOnly = !isEdit;

			if (isEdit)
			{
				_nameInputField.ActivateInputField();
				_nameInputField.Select();
			}
			else
			{
				_nameInputField.DeactivateInputField();
			}
		}

		
		private void SetAlert(string alert)
		{
		    _alert.gameObject.SetActive(true);
			_alert.text = alert;
			_alert.color = Color.red;
		}
		
		public Observable<string> OnClickApplyBtnAsObservable()
		{
			return _applyBtn
				.OnClickAsObservable()
				.Select(_ => _nameInputField.text);
		}

				
		private void OnDestroy()
		{
			_tween?.Kill();
		}
	}
}
