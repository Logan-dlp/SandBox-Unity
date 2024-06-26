using System;
using System.Collections.Generic;
using System.Linq;
using UnityEditor;
using UnityEngine;

public class HelpDevToolsConfigEditor : EditorWindow
{
    private static HelpDevToolsConfig _config;
    
    public static void CallDev()
    {
        
    }
    
    public static void ShowConfig()
    {
        FetchConfig();
        string path = GetConfigPath();
        EditorGUIUtility.PingObject(AssetDatabase.LoadAssetAtPath<HelpDevToolsConfig>(path).GetInstanceID());
    }

    [MenuItem("Window/HelpDevTools Windows")]
    public static void ShowWindows()
    {
        GetWindow<HelpDevToolsConfigEditor>("Help Developper");
    }

    private void OnGUI()
    {
        if (GUILayout.Button("Call Developper"))
        {
            CallDev();
        }
    }

    [InitializeOnLoadMethod]
    private static void OnInitialize()
    {
        FetchConfig();
    }

    private static void FetchConfig()
    {
        while (true)
        {
            if (_config != null) return;
            string path = GetConfigPath();
            if (path == null)
            {
                AssetDatabase.CreateAsset(CreateInstance<HelpDevToolsConfig>(), 
                    $"Assets/HelpDevTools/{nameof(HelpDevToolsConfig)}.asset");
                continue;
            }

            _config = AssetDatabase.LoadAssetAtPath<HelpDevToolsConfig>(path);
            break;
        }
    }

    private static string GetConfigPath()
    {
        List<string> pathList = AssetDatabase.FindAssets(nameof(HelpDevToolsConfig))
            .Select(AssetDatabase.GUIDToAssetPath)
            .Where(c => c.EndsWith(".asset"))
            .ToList();
        if (pathList.Count > 1) Debug.LogWarning($"Multiple {nameof(HelpDevToolsConfig)} assets found. Delete one.");
        return pathList.FirstOrDefault();
    }
}
