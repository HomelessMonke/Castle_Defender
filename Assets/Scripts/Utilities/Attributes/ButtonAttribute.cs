using System;

namespace Utilities.Attributes
{
    [AttributeUsage(AttributeTargets.Method)]
    public class ButtonAttribute : Attribute
    {
        public string ButtonName { get; }
        public bool RuntimeOnly { get; }

        public ButtonAttribute(string buttonName = null, bool runtimeOnly = false)
        {
            ButtonName = buttonName;
            RuntimeOnly = runtimeOnly;
        }
    }
}