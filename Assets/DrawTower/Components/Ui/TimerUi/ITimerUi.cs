using Cysharp.Threading.Tasks;
using R3;

namespace DrawTower.Ui
{
	public interface ITimerUi : IBaseUi
	{
		UniTask StartTimerAsync(int seconds);
		void Clear();
		Observable<Unit> OnTimeUp();
	}
}
