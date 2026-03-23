using DrawTower.Test;
using Zenject;

namespace DrawTower.Ui
{
	public class CountdownTest : TestSceneBase
	{
		[Inject]
		private ICountdown _countdown;
		
		private void Start()
		{	
			var token = GetToken();
			BindButton(0, _countdown.Show);
			BindButton(1, _countdown.Hide);
			BindButton(2, () => _countdown.StartCountdownAsync(CountdownType.Three, token));	
		}
	}
}
