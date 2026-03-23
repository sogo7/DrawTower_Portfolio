using UnityEngine;
using R3;

namespace DrawTower.Logic
{
    public class AppLifecycleObserver : MonoBehaviour, IAppLifecycleObserver
    {
        private readonly Subject<Unit> _onQuit = new();
        public Observable<Unit> OnQuitAsObservable() => _onQuit;

        private readonly Subject<bool> _onPause = new();
        public Observable<bool> OnPauseAsObservable() => _onPause;

        private void OnApplicationQuit()
        {
            _onQuit.OnNext(Unit.Default);
        }

        private void OnApplicationPause(bool pause)
        {
            _onPause.OnNext(pause);
        }
    }
}
