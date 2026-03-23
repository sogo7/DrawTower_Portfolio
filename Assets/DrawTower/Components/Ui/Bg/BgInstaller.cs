using Zenject;

namespace DrawTower.Ui
{
	public class BgInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container
				.Bind<IBg>()
				.FromInstance(GetComponent<IBg>())
				.AsSingle();
		}
	}
}
