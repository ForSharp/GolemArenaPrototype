using CharacterEntity;
using CharacterEntity.BaseStats;
using CharacterEntity.CharacterState;
using CharacterEntity.State;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

namespace GameLoop
{
    public class Spawner : MonoBehaviour
    {
        [FormerlySerializedAs("golemPrefabs")] [SerializeField] private GameObject[] characterPrefabs;
        [SerializeField] private GameObject[] creepPrefabs;
        [SerializeField] private Vector3 spawnPoint;
        [SerializeField] private float spawnAreaRadius = 50;
        [SerializeField] private Color[] groupColors;

        public static Spawner Instance { get; private set; }
        private static int _group;

        private void Awake()
        {
            Instance = this;
        }

        public void SpawnChampion(CharacterType characterType, Specialization specialization, bool isPlayerCharacter = false)
        {
            var character = new Character(characterType, specialization);
            var newCharacter = Instantiate(GetRelevantPrefab(characterType), GetRandomSpawnPoint(), Quaternion.identity);
            var state = ConnectChampionWithState(newCharacter, character, characterType, specialization);

            _group++;

            if (isPlayerCharacter)
            {
                Player.SetPlayerCharacter(state);
            }
        }

        public void SpawnCreep(CreepType creepType, CharacterBaseStats creepStats, ChampionState owner, Vector3 pos, float duration)
        {
            var character = new Character(creepStats);
            var newCharacter = Instantiate(GetRelevantPrefab(creepType), pos, Quaternion.identity);
            ConnectCreepWithState(newCharacter, character, creepType, owner, duration);
        }
        
        public void SpawnCreep(GameObject creepPrefab, CreepType creepType, CharacterBaseStats creepStats, ChampionState owner, Vector3 pos, float duration)
        {
            var character = new Character(creepStats);
            var newCharacter = Instantiate(creepPrefab, pos, Quaternion.identity);
            ConnectCreepWithState(newCharacter, character, creepType, owner, duration);
        }

        private Vector3 GetRandomSpawnPoint()
        {
            var randomPoint = spawnPoint +
                              new Vector3(Random.value - 0.5f, spawnPoint.y, Random.value - 0.5f).normalized *
                              spawnAreaRadius;
            return randomPoint;
        }

        private ChampionState ConnectChampionWithState(GameObject newChampion, Character character, CharacterType characterType, Specialization specialization)
        {
            var state = newChampion.GetComponent<ChampionState>();
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

        private CreepState ConnectCreepWithState(GameObject newCreep, Character character, CreepType creepType, ChampionState owner, float duration)
        {
            var state = newCreep.GetComponent<CreepState>();
            state.InitializeState(character, owner, creepType.ToString(), duration);
            Game.AddCharacterToAllCharactersList(state);
            return state;
        }
        
        private GameObject GetRelevantPrefab(CharacterType characterType)
        {
            var index = (int) characterType;
            return characterPrefabs[index];
        }
        
        private GameObject GetRelevantPrefab(CreepType creepType)
        {
            var index = (int) creepType;
            return creepPrefabs[index];
        }
    }
}