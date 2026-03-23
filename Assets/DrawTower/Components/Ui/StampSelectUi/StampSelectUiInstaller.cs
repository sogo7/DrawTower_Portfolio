using Zenject;

namespace DrawTower.Ui
{
	public class StampSelectUiInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container
				.Bind<IStampSelectUi>()
				.FromInstance(GetComponent<IStampSelectUi>())
				.AsSingle();
		}
	}
}
