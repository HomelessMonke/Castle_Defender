using System;
using PropertyAttribute = NUnit.Framework.PropertyAttribute;

namespace Utilities.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ButtonAttribute : PropertyAttribute
    {
        public string ButtonName { get; private set; }

        public ButtonAttribute(string buttonName = null)
        {
            ButtonName = buttonName;
        }
    }
}