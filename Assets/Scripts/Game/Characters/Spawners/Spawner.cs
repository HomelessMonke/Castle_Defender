using UnityEngine;
namespace Game.Characters.Spawners
{
    public abstract class CharacterSpawner: MonoBehaviour
    {
        public abstract void Init();
        public abstract Character[] Spawn(CharacterParameters parameters, int amount);
    }
}