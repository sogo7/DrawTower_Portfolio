using Zenject;

namespace DrawTower.Logic
{
	public class LineBlockCreatorInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container
				.Bind<ILineBlockCreator>()
				.FromInstance(GetComponent<ILineBlockCreator>())
				.AsSingle();
		}
	}
}