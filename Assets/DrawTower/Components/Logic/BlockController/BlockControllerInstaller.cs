using DrawTower.Logic;
using Zenject;

namespace DrawTower.Ui
{
	public class BlockControllerInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container
				.Bind<IBlockController>()
				.FromInstance(GetComponent<IBlockController>())
				.AsSingle();
		}
	}
}
