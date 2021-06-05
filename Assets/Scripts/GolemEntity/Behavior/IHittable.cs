using __Scripts.ExtraStats;
using GolemEntity.ExtraStats;

namespace __Scripts.GolemEntity.Behavior
{
    public interface IHittable
    {
        void Hit(GolemExtraStats extraStats);
    }
}
