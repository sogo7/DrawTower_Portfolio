using Zenject;

namespace DrawTower.Ui
{
	public class BgmToggleInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container
				.Bind<IBgmToggle>()
				.FromInstance(GetComponent<IBgmToggle>())
				.AsSingle();
		}
	}
}
