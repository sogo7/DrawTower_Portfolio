using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace DrawTower.Ui
{
	public class Bg : MonoBehaviour, IBg
	{
		[SerializeField]
		private RectTransform[] _points = new RectTransform[4]; // 0=左上,1=右上,2=左下,3=右下
		
		[SerializeField]
		private Cloud _cloudPrefab;
		
		[SerializeField]
		private FallObj _fallObjPrefab;
		
		public async UniTask BgAnimAsync(CancellationToken ct)
		{
			CreateCloud().BgAnimAsync(_points, ct).Forget();
			CreateFallObj().BgAnimAsync(_points, ct).Forget();
		}

		private Cloud CreateCloud()
		{
			return Instantiate(_cloudPrefab, transform);
		}
		
		private FallObj CreateFallObj()
		{
			return Instantiate(_fallObjPrefab, transform);
		}
	}
}
