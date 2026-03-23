using Zenject;

namespace DrawTower.Ui
{
	public class CopyrightBtnInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container
				.Bind<ICopyrightBtn>()
				.FromInstance(GetComponent<ICopyrightBtn>())
				.AsSingle();
		}
	}
}
