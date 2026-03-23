using System.Threading;
using Cysharp.Threading.Tasks;

namespace DrawTower.Ui
{
	public interface IStampUi : IBaseUi
	{
		UniTask ShowAndAnimAsync(StampType type, StampSide side, CancellationToken ct);
	}
	
	public enum StampType
	{
		Greet,    // よろしく
		Nice,     // ナイス
		OneMore,  // もう一回
		NoGood,   // あかーん
		Sorry     // ごめん
	}
	
	public enum StampSide
	{
		My,    		// 自分
		Opponent    // 対戦相手
	}
}