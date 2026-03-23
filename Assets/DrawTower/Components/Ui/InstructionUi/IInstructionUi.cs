using System.Threading;
using Cysharp.Threading.Tasks;

namespace DrawTower.Ui
{
	public interface IInstructionUi : IBaseUi
	{
		UniTask ShowAndAnimAsync(InstructionType type, CancellationToken token);
		UniTask HideAndAnimAsync(CancellationToken token);
	}
}
