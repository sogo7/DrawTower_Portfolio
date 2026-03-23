using Zenject;

namespace DrawTower.Ui
{
	public class PasscodeBtnInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container
				.Bind<IPasscodeBtn>()
				.FromInstance(GetComponent<IPasscodeBtn>())
				.AsSingle();
		}
	}
}
