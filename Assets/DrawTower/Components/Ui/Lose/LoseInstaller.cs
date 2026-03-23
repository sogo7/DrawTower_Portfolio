using Zenject;

namespace DrawTower.Ui
{
	public class LoseInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container
				.Bind<ILose>()
				.FromInstance(GetComponent<ILose>())
				.AsSingle();
		}
	}
}
