using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "BgmDataSO", menuName = "Scriptable Objects/BgmDataSO")]
public class BgmDataSO : ScriptableObject
{
    [Header("BGMデータリスト")]
    public List<BgmData> bgmDataList = new List<BgmData>();

    /// <summary>
    /// TypeでAudioClipを取得する
    /// </summary>
    public AudioClip GetClipByType(BgmType type)
    {
        return bgmDataList.Find(bgm => bgm.bgmType == type).audioClip;
    }

#if UNITY_EDITOR
    // インスペクタ変更時や生成時に呼ばれる
    private void OnValidate()
    {
        var enumValues = System.Enum.GetValues(typeof(BgmType)).Cast<BgmType>().ToList();

        // 重複を排除して最新の1つだけ残す
        bgmDataList = bgmDataList
            .GroupBy(b => b.bgmType)
            .Select(g => g.First()) // 先頭の要素だけ残す
            .ToList();

        // 足りない分を追加
        foreach (var value in enumValues)
        {
            if (!bgmDataList.Exists(b => b.bgmType == value))
            {
                bgmDataList.Add(new BgmData { bgmType = value });
            }
        }

        // 余分なものを削除（enumが減った場合用）
        bgmDataList.RemoveAll(b => !enumValues.Contains(b.bgmType));

        // enumの順番で並び替え
        bgmDataList = bgmDataList
            .OrderBy(b => enumValues.IndexOf(b.bgmType))
            .ToList();
    }
#endif
}

[System.Serializable]
public class BgmData
{
    [Header("音声種類")]
    public BgmType bgmType;

    [Header("音声データ")]
    public AudioClip audioClip;
}

public enum BgmType
{
    Title,
    Home,
    Game,
    Result
}
