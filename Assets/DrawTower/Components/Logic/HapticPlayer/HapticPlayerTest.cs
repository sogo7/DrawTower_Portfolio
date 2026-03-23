using DrawTower.Test;
using UnityEngine;
using Zenject;

namespace DrawTower.Logic
{
	public class HapticPlayerTest : TestSceneBase
	{
		[Inject]
		private IHapticPlayer _hapticPlayer;
		
		private void Start()
		{
			BindButton(0, _hapticPlayer.Play);
			BindButton(1, () => _hapticPlayer.SetEnabled(true));
			BindButton(2, () => _hapticPlayer.SetEnabled(false));
			BindButton(3, () => Debug.Log($"IsEnabled: {_hapticPlayer.IsEnabled()}"));
		}
	}
}

