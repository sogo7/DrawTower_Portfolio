using DrawTower.Test;
using Zenject;

namespace DrawTower.Ui
{
	public class RankingBtnTest : TestSceneBase
	{
		[Inject]
		private IRankingBtn _rankingBtn;
		
		private void Start()
		{	
			BindLog(_rankingBtn.OnClickAsObservable(), "OnClickAsObservable");
			BindButton(0, _rankingBtn.Show);
			BindButton(1, _rankingBtn.Hide);	
			BindButton(2, _rankingBtn.ShowAndAnimAsync);
			BindButton(3, _rankingBtn.Clear);	
		}
	}
}
