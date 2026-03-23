using DrawTower.Test;
using Zenject;

namespace DrawTower.Ui
{
	public class HomeBtnTest : TestSceneBase
	{
		[Inject]
		private IHomeBtn _homeBtn;
		
		private void Start()
		{	
			BindLog(_homeBtn.OnClickAsObservable(), "OnClickAsObservable");
			BindButton(0, _homeBtn.Show);
			BindButton(1, _homeBtn.Hide);	
			BindButton(2, _homeBtn.ShowAndAnimAsync);
			BindButton(3, _homeBtn.Clear);	
		}
	}
}
