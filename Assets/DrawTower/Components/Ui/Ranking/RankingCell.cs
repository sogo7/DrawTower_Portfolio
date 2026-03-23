using DrawTower.Model;
using TMPro;
using UnityEngine;

namespace DrawTower.Ui
{
	public class RankingCell : MonoBehaviour
	{
		[SerializeField]
		private TextMeshProUGUI _rank;
		
		[SerializeField]
		private TextMeshProUGUI _name;
		
		[SerializeField]
		private TextMeshProUGUI _score;
		
		public void SetRankingUser(RankingData rankingData, Player player = null)
		{
		    _rank.text = rankingData.rank;
		    _name.text = rankingData.name;
		    float lastHeightMeters = rankingData.score / 100f;
		    _score.text = $"{lastHeightMeters:F1}m";
		    
		    var color = player != null ? player.GetColor() : Color.black;
		    
		    _rank.color = color;
		    _name.color = color;
			_score.color = color;
		}
		
		public void UpdateRankingUser(string name, Player player)
		{
			_name.text = name;
			
		    var color = player != null ? player.GetColor() : Color.black;
		    
		    _rank.color = color;
		    _name.color = color;
			_score.color = color;
		}
	}
}
