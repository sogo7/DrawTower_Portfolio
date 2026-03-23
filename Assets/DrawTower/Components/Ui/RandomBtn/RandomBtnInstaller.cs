using Zenject;

namespace DrawTower.Ui
{
	public class RandomBtnInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container
				.Bind<IRandomBtn>()
				.FromInstance(GetComponent<IRandomBtn>())
				.AsSingle();
		}
	}
}
