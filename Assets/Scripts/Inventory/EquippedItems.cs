using GolemEntity;
using Inventory.Abstracts;

namespace Inventory
{
    public class EquippedItems
    {
        private Golem _inventoryOwner;
        private IInventorySlot[] _equippingSlots = new IInventorySlot[6];
        private int _slotsCapacity;
        public bool EquippingSlotsIsFull => _slotsCapacity == _equippingSlots.Length;

        public EquippedItems(Golem owner)
        {
            _inventoryOwner = owner;
        }
        
        public void EquipItem(IInventoryItem item)
        {
            //здесь нужно наложить эффект от предмета (если эффект имеется) и сохранить ссылку на этот эффект
            
            //также здесь нужно добавить предмет в массив и увеличить заполненность экипированных предметов на 1

            //у предмета есть информация об его эффекте
            
            //эффект может быть только у предметов, реализующих интерфейс АЙАртефакт
            
            //Голем должен предоставить метод для применения эффекта
        }
    }
}