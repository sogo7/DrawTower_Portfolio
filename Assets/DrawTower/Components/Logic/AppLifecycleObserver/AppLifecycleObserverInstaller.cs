using Zenject;

namespace DrawTower.Logic
{
    public class AppLifecycleObserverInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IAppLifecycleObserver>()
                .FromInstance(GetComponent<IAppLifecycleObserver>())
                .AsSingle();
        }
    }
}

