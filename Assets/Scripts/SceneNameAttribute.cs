using System;
using System.Collections.Generic;
using System.IO;
using UnityEditor;
using UnityEngine;


// 自定义属性，用于标记字段 
[AttributeUsage(AttributeTargets.Field)]
public class SceneNameAttribute : PropertyAttribute
{
}

// 自定义PropertyDrawer  
[CustomPropertyDrawer(typeof(SceneNameAttribute))]
public class ScenePickerDrawer : PropertyDrawer
{
    // 使用静态列表来缓存场景名称  
    private static List<string> _cachedSceneNames = null;

    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
    {
        // 确保我们处理的是字符串类型的属性  
        if (property.propertyType != SerializedPropertyType.String)
        {
            EditorGUI.LabelField(position, "Use with string field");
            return;
        }

        // 加载场景名称（如果需要）  
        if (_cachedSceneNames == null)
        {
            _cachedSceneNames = LoadAllSceneNames();
        }

        // 显示下拉列表  
        int selectedIndex = _cachedSceneNames.IndexOf(property.stringValue);
        if (selectedIndex == -1)
        {
            selectedIndex = 0; // 如果没有找到匹配项，则默认为第一个场景  
        }

        selectedIndex = EditorGUI.Popup(position, label.text, selectedIndex, _cachedSceneNames.ToArray());

        // 更新属性值  
        property.stringValue = _cachedSceneNames[selectedIndex];
    }

    private static List<string> LoadAllSceneNames()
    {
        var sceneNames = new List<string>();

        // 这里你可以根据需要加载所有场景名称，而不仅仅是构建设置中的场景  
        // 例如，你可以遍历 Assets/Scenes 文件夹中的所有 .unity 文件  
        foreach (var scene in EditorBuildSettings.scenes)
        {
            if (scene.enabled)
            {
                sceneNames.Add(Path.GetFileNameWithoutExtension(scene.path));
            }
        }

        // 示例：额外加载 Assets/Scenes 文件夹中的场景  
        string[] sceneFiles = Directory.GetFiles("Assets/Scenes", "*.unity", SearchOption.AllDirectories);
        foreach (var filePath in sceneFiles)
        {
            if (!sceneNames.Contains(Path.GetFileNameWithoutExtension(filePath)))
            {
                sceneNames.Add(Path.GetFileNameWithoutExtension(filePath));
            }
        }

        return sceneNames;
    }
}