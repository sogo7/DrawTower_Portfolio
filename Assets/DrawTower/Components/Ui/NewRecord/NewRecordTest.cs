using DrawTower.Test;
using Zenject;

namespace DrawTower.Ui
{
	public class NewRecordTest : TestSceneBase
	{
		[Inject]
		private INewRecord _newRecord;
		
		private void Start()
		{	
			BindButton(0, _newRecord.Show);
			BindButton(1, _newRecord.Hide);
			BindButton(2, _newRecord.ShowAnimAsync);		
		}
	}
}
