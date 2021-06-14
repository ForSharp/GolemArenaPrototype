using UnityEngine;
using System.Collections.Generic;
using Scripts;
using GolemEntity;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] golemPrefabs;
    [SerializeField] private Vector3 spawnPoint;

    private Golem Golem { get; set; }
    private static int _group = 0;

    public void SpawnGolem(GolemType golemType, Specialization specialization)
    {
        Golem = new Golem(golemType, specialization);
        var randomSpawn = new Vector3(spawnPoint.x + Random.Range(-20, +21), spawnPoint.y,
            spawnPoint.z + Random.Range(-20, +21));
        GameObject newGolem = Instantiate(GetRelevantPrefab(golemType), randomSpawn, Quaternion.identity);

        Game.AddToAllGolems(Golem);

        InitializeCharacterState(newGolem, golemType, specialization);
        _group++;
    }

    private void InitializeCharacterState(GameObject newGolem, GolemType golemType, Specialization specialization)
    {
        var state = newGolem.GetComponent<GameCharacterState>();
        state.Golem = Golem;
        state.Group = _group;
        state.InitProps();
        state.Type = golemType.ToString();
        state.Spec = specialization.ToString();
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