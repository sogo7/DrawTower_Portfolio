using DrawTower.Test;
using Zenject;

namespace DrawTower.Ui
{
	public class PauseBtnTest : TestSceneBase
	{
		[Inject]
		private IPauseBtn _pauseBtn;
		
		private void Start()
		{	
			BindLog(_pauseBtn.OnClickAsObservable(), "OnClickAsObservable");
			BindButton(0, _pauseBtn.Show);
			BindButton(1, _pauseBtn.Hide);	
			BindButton(2, _pauseBtn.ShowAndAnimAsync);
			BindButton(3, _pauseBtn.Clear);	
		}
	}
}
