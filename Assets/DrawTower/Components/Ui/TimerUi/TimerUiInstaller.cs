using Zenject;

namespace DrawTower.Ui
{
	public class TimerUiInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container
				.Bind<ITimerUi>()
				.FromInstance(GetComponent<ITimerUi>())
				.AsSingle();
		}
	}
}
