﻿using System;
using GolemEntity;
using GolemEntity.BaseStats;
using Inventory.Abstracts;
using Inventory.Info;

namespace Inventory.Items
{
    public class PotionUltimate : IInventoryItem, IPotionUltimateItem
    {
        public IInventoryItemInfo Info { get; }
        public IInventoryItemState State { get; }
        public PotionUltimateInfo PotionUltimateInfo { get; }
        public Type Type => GetType();

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
                    PotionUltimateInfo.GolemBaseStats = new GolemBaseStats()
                    {
                        strength = UnityEngine.Random.Range(1.0f, 2.0f),
                        agility = UnityEngine.Random.Range(0.6f, 1.6f),
                        intelligence = UnityEngine.Random.Range(0.6f, 1.6f)
                    };
                    break;
                case MainCharacterParameter.Agility:
                    PotionUltimateInfo.GolemBaseStats = new GolemBaseStats()
                    {
                        strength = UnityEngine.Random.Range(0.6f, 1.6f),
                        agility = UnityEngine.Random.Range(1.0f, 2.0f),
                        intelligence = UnityEngine.Random.Range(0.6f, 1.6f)
                    };
                    break;
                case MainCharacterParameter.Intelligence:
                    PotionUltimateInfo.GolemBaseStats = new GolemBaseStats()
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