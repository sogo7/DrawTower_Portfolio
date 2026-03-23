using DrawTower.Logic;
using Zenject;

namespace DrawTower.Ui
{
	public class BlockMonitorInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container
				.Bind<IBlockMonitor>()
				.FromInstance(GetComponent<IBlockMonitor>())
				.AsSingle();
		}
	}
}
