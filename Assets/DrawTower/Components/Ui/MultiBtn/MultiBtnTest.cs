using DrawTower.Test;
using Zenject;

namespace DrawTower.Ui
{
	public class MultiBtnTest : TestSceneBase
	{
		[Inject]
		private IMultiBtn _multiBtn;
		
		private void Start()
		{	
			BindLog(_multiBtn.OnClickAsObservable(), "OnClickAsObservable");
			BindButton(0, _multiBtn.Show);
			BindButton(1, _multiBtn.Hide);	
			BindButton(2, _multiBtn.ShowAndAnimAsync);
			BindButton(3, _multiBtn.Clear);
			BindButton(4, _multiBtn.MoveLeftAnimAsync);
			BindButton(5, _multiBtn.MoveCenterAnimAsync);
		}
	}
}
