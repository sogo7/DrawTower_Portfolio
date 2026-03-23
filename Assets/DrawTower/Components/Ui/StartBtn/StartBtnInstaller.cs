using Zenject;

namespace DrawTower.Ui
{
	public class StartBtnInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container
				.Bind<IStartBtn>()
				.FromInstance(GetComponent<IStartBtn>())
				.AsSingle();
		}
	}
}
