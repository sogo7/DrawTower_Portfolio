using Zenject;

namespace DrawTower.Ui
{
    public class DialogInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<IDialog>()
                .FromInstance(GetComponent<IDialog>())
                .AsSingle();
        }
    }
}
