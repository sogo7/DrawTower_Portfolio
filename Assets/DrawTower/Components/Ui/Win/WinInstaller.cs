using Zenject;

namespace DrawTower.Ui
{
	public class WinInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container
				.Bind<IWin>()
				.FromInstance(GetComponent<IWin>())
				.AsSingle();
		}
	}
}
