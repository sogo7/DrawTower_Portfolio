using System;
using System.Runtime.InteropServices;
using UnityEngine;

public class KeychainManager
{
    // Keychainにデータを保存
    [DllImport("__Internal")]
    private static extern void SaveToKeychain(string key, string value);

    // Keychainからデータを取得
    [DllImport("__Internal")]
    private static extern IntPtr LoadFromKeychain(string key);

    // Keychainのデータを削除
    [DllImport("__Internal")]
    private static extern void DeleteFromKeychain(string key);

    /// <summary>
    /// Keychainにデータを保存する
    /// </summary>
    public static void Save(string key, string value)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            SaveToKeychain(key, value);
        }
        else
        {
            Debug.LogWarning("SaveToKeychain is only available on iOS.");
        }
    }

    /// <summary>
    /// Keychainからデータを取得する
    /// </summary>
    public static string Load(string key)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            IntPtr resultPtr = LoadFromKeychain(key);
            if (resultPtr == IntPtr.Zero)
            {
                return null;
            }

            string result = Marshal.PtrToStringAnsi(resultPtr);
            Marshal.FreeHGlobal(resultPtr); // メモリ解放
            return result;
        }

        Debug.LogWarning("LoadFromKeychain is only available on iOS.");
        return null;
    }

    /// <summary>
    /// Keychainのデータを削除する
    /// </summary>
    public static void Delete(string key)
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            DeleteFromKeychain(key);
        }
        else
        {
            Debug.LogWarning("DeleteFromKeychain is only available on iOS.");
        }
    }
}

