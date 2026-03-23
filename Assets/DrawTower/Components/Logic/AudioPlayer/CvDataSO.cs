using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "CvDataSO", menuName = "Scriptable Objects/CvDataSO")]
public class CvDataSO : ScriptableObject
{
    [Header("SEデータリスト")]
    public List<CvData> cvDataList = new List<CvData>();

    /// <summary>
    /// TypeでAudioClipを取得する
    /// </summary>
    public AudioClip GetClipByType(CvType type)
    {
        return cvDataList.Find(bgm => bgm.cvType == type).audioClip;
    }

#if UNITY_EDITOR
    // インスペクタ変更時や生成時に呼ばれる
    private void OnValidate()
    {
        var enumValues = System.Enum.GetValues(typeof(CvType)).Cast<CvType>().ToList();

        // 重複を排除して最新の1つだけ残す
        cvDataList = cvDataList
            .GroupBy(b => b.cvType)
            .Select(g => g.First()) // 先頭の要素だけ残す
            .ToList();

        // 足りない分を追加
        foreach (var value in enumValues)
        {
            if (!cvDataList.Exists(b => b.cvType == value))
            {
                cvDataList.Add(new CvData { cvType = value });
            }
        }

        // 余分なものを削除（enumが減った場合用）
        cvDataList.RemoveAll(b => !enumValues.Contains(b.cvType));

        // enumの順番で並び替え
        cvDataList = cvDataList
            .OrderBy(b => enumValues.IndexOf(b.cvType))
            .ToList();
    }
#endif
}

[System.Serializable]
public class CvData
{
    [Header("音声種類")]
    public CvType cvType;

    [Header("音声データ")]
    public AudioClip audioClip;
}

public enum CvType
{
    Greet,    // よろしく
    Nice,     // ナイス
    OneMore,  // もう一回
    NoGood,   // あかーん
    Sorry     // ごめん
}
