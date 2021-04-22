using System;
using __Scripts.GolemEntity;
using UnityEngine;

namespace __Scripts
{
    public class TestGolem : MonoBehaviour
    {
        private Golem _golem;
        private void CreateGolem()
        {
            _golem = new Golem(GolemType.IronGolem, Specialization.Warrior);
            
            Debug.Log(_golem.GetGolemStats());
            Debug.Log(_golem.GetExtras());
        }
        private void Start()
        {
            CreateGolem();
            
            GolemBaseStats golem = _golem.GetCurrentStats(_golem._provider);
            
            Debug.Log("fdfd" +golem);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                _golem.ChangeBaseStatsProportionally(100);
                Debug.Log(_golem.GetGolemStats());
                Debug.Log(_golem.GetExtras());
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                _golem.ChangeBaseStatsProportionally(-100);
                Debug.Log(_golem.GetGolemStats());
                Debug.Log(_golem.GetExtras());
            }
        }
    }
}
