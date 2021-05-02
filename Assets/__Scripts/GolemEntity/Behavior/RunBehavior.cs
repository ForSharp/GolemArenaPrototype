using __Scripts.ExtraStats;
using UnityEngine;

namespace __Scripts.GolemEntity.Behavior
{
    public class RunBehavior : IMovable
    {
        private Transform _transform;
        private Vector3[] _pointsVariants;
        private const float RunningAcceleration = 3f;

        public RunBehavior(Transform transform, Vector3[] pointsVariants)
        {
            _transform = transform;
            _pointsVariants = pointsVariants;
        }
        
        public void Move(GolemExtraStats extraStats)
        {
            while (extraStats.Stamina >= 0f)
            {
                _transform.position = Vector3.MoveTowards(_transform.localPosition, 
                    _pointsVariants[0], Time.deltaTime * extraStats.MoveSpeed * RunningAcceleration);
                
                //DecreaseStaminaAmount();

                //Animation run
            }
        }
    }
}
