using Cysharp.Threading.Tasks;
using DrawTower.Test;
using Zenject;
using R3;
using UnityEngine;

namespace DrawTower.Ui
{
	public class SeToggleTest : TestSceneBase
	{
		[Inject]
		private ISeToggle _seToggle;
		
		private void Start()
		{			
			BindButton(0, () => _seToggle.SetIsOn(true));
			BindButton(1, () => _seToggle.SetIsOn(false));
			BindButton(2, _seToggle.MoveLeftAnimAsync);
			BindButton(3, _seToggle.MoveCenterAnimAsync);
			BindButton(4, _seToggle.MoveRightAnimAsync);
			
			_seToggle.OnValueChangedAsObservable()
				.Subscribe(isOn => Debug.Log($"isOn: {isOn}"))
				.AddTo(this);
		}
	}
}
