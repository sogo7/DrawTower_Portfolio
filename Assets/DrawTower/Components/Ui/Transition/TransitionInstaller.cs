using Zenject;

namespace DrawTower.Ui
{
	public class TransitionInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container
				.Bind<ITransition>()
				.FromInstance(GetComponent<ITransition>())
				.AsSingle();
		}
	}
}
