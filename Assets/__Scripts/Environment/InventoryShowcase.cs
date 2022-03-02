using System;
using System.Collections;
using __Scripts.Inventory;
using CharacterEntity.State;
using Environment;
using GameLoop;
using UnityEngine;

namespace __Scripts.Environment
{
    public class InventoryShowcase : MonoBehaviour
    {
        [SerializeField] private InventoryShowcaseItem[] cells;

        private ChampionState _owner;
        
        private void Start()
        {
            DisableAllCells();

        }

        private void OnEnable()
        {
            SetOwner(Player.PlayerCharacter);
        }

        public void SetOwner(ChampionState owner)
        {
            if (_owner)
            {
                _owner.InventoryHelper.InventoryOrganization.inventory.InventoryStateChanged -= FillCells;
            }
            
            _owner = owner;
            
            FillCells(this);

            _owner.InventoryHelper.InventoryOrganization.inventory.InventoryStateChanged += FillCells;
        }

        private void FillCells(object sender)
        {
            DisableAllCells();
            
            for (int i = 0; i < cells.Length; i++)
            {
                var item = _owner.InventoryHelper.NonEquippingSlots[i].GetSlotItem();
                if (item != null)
                {
                    cells[i].gameObject.SetActive(true);
                    cells[i].Initialize(item);
                }
                
            }
        }
        
        private void SortCellsByType(ItemType firstTypeInList)
        {
            
        }
        
        private void UpdateCells()
        {
            
        }
        
        private void DisableAllCells()
        {
            foreach (var cell in cells)
            {
                cell.gameObject.SetActive(false);
            }
        }

        private void OnDestroy()
        {
            if (_owner)
            {
                _owner.InventoryHelper.InventoryOrganization.inventory.InventoryStateChanged -= FillCells;
            }
        }
    }
}
