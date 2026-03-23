using DrawTower.Test;
using UnityEngine;
using Zenject;

namespace DrawTower.Ui
{
	public class StampUiTest : TestSceneBase
	{
		[Inject]
		private IStampUi _stampUi;
		
		[SerializeField]
		private StampType _stampType = StampType.Greet;
		
		[SerializeField]
		private StampSide _stampSide = StampSide.My;
		
		private void Start()
		{	
			var token = GetToken();
			BindButton(0, () => _stampUi.ShowAndAnimAsync(_stampType, _stampSide, token));
		}
	}
}
