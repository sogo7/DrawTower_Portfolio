using Zenject;

namespace DrawTower.Ui
{
    public class ProgressInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<IProgress>()
                .FromInstance(GetComponent<IProgress>())
                .AsSingle();
        }
    }
}
