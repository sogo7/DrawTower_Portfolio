using Zenject;

namespace DrawTower.Ui
{
	public class RotateBtnInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container
				.Bind<IRotateBtn>()
				.FromInstance(GetComponent<IRotateBtn>())
				.AsSingle();
		}
	}
}
