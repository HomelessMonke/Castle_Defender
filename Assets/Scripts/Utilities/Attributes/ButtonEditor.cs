using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
namespace Utilities.Attributes
{
    [CustomEditor(typeof(MonoBehaviour), true)]
    public class ButtonEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            // Получаем все методы текущего объекта
            MonoBehaviour monoBehaviour = (MonoBehaviour)target;
            MethodInfo[] methods = monoBehaviour.GetType().GetMethods(
                BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic
            );

            // Проходим по всем методам
            foreach (MethodInfo method in methods)
            {
                // Проверяем, есть ли у метода атрибут ButtonAttribute
                ButtonAttribute buttonAttribute = (ButtonAttribute)Attribute.GetCustomAttribute(method, typeof(ButtonAttribute));
                if (buttonAttribute != null)
                {
                    // Определяем текст кнопки
                    string buttonName = string.IsNullOrEmpty(buttonAttribute.ButtonName) ? method.Name : buttonAttribute.ButtonName;

                    // Отображаем кнопку
                    if (GUILayout.Button(buttonName))
                    {
                        // Вызываем метод при нажатии на кнопку
                        method.Invoke(monoBehaviour, null);
                    }
                }
            }
        }
    }
}