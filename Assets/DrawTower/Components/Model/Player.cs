using System;
using System.Collections.Generic;
using UnityEngine;

namespace DrawTower.Model
{
    [Serializable]
    public class Player
    {
        public string colorId = "0";
        public string bgm = "on";
        public string se = "on";
        public string vibration = "on";

        public Dictionary<string, string> GetData()
        {			
            var data = new Dictionary<string, string>()
            {
                {"colorId", colorId},
                {"bgm", bgm},
                {"se", se},
                {"vibration", vibration},
            };
            return data;
        }
        
        /// <summary>
        /// color_id に対応した Color を返す
        /// </summary>
        public Color GetColor()
        {
            return colorId switch
            {
                "0" => new Color(0.93f, 0.27f, 0.27f), // 赤 (#ED4545)
                "1" => new Color(0.27f, 0.60f, 0.93f), // 青 (#4599ED)
                "2" => new Color(0.39f, 0.80f, 0.40f), // 緑 (#63CC66)
                "3" => new Color(0.98f, 0.85f, 0.33f), // 黄 (#FAD856)
                "4" => new Color(0.95f, 0.58f, 0.24f), // オレンジ (#F2953C)
                _ => new Color(0.95f, 0.58f, 0.24f)
            };
        }
    }
}

