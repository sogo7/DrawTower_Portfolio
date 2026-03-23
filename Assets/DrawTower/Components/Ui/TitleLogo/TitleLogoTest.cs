using DrawTower.Test;
using Zenject;

namespace DrawTower.Ui
{
	public class TitleLogoTest : TestSceneBase
	{
		[Inject]
		private ITitleLogo _titleLogo;
		
		private void Start()
		{	
			BindButton(0, _titleLogo.Show);
			BindButton(1, _titleLogo.Hide);
			BindButton(2, _titleLogo.ShowAndAnimAsync);
			BindButton(3, _titleLogo.Clear);
		}
	}
}
