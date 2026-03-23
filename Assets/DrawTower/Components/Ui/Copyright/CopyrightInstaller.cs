using Zenject;

namespace DrawTower.Ui
{
    public class CopyrightInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container
                .Bind<ICopyright>()
                .FromInstance(GetComponent<ICopyright>())
                .AsSingle();
        }
    }
}
