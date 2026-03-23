using DrawTower.Test;
using Zenject;

namespace DrawTower.Ui
{
	public class TapToNextBtnTest : TestSceneBase
	{
		[Inject]
		private ITapToNextBtn _tapToNextBtn;
		
		private void Start()
		{	
			BindLog(_tapToNextBtn.OnClickAsObservable(), "OnClickAsObservable");
			BindButton(0, _tapToNextBtn.Show);
			BindButton(1, _tapToNextBtn.Hide);	
			BindButton(2, _tapToNextBtn.ShowAndAnimAsync);
			BindButton(3, _tapToNextBtn.Clear);	
		}
	}
}
