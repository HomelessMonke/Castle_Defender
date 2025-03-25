using Game.Waves;
using UnityEngine;

namespace Game.Characters.Spawners
{
    public class CharactersSpawner: MonoBehaviour
    {
        [SerializeField]
        EnemySwordsManSpawner swordsManSpawner;

        public void Init()
        {
            swordsManSpawner.Init();
        }

        public Character[] Spawn(SquadInfo squadInfo)
        {
            var characterType = squadInfo.Type;
            var charactersCount = squadInfo.Count;
            Character[] characters = new Character[charactersCount];
            switch (characterType)
            {
                case CharacterType.EnemySwordsMan:
                    characters = swordsManSpawner.Spawn(squadInfo.Parameters, charactersCount);
                    break;
            }
            return characters;
        }
    }
}