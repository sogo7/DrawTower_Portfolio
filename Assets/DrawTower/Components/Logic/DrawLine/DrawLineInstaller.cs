using Zenject;

namespace DrawTower.Logic
{
	public class DrawLineInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container
				.Bind<IDrawLine>()
				.FromInstance(GetComponent<IDrawLine>())
				.AsSingle();
		}
	}
}
