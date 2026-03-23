using Zenject;

namespace DrawTower.Ui
{
	public class CountdownInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container
				.Bind<ICountdown>()
				.FromInstance(GetComponent<ICountdown>())
				.AsSingle();
		}
	}
}
