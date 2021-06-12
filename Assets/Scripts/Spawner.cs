using UnityEngine;
using System.Collections.Generic;
using __Scripts;
using GolemEntity;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject[] golemPrefabs;

    [SerializeField] private Vector3 spawnPoint;

    public Golem Golem { get; private set; }

    public void SpawnGolem(GolemType golemType, Specialization specialization)
    {
        Golem = new Golem(golemType, specialization);
        var randomSpawn = new Vector3(spawnPoint.x + Random.Range(-20, +21), spawnPoint.y,
            spawnPoint.z + Random.Range(-10, +11));
        GameObject newGolem = Instantiate(GetRelevantPrefab(golemType), randomSpawn, Quaternion.identity);
        
        Game.AddToAllGolems(Golem);
        
        
        var state = newGolem.GetComponent<GameCharacterState>();
        state.Golem = Golem;
        state.Group = Random.Range(1, 10);
        state.InitProps();

        var user = GetComponent<UserInputTest>();
        user.Golem = Golem;
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