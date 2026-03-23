using Zenject;

namespace DrawTower.Ui
{
	public class UserInfoInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container
				.Bind<IUserInfo>()
				.FromInstance(GetComponent<IUserInfo>())
				.AsSingle();
		}
	}
}
