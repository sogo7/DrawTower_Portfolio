using Zenject;

namespace DrawTower.Ui
{
	public class SettingsBtnInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container
				.Bind<ISettingsBtn>()
				.FromInstance(GetComponent<ISettingsBtn>())
				.AsSingle();
		}
	}
}
