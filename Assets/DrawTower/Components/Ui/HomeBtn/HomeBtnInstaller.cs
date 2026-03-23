using Zenject;

namespace DrawTower.Ui
{
	public class HomeBtnInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container
				.Bind<IHomeBtn>()
				.FromInstance(GetComponent<IHomeBtn>())
				.AsSingle();
		}
	}
}
