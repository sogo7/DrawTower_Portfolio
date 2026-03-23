using Zenject;

namespace DrawTower.Ui
{
	public class RankingInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container
				.Bind<IRanking>()
				.FromInstance(GetComponent<IRanking>())
				.AsSingle();
		}
	}
}
