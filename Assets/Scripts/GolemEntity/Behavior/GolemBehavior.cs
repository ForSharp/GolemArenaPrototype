using UnityEngine;

namespace __Scripts.GolemEntity.Behavior
{
    public abstract class GolemBehavior : MonoBehaviour
    {
        protected IMovable _movable;
        protected IHittable _hittable;
        protected IDefendable _defendable;
        protected ICastable _castable;

        public void ChangeMoving(IMovable movable)
        {
            _movable = movable;
        }
        
        public void ChangeHitting(IHittable hittable)
        {
            _hittable = hittable;
        }
        public void ChangeDefending(IDefendable defendable)
        {
            _defendable = defendable;
        }
        public void ChangeCasting(ICastable castable)
        {
            _castable = castable;
        }

        protected abstract void InitBehaviors();
    }
}
