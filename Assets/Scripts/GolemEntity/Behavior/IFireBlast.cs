using __Scripts.ExtraStats;
using GolemEntity.ExtraStats;

namespace __Scripts.GolemEntity.Behavior
{
    public interface IFireBlast : ICastable
    {
        void Blast(GolemExtraStats extraStats);
    }
}
