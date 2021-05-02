using UnityEngine;

namespace __Scripts.GolemEntity.Behavior
{
    public abstract class GolemBehavior : MonoBehaviour
    {
        protected IMovable _movable;
        protected IHitable _hitable;
        protected IDefencable _defencable;
        protected ICastable _castable;

        public void ChangeMoving(IMovable movable)
        {
            _movable = movable;
        }
        
        public void ChangeHitting(IHitable hitable)
        {
            _hitable = hitable;
        }
        public void ChangeDefencing(IDefencable defencable)
        {
            _defencable = defencable;
        }
        public void ChangeCasting(ICastable castable)
        {
            _castable = castable;
        }

        protected abstract void InitBehaviors();
    }
}
