using System.Collections.Generic;
using Fight;
using GolemEntity;
using UnityEngine;

namespace GameLoop
{
    public class Spawner : MonoBehaviour
    {
        [SerializeField] private GameObject[] golemPrefabs;
        [SerializeField] private Vector3 spawnPoint;
        [SerializeField] private float spawnAreaRadius = 50;
        [SerializeField] private Color[] groupColors;
        public Camera[] charactersCameras;

        private Golem Golem { get; set; }
        private static int _group = 0;
        private readonly Game _game = new Game();

        public void SpawnGolem(GolemType golemType, Specialization specialization)
        {
            Golem = new Golem(golemType, specialization);
        
            GameObject newGolem = Instantiate(GetRelevantPrefab(golemType), GetRandomSpawnPoint(), Quaternion.identity);
        
            ConnectGolemWithState(newGolem, Golem, golemType, specialization);
        
            _group++;
        }

        private Vector3 GetRandomSpawnPoint()
        {
            Vector3 randomPoint = spawnPoint +
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

        private int[] GetThreeCharactersDifferentMainParameter()
        {
            var collection = new int[2];
            var res = Random.Range(0, golemPrefabs.Length);
            return default;
        }

        private GameObject GetRelevantPrefab(GolemType golemType)
        {
            var index = (int) golemType;
            return golemPrefabs[index];
        }
    }
}