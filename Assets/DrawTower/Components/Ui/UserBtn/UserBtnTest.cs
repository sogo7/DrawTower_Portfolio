using DrawTower.Test;
using Zenject;

namespace DrawTower.Ui
{
	public class UserBtnTest : TestSceneBase
	{
		[Inject]
		private IUserBtn _userBtn;
		
		private void Start()
		{	
			BindLog(_userBtn.OnClickAsObservable(), "OnClickAsObservable");
			BindButton(0, _userBtn.Show);
			BindButton(1, _userBtn.Hide);	
			BindButton(2, _userBtn.ShowAndAnimAsync);
			BindButton(3, _userBtn.Clear);	
		}
	}
}
