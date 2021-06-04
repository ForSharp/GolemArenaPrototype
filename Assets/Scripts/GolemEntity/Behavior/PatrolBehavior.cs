using __Scripts.ExtraStats;
using GolemEntity.ExtraStats;
using UnityEngine;

namespace __Scripts.GolemEntity.Behavior
{
    public class PatrolBehavior : IMovable
    {
        private Transform _transform;
        private Vector3[] _endPoints;
        
        public PatrolBehavior(Transform transform, Vector3[] endPoints)
        {
            _transform = transform;
            _endPoints = endPoints;
        }
        
        public void Move(GolemExtraStats extraStats)
        {
            //Move between endpoints
        }
    }
}
