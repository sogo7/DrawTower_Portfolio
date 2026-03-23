using DrawTower.Test;
using Zenject;

namespace DrawTower.Ui
{
	public class FinishTest : TestSceneBase
	{
		[Inject]
		private IFinish _finish;
		
		private void Start()
		{	
			BindButton(0, _finish.ShowAndAnimAsync);		
		}
	}
}
