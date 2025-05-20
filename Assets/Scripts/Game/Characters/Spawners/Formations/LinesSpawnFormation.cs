using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

namespace Game.Characters.Spawners.Formations
{
    [Serializable, Description("Линейное построение ")]
    public class LinesSpawnFormation: ISpawnFormation
    {
        [SerializeField]
        bool spawnToRight = true;
        
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

        public void SetLinePositionsCounts(int currentCount, int maxInLine)
        {
            List<int> lineCounts = new();
            while (currentCount / maxInLine >= 1)
            {
                lineCounts.Add(maxInLine);
                currentCount-=maxInLine;
            }
            
            if(currentCount>0)
                lineCounts.Add(currentCount);
            
            linePositionsCounts = lineCounts.ToArray();
        }

        public Vector2[] GetSpawnPoints(Transform transform)
        {
            Vector2 startPosition = transform.position;
            var count = linePositionsCounts.Sum(x => x);
            Vector2[] positions = new Vector2[count];

            int posIndex = 0;
            for (int i = 0; i < linePositionsCounts.Length; i++)
            {
                var positionsCount = linePositionsCounts[i];
                var yOffset = unitsOffsetY * ((float)(positionsCount-1) / 2);
                var lineOffset = (spawnToRight ? 1 : -1) * ((Vector2)transform.right * i * unitsOffsetX) - (Vector2)transform.up * yOffset;
                var lineStartPos = startPosition + lineOffset;
                for (int j = 0; j < positionsCount; j++)
                {
                    positions[posIndex] = lineStartPos + j * (Vector2)transform.up * unitsOffsetY;
                    posIndex++;
                }
            }
            return positions;
        }
    }
}