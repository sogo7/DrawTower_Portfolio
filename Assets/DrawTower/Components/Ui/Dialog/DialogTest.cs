using Zenject;
using DrawTower.Test;
using UnityEngine;
using R3;

namespace DrawTower.Ui
{
    public class DialogTest : TestSceneBase
	{
		[Inject]		
		private IDialog _gialog;
		
		[SerializeField]
		private DialogType _dialogType;
		
		[SerializeField]
		private string _text;
		
		private void Start()
		{
			var token = GetToken();
			_gialog.OnClickCloseBtnAsObservable()
				.Subscribe(_ => Debug.Log($"[TestScene] OnClickCloseBtnAsObservable: {_gialog.GetDialogType()}"))
				.AddTo(this);
				
			_gialog.OnClickYesBtnAsObservable()
				.Subscribe(_ => Debug.Log($"[TestScene] OnClickYesBtnAsObservable: {_gialog.GetDialogType()}"))
				.AddTo(this);
				
			_gialog.OnClickNoBtnAsObservable()
				.Subscribe(_ => Debug.Log($"[TestScene] OnClickNoBtnAsObservable: {_gialog.GetDialogType()}"))
				.AddTo(this);
				
			_gialog.OnClickActionBtnAsObservable()
				.Subscribe(_ => Debug.Log($"[TestScene] OnClickActionBtnAsObservable: {_gialog.GetDialogType()}"))
				.AddTo(this);
			
			BindButton(0, () => _gialog.ShowAndAnimAsync(_dialogType, token, _text));
			BindButton(1, _gialog.HideAndAnimAsync);
		}
	}
}