using Cysharp.Threading.Tasks;
using DrawTower.Test;
using Zenject;
using R3;
using UnityEngine;

namespace DrawTower.Ui
{
	public class VibrationToggleTest : TestSceneBase
	{
		[Inject]
		private IVibrationToggle _vibrationToggle;
		
		private void Start()
		{			
			BindButton(0, () => _vibrationToggle.SetIsOn(true));
			BindButton(1, () => _vibrationToggle.SetIsOn(false));
			BindButton(2, _vibrationToggle.MoveLeftAnimAsync);
			BindButton(3, _vibrationToggle.MoveCenterAnimAsync);
			BindButton(4, _vibrationToggle.MoveRightAnimAsync);
			
			_vibrationToggle.OnValueChangedAsObservable()
				.Subscribe(isOn => Debug.Log($"isOn: {isOn}"))
				.AddTo(this);
		}
	}
}
