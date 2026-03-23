using DrawTower.Test;
using UnityEngine;
using Zenject;

namespace DrawTower.Ui
{
	public class PasscodeAreaTest : TestSceneBase
	{
		[Inject]
		private IPasscodeArea _passcodeArea;
		
		private void Start()
		{	
			BindLog(_passcodeArea.OnClickSearchBtnAsObservable(), "OnClickSearchBtnAsObservable");
			BindButton(0, () => Debug.Log($"GetPasscode: {_passcodeArea.GetPasscode()}"));
			BindButton(1, _passcodeArea.MoveLeftAnimAsync);
			BindButton(2, _passcodeArea.MoveCenterAnimAsync);
			BindButton(3, _passcodeArea.MoveRightAnimAsync);
		}
	}
}
