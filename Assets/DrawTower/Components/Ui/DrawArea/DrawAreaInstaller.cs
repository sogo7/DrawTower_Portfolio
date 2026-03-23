using Zenject;

namespace DrawTower.Ui
{
	public class DrawAreaInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container
				.Bind<IDrawArea>()
				.FromInstance(GetComponent<IDrawArea>())
				.AsSingle();
		}
	}
}
