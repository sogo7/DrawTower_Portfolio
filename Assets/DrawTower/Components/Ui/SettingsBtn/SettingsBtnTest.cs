using DrawTower.Test;
using Zenject;

namespace DrawTower.Ui
{
	public class SettingsBtnTest : TestSceneBase
	{
		[Inject]
		private ISettingsBtn _settingsBtn;
		
		private void Start()
		{	
			BindLog(_settingsBtn.OnClickAsObservable(), "OnClickAsObservable");
			BindButton(0, _settingsBtn.Show);
			BindButton(1, _settingsBtn.Hide);	
			BindButton(2, _settingsBtn.ShowAndAnimAsync);
			BindButton(3, _settingsBtn.Clear);	
		}
	}
}
