using Zenject;

namespace DrawTower.Ui
{
	public class MultiBtnInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container
				.Bind<IMultiBtn>()
				.FromInstance(GetComponent<IMultiBtn>())
				.AsSingle();
		}
	}
}
