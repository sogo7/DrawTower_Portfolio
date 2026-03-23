using Zenject;

namespace DrawTower.Object
{
	public class KillZoneInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container
				.Bind<IKillZone>()
				.FromInstance(GetComponent<IKillZone>())
				.AsSingle();
		}
	}
}