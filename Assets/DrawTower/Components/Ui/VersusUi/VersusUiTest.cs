using DrawTower.Model;
using DrawTower.Test;
using Zenject;

namespace DrawTower.Ui
{
	public class VersusUiTest : TestSceneBase
	{
		[Inject]
		private IVersusUi _versusUi;
		
		private void Start()
		{
			var myData = new Player()
			{
			    colorId = "1"
			};
			var opponentData = new Player()
			{
			    colorId = "3"
			};
			
			BindButton(0, () => _versusUi.SetMyData(myData, "マイデータ"));
			BindButton(1, () => _versusUi.SetOpponentData(opponentData, "対戦相手"));
			BindButton(2, _versusUi.ShowAndAnimAsync);
			BindButton(3, _versusUi.Clear);
		}
	}
}
