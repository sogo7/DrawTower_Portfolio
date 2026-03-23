using Zenject;

namespace DrawTower.Ui
{
	public class NewRecordInstaller : MonoInstaller
	{
		public override void InstallBindings()
		{
			Container
				.Bind<INewRecord>()
				.FromInstance(GetComponent<INewRecord>())
				.AsSingle();
		}
	}
}
