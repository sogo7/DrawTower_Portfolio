using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "InstructionDataSO", menuName = "Scriptable Objects/InstructionDataSO")]
public class InstructionDataSO : ScriptableObject
{
    [Header("Instructionデータリスト")]
    public List<InstructionData> instructionDataList = new List<InstructionData>();

    /// <summary>
    /// TypeでInstructionDataを取得する
    /// </summary>
    public InstructionData GetInstructionDataByType(InstructionType type)
    {
        return instructionDataList.Find(data => data.instructionType == type);
    }

#if UNITY_EDITOR
    // インスペクタ変更時や生成時に呼ばれる
    private void OnValidate()
    {
        var enumValues = System.Enum.GetValues(typeof(InstructionType)).Cast<InstructionType>().ToList();

        // 重複を排除して最新の1つだけ残す
        instructionDataList = instructionDataList
            .GroupBy(b => b.instructionType)
            .Select(g => g.First()) // 先頭の要素だけ残す
            .ToList();

        // 足りない分を追加
        foreach (var value in enumValues)
        {
            if (!instructionDataList.Exists(b => b.instructionType == value))
            {
                instructionDataList.Add(new InstructionData { instructionType = value });
            }
        }

        // 余分なものを削除（enumが減った場合用）
        instructionDataList.RemoveAll(b => !enumValues.Contains(b.instructionType));

        // enumの順番で並び替え
        instructionDataList = instructionDataList
            .OrderBy(b => enumValues.IndexOf(b.instructionType))
            .ToList();
    }
#endif
}

[System.Serializable]
public class InstructionData
{
    [Header("ダイアログの用途")]
    public InstructionType instructionType;
    
    [Header("表示する文字")]
    public string text;

    [Header("文字の大きさ")]
    public float fontSize = 80f;
}

public enum InstructionType
{
    OfflineDrawShape,
    OfflinePlaceShape,
    OnlineMatchingNotice,
    OnlineMyTurnDraw,
    OnlineMyTurnPlace,
    OnlineOpponentTurn,
}
