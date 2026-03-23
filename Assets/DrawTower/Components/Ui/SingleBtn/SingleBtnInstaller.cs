using Zenject;

namespace DrawTower.Ui
{
	public class SingleBtnInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container
				.Bind<ISingleBtn>()
				.FromInstance(GetComponent<ISingleBtn>())
				.AsSingle();
		}
	}
}
