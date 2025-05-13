using System;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

namespace Game.Characters.Spawners
{
    [Serializable, Description("Линейное построение ")]
    public class LinesSpawnFormation: ISpawnFormation
    {
        [Header("Сколько позиций в каждой линии")]
        [SerializeField]
        int[] linePositionsCounts;
        
        [Header("Отступы между позициями по X")]
        [SerializeField, Range(0.2f, 1)]
        float unitsOffsetX;
        
        [Header("Отступы между позициями по Y")]
        [SerializeField, Range(0.2f, 1)]
        float unitsOffsetY;

        public LinesSpawnFormation()
        {
            unitsOffsetX = unitsOffsetY = 0.2f;
        }

        public Vector2[] GetSpawnPoints(Vector2 startPosition)
        {
            var count = linePositionsCounts.Sum(x => x);
            Vector2[] positions = new Vector2[count];

            int posIndex = 0;
            for (int i = 0; i < linePositionsCounts.Length; i++)
            {
                var positionsCount = linePositionsCounts[i];
                var yOffset = unitsOffsetY * ((float)(positionsCount-1) / 2);
                var lineStartPos = startPosition + new Vector2(i * unitsOffsetX, yOffset);
                for (int j = 0; j < positionsCount; j++)
                {
                    positions[posIndex] = lineStartPos - new Vector2(0, j * unitsOffsetY);
                    posIndex++;
                }
            }
            return positions;
        }
    }
}