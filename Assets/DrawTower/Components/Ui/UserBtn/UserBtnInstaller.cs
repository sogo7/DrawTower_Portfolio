using Zenject;

namespace DrawTower.Ui
{
	public class UserBtnInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container
				.Bind<IUserBtn>()
				.FromInstance(GetComponent<IUserBtn>())
				.AsSingle();
		}
	}
}
