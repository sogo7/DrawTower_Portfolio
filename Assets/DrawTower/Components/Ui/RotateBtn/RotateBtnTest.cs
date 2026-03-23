using DrawTower.Test;
using Zenject;

namespace DrawTower.Ui
{
	public class RotateBtnTest : TestSceneBase
	{
		[Inject]
		private IRotateBtn _rotateBtn;
		
		private void Start()
		{	
			BindLog(_rotateBtn.OnClickAsObservable(), "OnClickAsObservable");
			BindButton(0, _rotateBtn.Show);
			BindButton(1, _rotateBtn.Hide);	
			BindButton(2, _rotateBtn.ShowAndAnimAsync);
			BindButton(3, _rotateBtn.HideAndAnimAsync);
			BindButton(4, _rotateBtn.Clear);	
		}
	}
}
