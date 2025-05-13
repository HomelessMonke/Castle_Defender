using UnityEngine;

namespace Game.Characters.Parameters
{
    [CreateAssetMenu(menuName = "Characters/Parameters/Castle")]
    public class CastleParameters : ScriptableObject
    {
        [SerializeField]
        int hp = 150;
        
        public int HP => hp;
    }
}