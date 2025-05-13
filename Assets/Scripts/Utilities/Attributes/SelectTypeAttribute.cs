using System;
using UnityEngine;

/// <summary>
/// Позволяет сделать выпадающий список для поля с типом интерфейса и аттрибутом SerializeReference 
/// </summary>
public sealed class SelectTypeAttribute : PropertyAttribute
{
	public Type DefaultType { get; set; }
	public SelectTypeAttribute(Type defaultType = null) : base()
	{
		DefaultType = defaultType;
	}
}