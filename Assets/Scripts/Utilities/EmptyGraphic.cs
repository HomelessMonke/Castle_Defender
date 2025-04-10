using UnityEngine.UI;
namespace Utilities
{
    public class EmptyGraphic : Graphic {
        protected override void OnPopulateMesh(VertexHelper vh) {
            vh.Clear();
        }
    }
}