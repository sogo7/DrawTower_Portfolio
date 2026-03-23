using Zenject;

namespace DrawTower.Ui
{
	public class TapToNextBtnInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container
				.Bind<ITapToNextBtn>()
				.FromInstance(GetComponent<ITapToNextBtn>())
				.AsSingle();
		}
	}
}
