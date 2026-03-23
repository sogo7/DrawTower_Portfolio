using DrawTower.Test;
using Zenject;

namespace DrawTower.Ui
{
	public class TransitionTest : TestSceneBase
	{
		[Inject]
		private ITransition _transition;
		
		private void Start()
		{	
			BindButton(0, _transition.FadeInAsync);
			BindButton(1, _transition.FadeOutAsync);	
		}
	}
}
