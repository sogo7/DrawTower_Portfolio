using Zenject;

namespace DrawTower.Ui
{
	public class InstructionUiInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container
				.Bind<IInstructionUi>()
				.FromInstance(GetComponent<IInstructionUi>())
				.AsSingle();
		}
	}
}
