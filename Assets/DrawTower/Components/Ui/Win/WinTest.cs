using DrawTower.Test;
using Zenject;

namespace DrawTower.Ui
{
	public class WinTest : TestSceneBase
	{
		[Inject]
		private IWin _win;
		
		private void Start()
		{	
			var token = GetToken();
			BindButton(0, () => _win.ShowAndAnimAsync(token));	
		}
	}
}
