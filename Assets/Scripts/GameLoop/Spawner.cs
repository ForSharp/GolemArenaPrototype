﻿using System.Collections.Generic;
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
                state.InitializeState(golem, _group, groupColors[_group], specialization.ToString());
                _game.AddToAllGolems(state);
            }
            else if (_group >= groupColors.Length)
            {
                state.InitializeState(golem, _group, Color.black, specialization.ToString());
                _game.AddToAllGolems(state);
            }
        
        }

        private GameObject GetRelevantPrefab(GolemType golemType)
        {
            var golemDictionary = new Dictionary<string, GameObject>
            {
                {"WaterGolem", golemPrefabs[0]},
                {"AirGolem", golemPrefabs[1]},
                {"CrystalGolem", golemPrefabs[2]},
                {"FireGolem", golemPrefabs[3]},
                {"PlasmaGolem", golemPrefabs[4]},
                {"SteamGolem", golemPrefabs[5]},
                {"DarkGolem", golemPrefabs[6]},
                {"NatureGolem", golemPrefabs[7]},
                {"FogGolem", golemPrefabs[8]},
                {"ObsidianGolem", golemPrefabs[9]},
                {"InsectGolem", golemPrefabs[10]},
                {"StalagmiteGolem", golemPrefabs[11]}
            };

            return golemDictionary[golemType.ToString()];
        }
    }
}