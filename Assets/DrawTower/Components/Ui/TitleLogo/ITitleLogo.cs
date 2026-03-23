using System.Threading;
using Cysharp.Threading.Tasks;

namespace DrawTower.Ui
{
	public interface ITitleLogo : IBaseUi
	{
		UniTask ShowAndAnimAsync(CancellationToken cancellationToken);
		void Clear();
	}
}
