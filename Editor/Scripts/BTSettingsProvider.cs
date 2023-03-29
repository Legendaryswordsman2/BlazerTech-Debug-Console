using UnityEngine;
using UnityEditor;
using UnityEngine.UIElements;
using LootLocker;
using System.Drawing.Printing;

public class BTSettingsProvider : SettingsProvider
{
    private static BTSettingsSO settings;
    private SerializedObject m_CustomSettings;
    internal static SerializedObject GetSerializedSettings()
    {
        if (settings == null)
        {
            settings = BTSettingsSO.Get();
        }
        return new SerializedObject(settings);
    }

    const string k_additiveOptionPref = "ShowAdditiveSceneOption";

    public static bool AdditiveOptionEnabled
    {
        get { return EditorPrefs.GetBool(k_additiveOptionPref, true); }
        set { EditorPrefs.SetBool(k_additiveOptionPref, value); }
    }

    public BTSettingsProvider(string path, SettingsScope scope)
        : base(path, scope)
    { }

    public override void OnGUI(string searchContext)
    {
        //base.OnGUI(searchContext);

        m_CustomSettings.Update();

        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(m_CustomSettings.FindProperty("logCap"));
        if (EditorGUI.EndChangeCheck())
        {
            settings.logCap = m_CustomSettings.FindProperty("logCap").intValue;
        }

        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(m_CustomSettings.FindProperty("fontSize"));
        if (EditorGUI.EndChangeCheck())
        {
            settings.fontSize = m_CustomSettings.FindProperty("fontSize").floatValue;
        }

        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(m_CustomSettings.FindProperty("logFileCap"));
        if (EditorGUI.EndChangeCheck())
        {
            settings.logFileCap = m_CustomSettings.FindProperty("logFileCap").intValue;
        }

        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(m_CustomSettings.FindProperty("backgroundColor"));
        if (EditorGUI.EndChangeCheck())
        {
            settings.backgroundColor = m_CustomSettings.FindProperty("backgroundColor").colorValue;
        }

        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(m_CustomSettings.FindProperty("clearConsoleOnSceneChange"));
        if (EditorGUI.EndChangeCheck())
        {
            settings.clearConsoleOnSceneChange = m_CustomSettings.FindProperty("clearConsoleOnSceneChange").boolValue;
        }

        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(m_CustomSettings.FindProperty("activeInputSystem"));
        if (EditorGUI.EndChangeCheck())
        {
            settings.activeInputSystem = (ActiveInputSystem)m_CustomSettings.FindProperty("activeInputSystem").enumValueFlag;
        }

        EditorGUI.BeginChangeCheck();
        EditorGUILayout.PropertyField(m_CustomSettings.FindProperty("toggleConsoleKey"));
        if (EditorGUI.EndChangeCheck())
        {
            settings.toggleConsoleKey = (KeyCode)m_CustomSettings.FindProperty("toggleConsoleKey").enumValueFlag;
        }

        m_CustomSettings.ApplyModifiedProperties();

        ////GUILayout.Space(20f);

        //bool ClearConsoleOnSceneChangeToggle = EditorGUILayout.Toggle("Clear Console On Scene Change", settings.clearConsoleOnSceneChange);
        //if(settings.clearConsoleOnSceneChange != ClearConsoleOnSceneChangeToggle)
        //{
        //    settings.clearConsoleOnSceneChange = ClearConsoleOnSceneChangeToggle;
        //}

        //bool enabled = AdditiveOptionEnabled;
        //bool value = EditorGUILayout.Toggle("Additive Option", enabled, GUILayout.Width(200f));
        //if(enabled != value)
        //    AdditiveOptionEnabled = value;
    }


    public override void OnActivate(string searchContext, VisualElement rootElement)
    {
        Debug.Log("Getting settings instance");
        settings = BTSettingsSO.Get();

        m_CustomSettings = GetSerializedSettings();

        //bool ClearConsoleOnSceneChangeToggle = EditorGUILayout.Toggle("Clear Console On Scene Change", settings.clearConsoleOnSceneChange);
        //if (ClearConsoleOnSceneChangeToggle != settings.clearConsoleOnSceneChange)
        //{
        //    ClearConsoleOnSceneChangeToggle = settings.clearConsoleOnSceneChange;
        //}
    }

    [SettingsProvider]
    public static SettingsProvider CreateSettingsProvider()
    {
        return new BTSettingsProvider("Project/BlazerTech Debug Console", SettingsScope.Project);
    }
}
