using GolemEntity.ExtraStats;

namespace Inventory.Items
{
    public abstract class IArtefactItem
    {
        TypeExtraStats[] Effects { get; }

        void SetEffects()
        {
            
        }
    }
}
