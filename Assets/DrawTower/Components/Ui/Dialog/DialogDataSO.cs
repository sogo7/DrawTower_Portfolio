using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[CreateAssetMenu(fileName = "DialogDataSO", menuName = "Scriptable Objects/DialogDataSO")]
public class DialogDataSO : ScriptableObject
{
    [Header("Dialogデータリスト")]
    public List<DialogData> dialogDataList = new List<DialogData>();

    /// <summary>
    /// TypeでDialogDataを取得する
    /// </summary>
    public DialogData GetDialogDataByType(DialogType type)
    {
        return dialogDataList.Find(data => data.dialogType == type);
    }

#if UNITY_EDITOR
    // インスペクタ変更時や生成時に呼ばれる
    private void OnValidate()
    {
        var enumValues = System.Enum.GetValues(typeof(DialogType)).Cast<DialogType>().ToList();

        // 重複を排除して最新の1つだけ残す
        dialogDataList = dialogDataList
            .GroupBy(b => b.dialogType)
            .Select(g => g.First()) // 先頭の要素だけ残す
            .ToList();

        // 足りない分を追加
        foreach (var value in enumValues)
        {
            if (!dialogDataList.Exists(b => b.dialogType == value))
            {
                dialogDataList.Add(new DialogData { dialogType = value });
            }
        }

        // 余分なものを削除（enumが減った場合用）
        dialogDataList.RemoveAll(b => !enumValues.Contains(b.dialogType));

        // enumの順番で並び替え
        dialogDataList = dialogDataList
            .OrderBy(b => enumValues.IndexOf(b.dialogType))
            .ToList();
    }
#endif
}

[System.Serializable]
public class DialogData
{
    [Header("ダイアログの用途")]
    public DialogType dialogType;

    [Header("ダイアログの操作")]
    public DialogMode dialogMode;
    
    [Header("表示する文字")]
    public string text;
    
    [Header("アクションボタンの文字")]
    public string actionBtnText;
}

public enum DialogType
{
    ServiceError,
    OpponentLeft,
    Disconnected,
    BackToHome,
    Replay,
    Inactivity,
    AppUpdate
}

public enum DialogMode
{
    Close,
    YesNo,
    Action
}
