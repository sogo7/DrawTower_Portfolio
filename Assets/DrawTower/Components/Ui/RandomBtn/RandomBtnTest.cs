using DrawTower.Test;
using Zenject;

namespace DrawTower.Ui
{
	public class RandomBtnTest : TestSceneBase
	{
		[Inject]
		private IRandomBtn _randomBtn;
		
		private void Start()
		{	
			BindLog(_randomBtn.OnClickAsObservable(), "OnClickAsObservable");
			BindButton(0, _randomBtn.Show);
			BindButton(1, _randomBtn.Hide);	
			BindButton(2, _randomBtn.ShowAndAnimAsync);
			BindButton(3, _randomBtn.Clear);
			BindButton(4, _randomBtn.MoveLeftAnimAsync);
			BindButton(5, _randomBtn.MoveCenterAnimAsync);
			BindButton(6, _randomBtn.MoveRightAnimAsync);
		}
	}
}
