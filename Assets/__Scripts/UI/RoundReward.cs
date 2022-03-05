using System.Collections.Generic;
using __Scripts.GameLoop;
using __Scripts.Inventory;
using __Scripts.Inventory.Abstracts;
using CharacterEntity.State;
using Inventory;
using UnityEngine;

namespace __Scripts.UI
{
    public class RoundReward : MonoBehaviour
    {
        [SerializeField] private GameObject rewardPanel;
        [SerializeField] private RoundRewardSlot[] slots;
        
        private Dictionary<string, int> itemAndAmounts = new Dictionary<string, int>();
        private void OnEnable()
        {
            ItemDispenser.RoundRewardsDispensed += ShowPanel;
        }

        private void OnDestroy()
        {
            ItemDispenser.RoundRewardsDispensed -= ShowPanel;
        }

        private void ShowPanel(ChampionState owner, List<IInventoryItem> items)
        {
            rewardPanel.SetActive(true);
            itemAndAmounts.Clear();
            
            if (owner == Player.PlayerCharacter)
            {
                foreach (var item in items)
                {
                    if (itemAndAmounts.ContainsKey(item.Id))
                    {
                        itemAndAmounts.TryGetValue(item.Id, out var amount);
                        amount++;
                        itemAndAmounts.Remove(item.Id);
                        itemAndAmounts.Add(item.Id, amount);
                    }
                    else
                    {
                        itemAndAmounts.Add(item.Id, 1);
                    }
                }

                var counter = 0;
                foreach (var itemAndAmount in itemAndAmounts)
                {
                    if (slots.Length > counter)
                    {
                        slots[counter].gameObject.SetActive(true);
                        slots[counter].SetInfo(ItemContainer.Instance.GetItemById(itemAndAmount.Key).Info.SpriteIcon, itemAndAmount.Value.ToString());
                        counter++;
                    }
                }
            }
        }

        public void CloseRewardPanel()
        {
            foreach (var slot in slots)
            {
                slot.gameObject.SetActive(false);
            }
            
            rewardPanel.SetActive(false);
        }
    }
}