using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Editor
{
	public class FastSelectionTool : ScriptableObject
	{
		[SerializeField]
		Object[] objects = Array.Empty<Object>();
		
		public Object[] Objects => objects; 
	}
}