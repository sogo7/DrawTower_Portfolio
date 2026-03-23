using DrawTower.Test;
using Zenject;
using UnityEngine;

namespace DrawTower.Ui
{
	public class ScoreTest : TestSceneBase
	{
		[Inject]
		private IScore _score;
		
		[SerializeField]
		private float _duration = 2f;
		
		private float _count = 10f;
		
		private void Start()
		{
			var token = GetToken();
			BindButton(0, _score.ShowAndAnimAsync);
			BindButton(1, _score.HideAndAnimAsync);
			BindButton(2, () => _score.UpdateTextAsync(_count++, _duration, token));
		}
	}
}
