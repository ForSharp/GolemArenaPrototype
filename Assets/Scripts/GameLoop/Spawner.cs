using CharacterEntity;
using CharacterEntity.CharacterState;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace GameLoop
{
    public class Spawner : MonoBehaviour
    {
        [FormerlySerializedAs("golemPrefabs")] [SerializeField] private GameObject[] characterPrefabs;
        [SerializeField] private Vector3 spawnPoint;
        [SerializeField] private float spawnAreaRadius = 50;
        [SerializeField] private Color[] groupColors;

        public static Spawner Instance { get; private set; }
        private static int _group;

        private void Awake()
        {
            Instance = this;
        }

        public void SpawnGolem(CharacterType characterType, Specialization specialization, bool isPlayerCharacter = false)
        {
            var character = new Character(characterType, specialization);
            var newCharacter = Instantiate(GetRelevantPrefab(characterType), GetRandomSpawnPoint(), Quaternion.identity);
            var state = ConnectGolemWithState(newCharacter, character, characterType, specialization);

            _group++;

            if (isPlayerCharacter)
            {
                Player.SetPlayerCharacter(state);
            }
        }

        private Vector3 GetRandomSpawnPoint()
        {
            var randomPoint = spawnPoint +
                              new Vector3(Random.value - 0.5f, spawnPoint.y, Random.value - 0.5f).normalized *
                              spawnAreaRadius;
            return randomPoint;
        }

        private CharacterState ConnectGolemWithState(GameObject newGolem, Character character, CharacterType characterType, Specialization specialization)
        {
            var state = newGolem.GetComponent<CharacterState>();
            if (_group < groupColors.Length)
            {
                state.InitializeState(character, _group, groupColors[_group], characterType.ToString(), specialization.ToString());
                Game.AddCharacterToAllCharactersList(state);
            }
            else if (_group >= groupColors.Length)
            {
                state.InitializeState(character, _group, Color.black, characterType.ToString(),specialization.ToString());
                Game.AddCharacterToAllCharactersList(state);
            }

            return state;
        }

        private GameObject GetRelevantPrefab(CharacterType characterType)
        {
            var index = (int) characterType;
            return characterPrefabs[index];
        }
    }
}