using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;

namespace Editor
{
    public class FastSelectionWindow : EditorWindow
    {
        FastSelectionTool toolCache;

        [MenuItem("Tools/Быстрое Выделение")]
        public static void Init()
        {
            FastSelectionWindow window = GetWindow<FastSelectionWindow>();
            window.titleContent.text = "Быстрое выделение";
        }

        void OnGUI()
        {
            var tool = GetTool();

            foreach (var obj in tool.Objects)
            {
                if (obj)
                {
                    if (obj is SceneAsset scene)
                    {
                        Color c = GUI.color;
                        GUI.color = Color.gray;
                        if (GUILayout.Button(obj.name, GUILayout.ExpandWidth(true)))
                        {
                            var pathToScene = AssetDatabase.GetAssetPath(scene);
                            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                                EditorSceneManager.OpenScene(pathToScene);
                        }

                        GUI.color = c;
                    }
                    else
                    {
                        if (GUILayout.Button(obj.name, GUILayout.ExpandWidth(true)))
                            //Selection.activeObject = obj;
                            AssetDatabase.OpenAsset(obj);
                    }
                }
            }

            GUILayout.Space(10);
            if (GUILayout.Button("Tool", GUILayout.ExpandWidth(true)))
                Selection.activeObject = tool;
        }

        FastSelectionTool GetTool()
        {
            if (toolCache)
                return toolCache;
			
            toolCache = Load();

            if (!toolCache)
            {
                FastSelectionTool asset = CreateInstance<FastSelectionTool>();
                AssetDatabase.CreateAsset(asset, "Assets/Editor/FastSelectionTool.asset");
                AssetDatabase.SaveAssets();

                toolCache = Load();
            }
			
            return toolCache;
        }

        FastSelectionTool Load()
        {
            return AssetDatabase.LoadAssetAtPath<FastSelectionTool>("Assets/Editor/FastSelectionTool.asset");
        }
    }
}