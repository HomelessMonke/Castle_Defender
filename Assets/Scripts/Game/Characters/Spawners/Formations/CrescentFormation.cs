using System;
using System.ComponentModel;
using System.Linq;
using UnityEngine;

namespace Game.Characters.Spawners.Formations
{
    [Serializable, Description("Строение полумесяцем")]
    public class CrescentFormation: ISpawnFormation
    {
        [Header("Направление построение")]
        [SerializeField]
        bool facingLeft;
        
        [Header("Расстояние между линиями")]
        [SerializeField]
        [Range(0.5f, 1f)]
        float lineSpacing;

        [Header("Угол охвата дуги(в градусах)")]
        [SerializeField]
        [Range(90, 360)]
        float angleRangeDegrees;
        
        [Header("Начальный радиус первой линии")]
        [SerializeField]
        [Range(0, 1.5f)]
        float baseRadius;

        public CrescentFormation()
        {
            lineSpacing = 0.5f;
            angleRangeDegrees = 90;
        }

        public Vector2[] GetSpawnPoints(int[] charactersInLines, Transform transform)
        {
            Vector2 startPosition = transform.position;
            var count = charactersInLines.Sum(x => x);
            Vector2[] positions = new Vector2[count];

            if (charactersInLines == null || charactersInLines.Length == 0)
                return positions;

            int directionSign = facingLeft ? 1 : -1;
            var posIndex = 0;
            for (int i = 0; i < charactersInLines.Length; i++)
            {
                int unitCount = charactersInLines[i];
                if (unitCount <= 0) continue;

                float radius = baseRadius + i * lineSpacing;

                int stepsCount = angleRangeDegrees == 360 ? unitCount : unitCount - 1;
                float angleStep = unitCount > 1 ? angleRangeDegrees / stepsCount : 0f;
                float startAngle = -angleRangeDegrees / 2f;

                for (int j = 0; j < unitCount; j++)
                {
                    float angle = startAngle + j * angleStep;
                    float rad = angle * Mathf.Deg2Rad;

                    Vector2 offset = new Vector2(directionSign * Mathf.Cos(rad), Mathf.Sin(rad)) * radius;
                    Vector2 position = startPosition + offset;

                    positions[posIndex] = position;
                    posIndex++;
                }
            }

            return positions;
        }
    }
}