using Game.Grades.CastleGrades;
using UnityEngine;

namespace Game.Characters.Parameters
{
    [CreateAssetMenu(menuName = "Characters/Parameters/Castle")]
    public class CastleParameters : ScriptableObject
    {
        [SerializeField]
        CastleHealthGrades hpGrades;
        
        public float HP => hpGrades.HP;
    }
}