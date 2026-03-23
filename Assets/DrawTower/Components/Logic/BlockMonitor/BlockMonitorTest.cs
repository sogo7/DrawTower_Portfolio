using DrawTower.Test;
using UnityEngine;
using Zenject;

namespace DrawTower.Logic
{
	public class BlockMonitorTest : TestSceneBase
	{
		[Inject]
		private IBlockMonitor _blockMonitor;
		
		[SerializeField]
		private GameObject _testObj;

		private void Start()
		{			
			var token = GetToken();
			
			BindButton(0, () => 
			{
			    _blockMonitor.TrackBlockAsync(_testObj.transform, token);
			});
		}
	}
}

