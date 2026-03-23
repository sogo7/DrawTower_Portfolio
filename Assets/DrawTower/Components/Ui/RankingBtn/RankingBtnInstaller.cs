using Zenject;

namespace DrawTower.Ui
{
	public class RankingBtnInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container
				.Bind<IRankingBtn>()
				.FromInstance(GetComponent<IRankingBtn>())
				.AsSingle();
		}
	}
}
