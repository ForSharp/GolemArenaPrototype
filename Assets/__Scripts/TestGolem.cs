using UnityEngine;

namespace __Scripts
{
    public class TestGolem : MonoBehaviour
    {
        private void CreateGolem()
        {
            var golem = gameObject.AddComponent<Golem>();
            Debug.Log(golem.GetGolemStats());
        }
        private void Start()
        {
            CreateGolem();
            
        }
    }
}
