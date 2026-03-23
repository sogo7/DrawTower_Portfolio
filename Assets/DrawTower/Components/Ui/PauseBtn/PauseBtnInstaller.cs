using Zenject;

namespace DrawTower.Ui
{
	public class PauseBtnInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container
				.Bind<IPauseBtn>()
				.FromInstance(GetComponent<IPauseBtn>())
				.AsSingle();
		}
	}
}
