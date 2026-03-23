using Cysharp.Threading.Tasks;
using DrawTower.Test;
using Zenject;
using R3;
using UnityEngine;

namespace DrawTower.Ui
{
	public class BgmToggleTest : TestSceneBase
	{
		[Inject]
		private IBgmToggle _bgmToggle;
		
		private void Start()
		{			
			BindButton(0, () => _bgmToggle.SetIsOn(true));
			BindButton(1, () => _bgmToggle.SetIsOn(false));
			BindButton(2, _bgmToggle.MoveLeftAnimAsync);
			BindButton(3, _bgmToggle.MoveCenterAnimAsync);
			BindButton(4, _bgmToggle.MoveRightAnimAsync);
			
			_bgmToggle.OnValueChangedAsObservable()
				.Subscribe(isOn => Debug.Log($"isOn: {isOn}"))
				.AddTo(this);
		}
	}
}
