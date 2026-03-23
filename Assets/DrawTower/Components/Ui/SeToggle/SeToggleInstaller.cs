using Zenject;

namespace DrawTower.Ui
{
	public class SeToggleInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container
				.Bind<ISeToggle>()
				.FromInstance(GetComponent<ISeToggle>())
				.AsSingle();
		}
	}
}
