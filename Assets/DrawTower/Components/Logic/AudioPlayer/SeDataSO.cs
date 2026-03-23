using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "SeDataSO", menuName = "Scriptable Objects/SeDataSO")]
public class SeDataSO : ScriptableObject
{
    [Header("SEデータリスト")]
    public List<SeData> seDataList = new List<SeData>();

    /// <summary>
    /// TypeでAudioClipを取得する
    /// </summary>
    public AudioClip GetClipByType(SeType type)
    {
        return seDataList.Find(bgm => bgm.seType == type).audioClip;
    }

#if UNITY_EDITOR
    // インスペクタ変更時や生成時に呼ばれる
    private void OnValidate()
    {
        var enumValues = System.Enum.GetValues(typeof(SeType)).Cast<SeType>().ToList();

        // 重複を排除して最新の1つだけ残す
        seDataList = seDataList
            .GroupBy(b => b.seType)
            .Select(g => g.First()) // 先頭の要素だけ残す
            .ToList();

        // 足りない分を追加
        foreach (var value in enumValues)
        {
            if (!seDataList.Exists(b => b.seType == value))
            {
                seDataList.Add(new SeData { seType = value });
            }
        }

        // 余分なものを削除（enumが減った場合用）
        seDataList.RemoveAll(b => !enumValues.Contains(b.seType));

        // enumの順番で並び替え
        seDataList = seDataList
            .OrderBy(b => enumValues.IndexOf(b.seType))
            .ToList();
    }
#endif
}

[System.Serializable]
public class SeData
{
    [Header("音声種類")]
    public SeType seType;

    [Header("音声データ")]
    public AudioClip audioClip;
}

public enum SeType
{
    Button,     // ボタン
    Countdown,  // カウントダウン
    Start,      // スタート音（ゲーム開始）
    Draw,       // 線を書く
    Spawn,      // ブロックの生成
    Drop,       // ブロックの落下
    Finish,     // フィニッシュ（ゲーム終了）
    Score,      // スコア表示
    Win,      // 勝利
    Lose,      // 敗北
}
