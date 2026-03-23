using System;
using System.Collections.Generic;

namespace DrawTower.Model
{
    [Serializable]
    public class RankingData
    {
        public string name;
        public int score;
        public string rank;   
        public string playFabId;
        
        public RankingData(string name, int score, string rank, string playFabId)
        {
            this.name = name;
            this.score = score;
            this.rank = rank;
            this.playFabId = playFabId;
        }
    }
}
