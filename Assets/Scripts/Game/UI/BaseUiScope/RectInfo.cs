using UnityEngine;

namespace Game.UI.BaseUiScope
{
    public struct RectInfo
    {
        Vector2 pos;
        bool enable;
        
        public bool Enable => enable;
        public Vector2 Position => pos;
        
        public RectInfo(BaseUiItem item)
        {
            pos = item.RectTransform.position;
            enable = item.IsEnabled;
        }
    }
}