using DrawTower.Test;
using Zenject;

namespace DrawTower.Ui
{
	public class SingleBtnTest : TestSceneBase
	{
		[Inject]
		private ISingleBtn _singleBtn;
		
		private void Start()
		{	
			BindLog(_singleBtn.OnClickAsObservable(), "OnClickAsObservable");
			BindButton(0, _singleBtn.Show);
			BindButton(1, _singleBtn.Hide);	
			BindButton(2, _singleBtn.ShowAndAnimAsync);
			BindButton(3, _singleBtn.Clear);
			BindButton(4, _singleBtn.MoveLeftAnimAsync);
			BindButton(5, _singleBtn.MoveCenterAnimAsync);
		}
	}
}
