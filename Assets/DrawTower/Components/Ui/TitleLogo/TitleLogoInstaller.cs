using Zenject;

namespace DrawTower.Ui
{
	public class TitleLogoInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container
				.Bind<ITitleLogo>()
				.FromInstance(GetComponent<ITitleLogo>())
				.AsSingle();
		}
	}
}
