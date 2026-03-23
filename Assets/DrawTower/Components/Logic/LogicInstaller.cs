using Zenject;

namespace DrawTower.Logic
{
    public class LogicInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<SceneLoader>().AsSingle();
            Container.BindInterfacesAndSelfTo<HapticPlayer>().AsSingle();
            Container.BindInterfacesAndSelfTo<ExternalNavigator>().AsSingle();
        }
    }
}
