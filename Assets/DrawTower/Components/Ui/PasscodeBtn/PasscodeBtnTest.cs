using DrawTower.Test;
using Zenject;

namespace DrawTower.Ui
{
	public class PasscodeBtnTest : TestSceneBase
	{
		[Inject]
		private IPasscodeBtn _passcodeBtn;
		
		private void Start()
		{	
			BindLog(_passcodeBtn.OnClickAsObservable(), "OnClickAsObservable");
			BindButton(0, _passcodeBtn.Show);
			BindButton(1, _passcodeBtn.Hide);	
			BindButton(2, _passcodeBtn.ShowAndAnimAsync);
			BindButton(3, _passcodeBtn.Clear);
			BindButton(4, _passcodeBtn.MoveLeftAnimAsync);
			BindButton(5, _passcodeBtn.MoveCenterAnimAsync);
			BindButton(6, _passcodeBtn.MoveRightAnimAsync);
		}
	}
}
