using Game.Characters.Parameters;
using Game.Characters.Units;
using Game.Waves;
using UnityEngine;

namespace Game.Characters.Spawners
{
    public class AllyMeleeCharacterSpawner : CharacterSpawner
    {
        [SerializeField]
        ObjectsPool<MeleeCharacter> pool;
        
        [SerializeField]
        AllyMeleeUnitParameters unitParameters;
        
        [Space(10)]
        [SerializeField]
        Transform spawnPointTransform;
        
        //TODO: перенести оффсеты в squadInfo или отдельный класс, на который будет ссылать squadInfo
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
                characters[i] = Spawn(spawnPos);
            }
            return characters;
        }
        
        Character Spawn(Vector2 spawnPosition)
        {
            var unit = pool.Spawn(true);
            unit.transform.position = spawnPosition;
            unit.Init(unitParameters);
            unit.Died+= ()=> pool.Despawn(unit);
            return unit;
        }
    }
}