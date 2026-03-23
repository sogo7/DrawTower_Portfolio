using System.Collections.Generic;
using DrawTower.Model;
using DrawTower.Test;
using Zenject;

namespace DrawTower.Ui
{
	public class RankingTest : TestSceneBase
	{
		[Inject]
		private IRanking _ranking;
		
		private void Start()
		{
			var rankingPlayers = new List<RankingData>()
			{
				new RankingData("1位くん", 100000, "1", "7FEA92B8D464C865"),
				new RankingData("2位くん", 80000, "2", "0"),
				new RankingData("3位くん", 50000, "3", "0"),
			};
			
			var player = new Player
			{
				colorId = "0",		
			};
			
			BindButton(0, _ranking.Show);
			BindButton(1, _ranking.Hide);
			BindButton(2, () => _ranking.SetHighScoreRanking(rankingPlayers));
			BindButton(3, () => _ranking.UpdateHighScoreRanking("portfolio-player", "ランキング君", player));
			BindButton(4, _ranking.MoveLeftAnimAsync);
			BindButton(5, _ranking.MoveCenterAnimAsync);
			BindButton(6, _ranking.MoveRightAnimAsync);
		}
	}
}
