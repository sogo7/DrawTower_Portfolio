using DrawTower.Test;
using Zenject;

namespace DrawTower.Ui
{
    public class ProgressTest : TestSceneBase
	{
		[Inject]	
		private IProgress _progress;
		
		private void Start()
		{	
			BindButton(0, _progress.ShowAnimAsync);
			BindButton(1, _progress.Hide);
		}
	}
}

