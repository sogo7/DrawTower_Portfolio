using Zenject;

namespace DrawTower.Ui
{
	public class PasscodeAreaInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container
				.Bind<IPasscodeArea>()
				.FromInstance(GetComponent<IPasscodeArea>())
				.AsSingle();
		}
	}
}
