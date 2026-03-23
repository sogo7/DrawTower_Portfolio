using Zenject;

namespace DrawTower.Ui
{
	public class VibrationToggleInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container
				.Bind<IVibrationToggle>()
				.FromInstance(GetComponent<IVibrationToggle>())
				.AsSingle();
		}
	}
}
