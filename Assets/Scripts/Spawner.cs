using UnityEngine;
using System.Collections.Generic;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject [] golemPrefabs;
    private Dictionary<string, GameObject> _golemDictionary = new Dictionary<string, GameObject>();
    
    public void SpawnGolem()
    {
        
    }
}

