using DrawTower.Test;
using Zenject;
using UnityEngine;

namespace DrawTower.Ui
{
	public class InstructionUiTest : TestSceneBase
	{
		[Inject]
		private IInstructionUi _instructionUi;

		[SerializeField]
		private InstructionType _instructionType = InstructionType.OfflineDrawShape;
		
		private void Start()
		{	
			var token = GetToken();
			BindButton(0, () => _instructionUi.ShowAndAnimAsync(_instructionType, token));
			BindButton(1, _instructionUi.HideAndAnimAsync);
		}
	}
}
