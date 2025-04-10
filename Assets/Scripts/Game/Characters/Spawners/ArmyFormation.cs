using UnityEngine;
namespace Game.Characters.Spawners
{
    public class ArmyFormation
    {
        int maxInRow;
        Vector3 defaultSpawnPos;
        float unitsOffsetX,unitsOffsetY;
        
        public ArmyFormation(Vector3 defaultSpawnPos, float unitsOffsetX, float unitsOffsetY, int maxInRow)
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
        public Vector3 GetSpawnPoint(int index)
        {
            var currentRowIndex = index % maxInRow;
            bool isEvenNumber = currentRowIndex % 2 == 0;
            var yOffsetMultiplier = Mathf.CeilToInt((float)currentRowIndex / 2) * (isEvenNumber ? 1 : -1);
            var offsetY = unitsOffsetY * yOffsetMultiplier;
            var spawnPos = defaultSpawnPos + new Vector3(0,offsetY,0);
                
            var xOffsetMultiplier = index/maxInRow;
            if (xOffsetMultiplier>0)
                spawnPos += new Vector3(unitsOffsetX * xOffsetMultiplier, 0, 0);

            return spawnPos;
        }
    }
}