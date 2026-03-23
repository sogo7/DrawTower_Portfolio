using DrawTower.Test;
using Zenject;

namespace DrawTower.Ui
{
	public class TimerUiTest : TestSceneBase
	{
		[Inject]
		private ITimerUi _timerUi;
		
		private void Start()
		{	
			BindButton(0, () => _timerUi.StartTimerAsync(10));
			BindButton(1, () => _timerUi.Clear());
			BindLog(_timerUi.OnTimeUp(), "OnTimeUp");
		}
	}
}
