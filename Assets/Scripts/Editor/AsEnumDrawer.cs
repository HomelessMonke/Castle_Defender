using System;
using System.Reflection;
using UnityEditor;
using UnityEngine;
using Utilities.Attributes;

namespace Editor
{
    [CustomPropertyDrawer(typeof(AsEnumAttribute))]
    public class AsEnumDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType != SerializedPropertyType.String)
            {
                EditorGUI.LabelField(position, label.text, "Use AsEnum with strings only");
                return;
            }

            var asEnumAttribute = (AsEnumAttribute)attribute;
            var enumType = asEnumAttribute.ValuesSource;

            // Получаем все public static поля с константами из указанного класса
            FieldInfo[] fields = enumType.GetFields(BindingFlags.Public | BindingFlags.Static);
        
            string[] options = new string[fields.Length];
            for (int i = 0; i < fields.Length; i++)
            {
                options[i] = (string)fields[i].GetValue(null);
            }

            // Находим текущий индекс выбранного значения
            int selectedIndex = Array.IndexOf(options, property.stringValue);
            if (selectedIndex < 0) selectedIndex = 0;

            // Рисуем popup с вариантами выбора
            selectedIndex = EditorGUI.Popup(position, label.text, selectedIndex, options);

            // Устанавливаем новое значение
            if (selectedIndex >= 0 && selectedIndex < options.Length)
            {
                property.stringValue = options[selectedIndex];
            }
        }
    }
}