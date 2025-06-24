using System;
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

        public Vector2[] GetSpawnPoints(int[] charactersInLines, Transform transform)
        {
            Vector2 startPosition = transform.position;
            var count = charactersInLines.Sum(x => x);
            Vector2[] positions = new Vector2[count];

            int posIndex = 0;
            for (int i = 0; i < charactersInLines.Length; i++)
            {
                var positionsCount = charactersInLines[i];
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