using UnityEngine;

namespace Game.Characters.Movements
{
    public interface IMovement
    {
        void MoveToTarget(Vector3 position);
    }

}