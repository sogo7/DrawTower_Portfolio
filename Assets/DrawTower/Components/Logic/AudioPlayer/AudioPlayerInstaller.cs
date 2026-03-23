using Zenject;

namespace DrawTower.Logic
{
    public class AudioPlayerInstaller : MonoInstaller
    {
        public override void InstallBindings()
        {
            Container.Bind<IAudioPlayer>()
                .FromInstance(GetComponent<IAudioPlayer>())
                .AsSingle();
        }
    }
}

