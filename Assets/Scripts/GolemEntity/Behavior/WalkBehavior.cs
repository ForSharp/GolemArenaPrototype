using __Scripts.ExtraStats;
using GolemEntity.ExtraStats;
using UnityEngine;

namespace __Scripts.GolemEntity.Behavior
{
    public class WalkBehavior : IMovable
    {
        private Transform _transform;
        private Vector3[] _pointsVariants;

        public WalkBehavior(Transform transform, Vector3[] pointsVariants)
        {
            _transform = transform;
            _pointsVariants = pointsVariants;
        }
        
        public void Move(GolemExtraStats extraStats)
        {
            _transform.position = Vector3.MoveTowards(_transform.localPosition, 
                _pointsVariants[0], Time.deltaTime * extraStats.MoveSpeed);
            
            //Animation walk
        }
    }
}
