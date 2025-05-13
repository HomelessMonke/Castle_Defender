using System;
using System.ComponentModel;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Game.Characters.Spawners
{
    [Serializable, Description("Неупорядоченное построение")]
    public class RandomSpawnFormation: ISpawnFormation
    {
        [Header("Сколько позиций в каждой линии")]
        [SerializeField]
        int[] linePositionsCounts;
        
        [SerializeField, Range(0.2f, 1)]
        float maxOffsetX;
        
        [SerializeField, Range(0.2f, 1)]
        float maxOffsetY;

        public RandomSpawnFormation()
        {
            maxOffsetX = maxOffsetY = 0.2f;
        }

        public Vector2[] GetSpawnPoints(Vector2 startPosition)
        {
            var count = linePositionsCounts.Sum(x => x);
            Vector2[] positions = new Vector2[count];

            int posIndex = 0;
            for (int i = 0; i < linePositionsCounts.Length; i++)
            {
                var positionsCount = linePositionsCounts[i];
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