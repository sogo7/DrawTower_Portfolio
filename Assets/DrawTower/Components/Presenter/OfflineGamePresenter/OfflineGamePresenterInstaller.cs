using Zenject;

namespace DrawTower.Presenter
{
	public class OfflineGamePresenterInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container.BindInterfacesAndSelfTo<OfflineGamePresenter>().AsSingle();
		}
	}
}
