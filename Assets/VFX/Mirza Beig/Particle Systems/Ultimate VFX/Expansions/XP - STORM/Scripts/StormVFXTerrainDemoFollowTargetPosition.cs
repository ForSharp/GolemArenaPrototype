using UnityEngine;

namespace VFX.Mirza_Beig.Particle_Systems.Ultimate_VFX.Expansions.XP___STORM.Scripts
{
    public class StormVFXTerrainDemoFollowTargetPosition : MonoBehaviour
    {
        public Transform target;

        void Start()
        {

        }

        void Update()
        {

        }

        void LateUpdate()
        {
            transform.position = target.position;
        }
    }
}
