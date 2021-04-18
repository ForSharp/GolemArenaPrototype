using System;
using UnityEngine;

namespace __Scripts
{
    public class TestGolem : MonoBehaviour
    {
        private Golem _golem;
        private void CreateGolem()
        {
            _golem = new Golem(GolemType.IronGolem, Specialization.Wizard);
            
            Debug.Log(_golem.GetGolemStats());
        }
        private void Start()
        {
            CreateGolem();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                _golem.ChangeBaseStatsProportionally(100);
                Debug.Log(_golem.GetGolemStats());
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                _golem.ChangeBaseStatsProportionally(-100);
                Debug.Log(_golem.GetGolemStats());
            }
        }
    }
}
