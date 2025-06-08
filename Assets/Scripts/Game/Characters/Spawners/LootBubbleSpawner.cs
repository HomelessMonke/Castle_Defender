using UnityEngine;

namespace Game.Characters.Spawners
{
    public class LootBubbleSpawner : MonoBehaviour
    {
        [SerializeField]
        ObjectsPool<LootBubble> pool;

        public void Init()
        {
            pool.Init();
        }
        
        public void Spawn(Vector3 position, int value)
        {
            LootBubble bubble = pool.Spawn(true);
            bubble.transform.position = position;
            bubble.Draw(value);
            bubble.Glide(() => OnComplete(bubble));
        }

        void OnComplete(LootBubble bubble)
        {
            pool.Despawn(bubble);
        }
    }
}