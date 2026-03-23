using DrawTower.Test;
using Zenject;

namespace DrawTower.Ui
{
	public class CopyrightBtnTest : TestSceneBase
	{
		[Inject]
		private ICopyrightBtn _copyrightBtn;
		
		private void Start()
		{	
			BindLog(_copyrightBtn.OnClickAsObservable(), "OnClickAsObservable");
			BindButton(0, _copyrightBtn.Show);
			BindButton(1, _copyrightBtn.Hide);	
			BindButton(2, _copyrightBtn.ShowAndAnimAsync);
			BindButton(3, _copyrightBtn.Clear);
		}
	}
}
