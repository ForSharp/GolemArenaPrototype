using __Scripts.GolemEntity;
using UnityEngine;

namespace __Scripts.TestScripts
{
    public class TestGolem : MonoBehaviour
    {
        private Golem _golem;
        private void CreateGolem()
        {
            _golem = new Golem(GolemType.IronGolem, Specialization.Warrior);
            
            Debug.Log(_golem.GetGolemBaseStats());
            Debug.Log(_golem.GetGolemExtraStats());
            
            _golem.ShowGolemBaseStats();
            _golem.ShowGolemExtraStats();
        }
        private void Start()
        {
            CreateGolem();
            
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                _golem.ChangeBaseStatsProportionally(10);
                Debug.Log(_golem.GetGolemBaseStats());
                Debug.Log(_golem.GetGolemExtraStats());
                _golem.ShowGolemBaseStats();
                _golem.ShowGolemExtraStats();
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                _golem.ChangeBaseStatsProportionally(-10);
                Debug.Log(_golem.GetGolemBaseStats());
                Debug.Log(_golem.GetGolemExtraStats());
                _golem.ShowGolemBaseStats();
                _golem.ShowGolemExtraStats();
            }
        }
    }
}
