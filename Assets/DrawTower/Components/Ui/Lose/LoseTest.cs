using DrawTower.Test;
using Zenject;

namespace DrawTower.Ui
{
	public class LoseTest : TestSceneBase
	{
		[Inject]
		private ILose _lose;
		
		private void Start()
		{	
			var token = GetToken();
			BindButton(0, () => _lose.ShowAndAnimAsync(token));	
		}
	}
}
