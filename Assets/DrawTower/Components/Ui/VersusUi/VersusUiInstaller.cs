using Zenject;

namespace DrawTower.Ui
{
	public class VersusUiInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container
				.Bind<IVersusUi>()
				.FromInstance(GetComponent<IVersusUi>())
				.AsSingle();
		}
	}
}
