﻿using System;
using __Scripts.CharacterEntity;
using __Scripts.CharacterEntity.BaseStats;
using __Scripts.Inventory.Abstracts;
using __Scripts.Inventory.Info;

namespace __Scripts.Inventory.Items.PotionItems
{
    public class PotionUltimate : IInventoryItem, IPotionUltimateItem
    {
        public IInventoryItemInfo Info { get; }
        public IInventoryItemState State { get; }
        public PotionUltimateInfo PotionUltimateInfo { get; }
        public string Id => Info.Id;

        public PotionUltimate(IInventoryItemInfo info, PotionUltimateInfo potionUltimateInfo)
        {
            Info = info;
            State = new InventoryItemState();
            PotionUltimateInfo = potionUltimateInfo;
            
            CalculateStats();
        }
        
        public IInventoryItem Clone()
        {
            var clonedPotionUltimate = new PotionUltimate(Info, PotionUltimateInfo);
            clonedPotionUltimate.State.Amount = State.Amount;
            return clonedPotionUltimate;
        }

        private void CalculateStats()
        {
            switch (PotionUltimateInfo.MainCharacterParameter)
            {
                case MainCharacterParameter.Strength:
                    PotionUltimateInfo.CharacterBaseStats = new CharacterBaseStats()
                    {
                        strength = UnityEngine.Random.Range(1.0f, 2.0f),
                        agility = UnityEngine.Random.Range(0.6f, 1.6f),
                        intelligence = UnityEngine.Random.Range(0.6f, 1.6f)
                    };
                    break;
                case MainCharacterParameter.Agility:
                    PotionUltimateInfo.CharacterBaseStats = new CharacterBaseStats()
                    {
                        strength = UnityEngine.Random.Range(0.6f, 1.6f),
                        agility = UnityEngine.Random.Range(1.0f, 2.0f),
                        intelligence = UnityEngine.Random.Range(0.6f, 1.6f)
                    };
                    break;
                case MainCharacterParameter.Intelligence:
                    PotionUltimateInfo.CharacterBaseStats = new CharacterBaseStats()
                    {
                        strength = UnityEngine.Random.Range(0.6f, 1.6f),
                        agility = UnityEngine.Random.Range(0.6f, 1.6f),
                        intelligence = UnityEngine.Random.Range(1.0f, 2.0f)
                    };
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}