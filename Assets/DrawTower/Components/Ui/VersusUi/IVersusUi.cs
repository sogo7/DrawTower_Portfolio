using System.Threading;
using Cysharp.Threading.Tasks;
using DrawTower.Model;

namespace DrawTower.Ui
{
	public interface IVersusUi : IBaseUi
	{
		void SetMyData(Player player, string name);
		void SetOpponentData(Player opponent, string name);
		UniTask ShowAndAnimAsync(CancellationToken ct);
		void Clear();
	}
}
