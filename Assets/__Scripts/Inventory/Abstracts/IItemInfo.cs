using UnityEngine;

namespace Inventory.Abstracts
{
    public interface IItemInfo<out T> where T : ScriptableObject
    {
        T Info { get; }
    }
}