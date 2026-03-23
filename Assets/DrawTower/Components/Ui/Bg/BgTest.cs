using DrawTower.Test;
using Zenject;

namespace DrawTower.Ui
{
	public class BgTest : TestSceneBase
	{
		[Inject]
		private IBg _bg;
		
		private void Start()
		{	
			BindButton(0, _bg.BgAnimAsync);
		}
	}
}
