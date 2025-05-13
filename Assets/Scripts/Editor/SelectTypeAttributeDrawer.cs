using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Runtime.Serialization;
using UnityEditor;
using UnityEngine;

/// <summary>
/// Добавь <see cref="SelectTypeAttribute"/> к полю который имеет аттрибут [SerializeReference] шобы нарисовать выпадающий список с реализациями этой абстракции
/// </summary>
/// <remarks>
/// https://forum.unity.com/threads/how-to-set-serializedproperty-managedreferencevalue-to-null.746645/
/// <br></br>
/// <br></br>
/// N.B.: ОНО НЕ УЧИТЫВАЕТ ИЕРАРХИЮ (с учетом этого сделана например иерархия <see cref="Game.Items.IGoodsHint"/> : <see cref="Game.Items.IHint"/>)
/// <br></br>
/// Можно и переделать, но не забудьте это учесть тогда
/// </remarks>
[CustomPropertyDrawer(typeof(SelectTypeAttribute))]
public sealed class SelectTypeAttributeDrawer : PropertyDrawer
{
	bool initialised = false;

	List<Type> instantiableTypes;

	public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
	{
		SelectTypeAttribute selectTypeAttribute = attribute as SelectTypeAttribute;
		
		if (!initialised)
        {
			// type is returned as "{assembly name} {type name}" from property.managedReferenceFieldTypename, needs to be "{assembly name}, {type name}" for Type.GetType()
			string[] baseTypeAndAssemblyName = property.managedReferenceFieldTypename.Split(' ');
			string baseTypeString = $"{baseTypeAndAssemblyName[1]}, {baseTypeAndAssemblyName[0]}";
			Type baseType = Type.GetType(baseTypeString);

			instantiableTypes = GetAllInstantiableTypesDerivedFrom(baseType);//
		}

        if (instantiableTypes.Count == 0)
        {
            property.managedReferenceValue = null;
            return;
        }


		var instantiableTypes2 = instantiableTypes.OrderBy(x => selectTypeAttribute.DefaultType != null && x == selectTypeAttribute.DefaultType ? 0 : 1).ToList();

        var options2 = new GUIContent[instantiableTypes2.Count];
        int selectedIndex2 = -1;
        for (int i = 0; i < instantiableTypes2.Count; i++)
        {
            var type = instantiableTypes2[i];
            var description = GetDescription(type);
            options2[i] = new GUIContent(description ?? type.Name);
            string typeAndAssemblyName = $"{type.Assembly.GetName().Name} {type.FullName}";
            if (property.managedReferenceFullTypename == typeAndAssemblyName)
            {
                selectedIndex2 = i;
            }
        }

        var popupSelectionIndex = selectedIndex2 == -1 ? 0 : selectedIndex2;
        int newSelectedIndex2 = EditorGUI.Popup(new Rect(position.x + position.width - 300, position.y, 300, EditorGUIUtility.singleLineHeight), popupSelectionIndex, options2);
        if (selectedIndex2 != newSelectedIndex2)
        {
            Undo.RegisterCompleteObjectUndo(property.serializedObject.targetObject, "selected type change");
            Type selectedType2 = instantiableTypes2[newSelectedIndex2];
            property.managedReferenceValue = FormatterServices.GetUninitializedObject(selectedType2);
        }
		
		EditorGUI.PropertyField(
			new Rect(
				position.x,
				position.y,
				position.width,
				position.height),
			property,
			label,
			true);

		initialised = true;
	}

	public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
	{
		return EditorGUI.GetPropertyHeight(property, true);
	}

	List<Type> GetAllInstantiableTypesDerivedFrom(Type targetType)
	{
		Assembly[] assemblies = AppDomain.CurrentDomain.GetAssemblies();
		List<Type> derivedTypes = new List<Type>();
		foreach (Assembly assembly in assemblies)
		{
			Type[] types = assembly.GetTypes();
			foreach (Type type in types)
			{
				if (type != targetType &&
					targetType.IsAssignableFrom(type) &&
					CheckInstantiable(type))
				{
					derivedTypes.Add(type);
				}
			}
		}

		if (CheckInstantiable(targetType))
		{
			derivedTypes.Add(targetType);
		}

		return derivedTypes;
	}

	bool CheckInstantiable(Type type)
	{
		if (typeof(UnityEngine.Component).IsAssignableFrom(type))
		{
			return false;
		}

		if (type == typeof(string) || type.IsValueType)
		{
			return true;
		}

		return !type.IsInterface &&
			!type.IsGenericTypeDefinition &&
			!type.IsAbstract &&
			type.IsVisible;
	}

	string GetDescription(Type type)
	{
		var descriptions = (DescriptionAttribute[])type.GetCustomAttributes(typeof(DescriptionAttribute), false);

		if (descriptions.Length == 0)
		{
			return null;
		}
		return descriptions[0].Description;
	}
}