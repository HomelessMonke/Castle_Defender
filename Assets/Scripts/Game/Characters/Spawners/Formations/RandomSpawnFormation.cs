using System;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Characters.Spawners.Formations
{
    [Serializable, Description("Неупорядоченное построение")]
    public class RandomSpawnFormation: ISpawnFormation
    {
        [SerializeField, Range(0.25f, 1)]
        float maxOffsetX;
        
        [SerializeField, Range(0.25f, 1)]
        float maxOffsetY;

        public bool SyncMovement => false;
        
        public RandomSpawnFormation()
        {
            maxOffsetX = maxOffsetY = 0.25f;
        }

        public Vector2[] GetSpawnPoints(int[] charactersInLines, Transform transform)
        {
            Vector2 startPosition = transform.position; 
            var count = charactersInLines.Sum(x => x);
            Vector2[] positions = new Vector2[count];

            int posIndex = 0;
            for (int i = 0; i < charactersInLines.Length; i++)
            {
                var positionsCount = charactersInLines[i];
                var yOffset = maxOffsetY * ((float)(positionsCount-1) / 2);
                var linePos = startPosition + new Vector2(i * maxOffsetX, yOffset);
                for (int j = 0; j < positionsCount; j++)
                {
                    var rndOffsetX = Random.Range(0.1f, maxOffsetX);
                    var rndOffsetY = Random.Range(0.1f, maxOffsetY);
                    positions[posIndex] = linePos - new Vector2(rndOffsetX, rndOffsetY);
                    linePos -= new Vector2(0, maxOffsetY);
                    posIndex++;
                }
            }
            return positions;
        }
    }
}