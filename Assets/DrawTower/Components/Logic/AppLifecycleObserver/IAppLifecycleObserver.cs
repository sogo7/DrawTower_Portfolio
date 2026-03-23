using R3;

namespace DrawTower.Logic
{
	public interface IAppLifecycleObserver
	{
		Observable<Unit> OnQuitAsObservable();
		Observable<bool> OnPauseAsObservable();
	}
}

