using Zenject;

namespace DrawTower.Ui
{
	public class StampUiInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container
				.Bind<IStampUi>()
				.FromInstance(GetComponent<IStampUi>())
				.AsSingle();
		}
	}
}
