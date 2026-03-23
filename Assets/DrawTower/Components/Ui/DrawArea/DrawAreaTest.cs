using DrawTower.Test;
using Zenject;

namespace DrawTower.Ui
{
	public class DrawAreaTest : TestSceneBase
	{
		[Inject]
		private IDrawArea _drawArea;
		
		private void Start()
		{	
			BindButton(0, _drawArea.Show);
			BindButton(1, _drawArea.Hide);
			BindButton(2, _drawArea.TopAnimAsync);
			BindButton(3, _drawArea.CenterAnimAsync);
		}
	}
}
