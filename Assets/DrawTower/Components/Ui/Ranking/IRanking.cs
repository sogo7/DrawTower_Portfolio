using System.Collections.Generic;
using System.Threading;
using Cysharp.Threading.Tasks;
using DrawTower.Model;

namespace DrawTower.Ui
{
	public interface IRanking : IBaseUi
	{
		void SetHighScoreRanking(List<RankingData> rankingDatas);
		void UpdateHighScoreRanking(string playFabId, string name, Player player);
		UniTask MoveLeftAnimAsync(CancellationToken ct);
		UniTask MoveCenterAnimAsync(CancellationToken ct);
		UniTask MoveRightAnimAsync(CancellationToken ct);
	}
}
