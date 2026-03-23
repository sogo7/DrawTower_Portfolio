using Zenject;

namespace DrawTower.Ui
{
	public class ScoreInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container
				.Bind<IScore>()
				.FromInstance(GetComponent<IScore>())
				.AsSingle();
		}
	}
}
