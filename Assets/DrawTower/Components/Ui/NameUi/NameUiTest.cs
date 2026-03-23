using Cysharp.Threading.Tasks;
using DrawTower.Test;
using UnityEngine;
using Zenject;
using R3;

namespace DrawTower.Ui
{
	public class NameUiTest : TestSceneBase
	{
		[Inject]
		private INameUi _nameUi;
		
		private void Start()
		{						
			BindButton(0, _nameUi.ShowAndAnimAsync);
			BindButton(1, _nameUi.HideAndAnimAsync);
			BindButton(2, () => Debug.Log($"GetName: {_nameUi.GetName()}"));
			BindButton(3, () => Debug.Log($"IsNameLengthInvalid: {_nameUi.IsNameLengthInvalid(_nameUi.GetName())}"));
			BindButton(4, _nameUi.ShowNgWordAlert);
			BindButton(5, () => _nameUi.SetEditMode(true));
			BindButton(6, () => _nameUi.SetEditMode(false));
			
			_nameUi.OnClickApplyBtnAsObservable()
				.Subscribe(name => Debug.Log($"OnClickApplyBtnAsObservable: {name}"))
				.AddTo(this);
		}
	}
}
