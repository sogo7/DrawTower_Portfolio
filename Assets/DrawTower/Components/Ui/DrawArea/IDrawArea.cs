using System.Threading;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace DrawTower.Ui
{
	public interface IDrawArea : IBaseUi
	{
		UniTask TopAnimAsync(CancellationToken ct);
		UniTask CenterAnimAsync(CancellationToken ct);
		RectTransform GetArea();
	}
}
