using Game.Waves;
using UnityEngine;
namespace Game.Characters.Spawners
{
    public class SwordsManCharacterSpawner : CharacterSpawner
    {
        [SerializeField]
        ObjectsPool<SwordsManCharacter> pool;
        
        [Space(10)]
        [SerializeField]
        Transform spawnPointTransform;
        
        [SerializeField]
        Transform mainTargetTransform;
        
        //TODO: перенести оффсеты в squadInfo или отдельынй класс, на который будет ссылать squadInfo
        [SerializeField]
        protected float unitsOffsetX;
        
        [SerializeField]
        protected float unitsOffsetY;
        
        public override void Init()
        {
            pool.Init();
        }
        
        public override Character[] Spawn(SquadInfo squadInfo)
        {
            int unitsCount = squadInfo.Count;
            ArmyFormation armyFormation = new ArmyFormation(spawnPointTransform.position, unitsOffsetX, unitsOffsetY, squadInfo.MaxUnitsInRow);
            var characters = new Character[unitsCount];
            for (int i = 0; i < unitsCount; i++)
            {
                var spawnPos = armyFormation.GetSpawnPoint(i);
                characters[i] = Spawn(squadInfo.Parameters, spawnPos);
            }
            return characters;
        }
        
        Character Spawn(CharacterParameters parameters, Vector3 spawnPosition)
        {
            var unit = pool.Spawn(true);
            unit.transform.position = spawnPosition;
            unit.Init(mainTargetTransform, parameters);
            unit.Died+= ()=> pool.Despawn(unit);
            return unit;
        }
    }
}