using DrawTower.Test;
using Zenject;

namespace DrawTower.Ui
{
	public class StartBtnTest : TestSceneBase
	{
		[Inject]
		private IStartBtn _startBtn;
		
		private void Start()
		{	
			BindLog(_startBtn.OnClickAsObservable(), "OnClickAsObservable");
			BindButton(0, _startBtn.Show);
			BindButton(1, _startBtn.Hide);	
			BindButton(2, _startBtn.ShowAndAnimAsync);
			BindButton(3, _startBtn.Clear);	
		}
	}
}
