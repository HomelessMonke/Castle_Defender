using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace Utilities.Attributes
{
    // Указываем, что редактор работает для MonoBehaviour И ScriptableObject
    [CustomEditor(typeof(UnityEngine.Object), true)]
    [CanEditMultipleObjects]
    public class ButtonEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            // Отрисовываем стандартные поля
            DrawDefaultInspector();

            // Получаем текущий объект (работает для любого UnityEngine.Object)
            UnityEngine.Object targetObject = target;

            // Пропускаем объекты без методов (например, скрипты по умолчанию)
            if (targetObject.GetType().Namespace == "UnityEngine") return;

            // Получаем все методы
            MethodInfo[] methods = targetObject.GetType().GetMethods(
                BindingFlags.Instance | BindingFlags.Static | 
                BindingFlags.Public | BindingFlags.NonPublic
            );

            // Отрисовываем кнопки для методов с атрибутом
            foreach (MethodInfo method in methods)
            {
                ButtonAttribute buttonAttribute = method.GetCustomAttribute<ButtonAttribute>();
                if (buttonAttribute != null && IsValidMethod(method))
                {
                    string buttonName = string.IsNullOrEmpty(buttonAttribute.ButtonName) 
                        ? ObjectNames.NicifyVariableName(method.Name) 
                        : buttonAttribute.ButtonName;

                    EditorGUI.BeginDisabledGroup(!Application.isPlaying && buttonAttribute.RuntimeOnly);
                    if (GUILayout.Button(buttonName))
                    {
                        foreach (var t in targets) // Поддержка multi-select
                        {
                            method.Invoke(t, null);
                        }
                    }
                    EditorGUI.EndDisabledGroup();
                }
            }
        }

        private bool IsValidMethod(MethodInfo method)
        {
            // Проверяем, что метод не имеет параметров
            return method.GetParameters().Length == 0;
        }
    }
}