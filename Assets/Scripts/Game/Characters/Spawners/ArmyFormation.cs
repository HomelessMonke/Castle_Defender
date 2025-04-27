using UnityEngine;
namespace Game.Characters.Spawners
{
    public class ArmyFormation
    {
        int maxInRow;
        Vector2 defaultSpawnPos;
        float unitsOffsetX,unitsOffsetY;
        
        public ArmyFormation(Vector2 defaultSpawnPos, float unitsOffsetX, float unitsOffsetY, int maxInRow)
        {
            this.defaultSpawnPos  = defaultSpawnPos;
            this.unitsOffsetX = unitsOffsetX;
            this.unitsOffsetY = unitsOffsetY;
            this.maxInRow = maxInRow;
        }

        /// <summary>
        /// </summary>
        /// <param name="index">Порядковый номер лемента в коллекции при спауне</param>
        /// <returns></returns>
        public Vector2 GetSpawnPoint(int index)
        {
            var currentRowIndex = index % maxInRow;
            bool isEvenNumber = currentRowIndex % 2 == 0;
            var yOffsetMultiplier = Mathf.CeilToInt((float)currentRowIndex / 2) * (isEvenNumber ? 1 : -1);
            var offsetY = unitsOffsetY * yOffsetMultiplier;
            var spawnPos = defaultSpawnPos + new Vector2(0,offsetY);
                
            var xOffsetMultiplier = index/maxInRow;
            if (xOffsetMultiplier>0)
                spawnPos += new Vector2(unitsOffsetX * xOffsetMultiplier, 0);

            return spawnPos;
        }
    }
}