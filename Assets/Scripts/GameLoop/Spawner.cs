using System;
using System.Collections.Generic;
using Fight;
using GolemEntity;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameLoop
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] golemPrefabs;
        [SerializeField] private Vector3 spawnPoint;
        [SerializeField] private float spawnAreaRadius = 50;
        [SerializeField] private Color[] groupColors;

        public static Spawner Instance { get; private set; }
        private static int _group = 0;
        private readonly Game _game = new Game();

        private void Start()
        {
            Instance = this;
        }

        public void SpawnGolem(GolemType golemType, Specialization specialization, bool isPlayerCharacter = false)
        {
            var golem = new Golem(golemType, specialization);
            var newGolem = Instantiate(GetRelevantPrefab(golemType), GetRandomSpawnPoint(), Quaternion.identity);
            ConnectGolemWithState(newGolem, golem, golemType, specialization);

            _group++;
            
            if (isPlayerCharacter)
                Player.SetPlayerCharacter(golem);
        }

        private Vector3 GetRandomSpawnPoint()
        {
            var randomPoint = spawnPoint +
                              new Vector3(Random.value - 0.5f, spawnPoint.y, Random.value - 0.5f).normalized *
                              spawnAreaRadius;
            return randomPoint;
        }

        private void ConnectGolemWithState(GameObject newGolem, Golem golem, GolemType golemType, Specialization specialization)
        {
            var state = newGolem.GetComponent<GameCharacterState>();
            if (_group < groupColors.Length)
            {
                state.InitializeState(golem, _group, groupColors[_group], golemType.ToString(), specialization.ToString());
                _game.AddToAllGolems(state);
            }
            else if (_group >= groupColors.Length)
            {
                state.InitializeState(golem, _group, Color.black, golemType.ToString(),specialization.ToString());
                _game.AddToAllGolems(state);
            }
        }

        private GameObject GetRelevantPrefab(GolemType golemType)
        {
            var index = (int) golemType;
            return golemPrefabs[index];
        }
    }
}