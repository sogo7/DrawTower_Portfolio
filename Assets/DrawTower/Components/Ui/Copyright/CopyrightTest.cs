using Zenject;
using DrawTower.Test;
using UnityEngine;

namespace DrawTower.Ui
{
    public class CopyrightTest : TestSceneBase
	{
		[Inject]		
		private ICopyright _copyright;
		
		[SerializeField]
		private string _testStr;
		
		private void Start()
		{
			var token = GetToken();
			BindLog(_copyright.OnClickCloseBtnAsObservable(), "OnClickCloseBtnAsObservable");
			
			BindButton(0, _copyright.ShowAndAnimAsync);
			BindButton(1, _copyright.HideAndAnimAsync);
			BindButton(2, () => _copyright.SetText(_testStr));
			BindButton(3, () => Debug.Log($"IsLoaded: {_copyright.IsLoaded()}"));
		}
	}
}