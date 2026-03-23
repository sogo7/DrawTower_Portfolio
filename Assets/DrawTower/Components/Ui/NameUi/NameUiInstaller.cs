using Zenject;

namespace DrawTower.Ui
{
	public class NameUiInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container
				.Bind<INameUi>()
				.FromInstance(GetComponent<INameUi>())
				.AsSingle();
		}
	}
}
