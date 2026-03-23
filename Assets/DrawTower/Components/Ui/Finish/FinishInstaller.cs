using Zenject;

namespace DrawTower.Ui
{
	public class FinishInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container
				.Bind<IFinish>()
				.FromInstance(GetComponent<IFinish>())
				.AsSingle();
		}
	}
}
