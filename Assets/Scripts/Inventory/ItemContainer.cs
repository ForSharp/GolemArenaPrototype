﻿using System;
using System.Collections.Generic;
using Inventory.Abstracts;
using Inventory.Info;
using Inventory.Info.Spells;
using Inventory.Items;
using Inventory.Items.SpellItems;
using UnityEngine;

namespace Inventory
{
    public class ItemContainer : MonoBehaviour
    {
        [Header("Inventory Item Info")]
        [SerializeField] private InventoryItemInfo appleItemInfo;
        public Apple Apple => new Apple(appleItemInfo, appleConsumableHealingInfo);
        
        [SerializeField] private InventoryItemInfo warHelmetItemInfo;
        public WarHelmet WarHelmet => new WarHelmet(warHelmetItemInfo, warHelmetArtefactInfo);
        
        [SerializeField] private InventoryItemInfo fireBallItemInfoLvl1;
        public FireBallItem FireBallLvl1 => new FireBallItem(fireBallItemInfoLvl1, fireBallLvl1, fireBallDamageLvl1,
            fireBallPeriodicDamageLvl1);

        [SerializeField] private InventoryItemInfo graceBuffItemInfoLvl1;
        public GraceBuffItem GraceBuffItemLvl1 => new GraceBuffItem(graceBuffItemInfoLvl1, graceBuffLvl1, 
            graceBuffHealLvl1, graceBuffBuffLvl1);

        [SerializeField] private InventoryItemInfo snowstormItemInfoLvl1;
        public SnowstormItem SnowstormItemLvl1 => new SnowstormItem(snowstormItemInfoLvl1, snowstormLvl1, 
            snowstormPeriodicDamageLvl1, snowstormDebuffLvl1);

        [SerializeField] private InventoryItemInfo summonSpiderInfoLvl1;
        public SummonSpiderItem SummonSpiderItemLvl1 =>
            new SummonSpiderItem(summonSpiderInfoLvl1, summonSpiderLvl1, summonSpiderSummonInfoLvl1);

        [SerializeField] private InventoryItemInfo freezingItemInfoLvl1;
        public FreezingItem FreezingItemLvl1 =>
            new FreezingItem(freezingItemInfoLvl1, freezingLvl1, freezingPolymorphInfoLvl1);
        
        [SerializeField] private InventoryItemInfo potionFlatSmallItemInfo;
        public PotionFlat PotionFlatSmall => new PotionFlat(potionFlatSmallItemInfo, potionFlatSmallInfo);
        
        [SerializeField] private InventoryItemInfo potionFlatMediumItemInfo;
        public PotionFlat PotionFlatMedium => new PotionFlat(potionFlatMediumItemInfo, potionFlatMediumInfo);
        
        [SerializeField] private InventoryItemInfo potionFlatLargeItemInfo;
        public PotionFlat PotionFlatLarge => new PotionFlat(potionFlatLargeItemInfo, potionFlatLargeInfo);

        [SerializeField] private InventoryItemInfo potionMultiplySmallItemInfo;
        public PotionMultiply PotionMultiplySmall => new PotionMultiply(potionMultiplySmallItemInfo, 
            potionMultiplySmallInfo);
        
        [SerializeField] private InventoryItemInfo potionMultiplyMediumItemInfo;
        public PotionMultiply PotionMultiplyMedium => new PotionMultiply(potionMultiplyMediumItemInfo, 
            potionMultiplyMediumInfo);
        
        [SerializeField] private InventoryItemInfo potionMultiplyLargeItemInfo;
        public PotionMultiply PotionMultiplyLarge => new PotionMultiply(potionMultiplyLargeItemInfo, 
            potionMultiplyLargeInfo);
        
        [SerializeField] private InventoryItemInfo potionUltimateItemInfo;
        public PotionUltimate PotionUltimateStrength => new PotionUltimate(potionUltimateItemInfo, 
            potionUltimateStrengthInfo);
        public PotionUltimate PotionUltimateAgility => new PotionUltimate(potionUltimateItemInfo, 
            potionUltimateAgilityInfo);
        public PotionUltimate PotionUltimateIntelligence => new PotionUltimate(potionUltimateItemInfo, 
            potionUltimateIntelligenceInfo);

        [SerializeField] private InventoryItemInfo chocolateItemInfo;
        public Chocolate Chocolate => new Chocolate(chocolateItemInfo, chocolateConsumableBuffInfo);
        
        [Header("Artefact Info")]
        [SerializeField] private ArtefactInfo warHelmetArtefactInfo;
        [Header("Consumable Healing Info")]
        [SerializeField] private ConsumableHealingInfo appleConsumableHealingInfo;
        [Header("Consumable Buff Info")]
        [SerializeField] private ConsumableBuffInfo chocolateConsumableBuffInfo;
        [Header("PotionFlatInfo")]
        [SerializeField] private PotionFlatInfo potionFlatSmallInfo;
        [SerializeField] private PotionFlatInfo potionFlatMediumInfo;
        [SerializeField] private PotionFlatInfo potionFlatLargeInfo;
        [Header("Potion Multiply Info")]
        [SerializeField] private PotionMultiplyInfo potionMultiplySmallInfo;
        [SerializeField] private PotionMultiplyInfo potionMultiplyMediumInfo;
        [SerializeField] private PotionMultiplyInfo potionMultiplyLargeInfo;
        [Header("Potion Ultimate Info")]
        [SerializeField] private PotionUltimateInfo potionUltimateStrengthInfo;
        [SerializeField] private PotionUltimateInfo potionUltimateAgilityInfo;
        [SerializeField] private PotionUltimateInfo potionUltimateIntelligenceInfo;
        [Header("Spells Info")]
        [SerializeField] private SpellInfo fireBallLvl1;
        [SerializeField] private SpellInfo graceBuffLvl1;
        [SerializeField] private SpellInfo snowstormLvl1;
        [SerializeField] private SpellInfo summonSpiderLvl1;
        [SerializeField] private SpellInfo freezingLvl1;
        [Header("Buff Spells Info")] 
        [SerializeField] private BuffSpellInfo graceBuffBuffLvl1;
        [Header("Damage Spells Info")]
        [SerializeField] private DamageSpellInfo fireBallDamageLvl1;
        [Header("Debuff Spells Info")] 
        [SerializeField] private DebuffSpellInfo snowstormDebuffLvl1;
        [Header("Heal Spells Info")] 
        [SerializeField] private HealSpellInfo graceBuffHealLvl1;
        [Header("Periodic Damage Spells Info")] 
        [SerializeField] private PeriodicDamageSpellInfo fireBallPeriodicDamageLvl1;
        [SerializeField] private PeriodicDamageSpellInfo snowstormPeriodicDamageLvl1;
        [Header("Polymorph Spells")] 
        [SerializeField] private PolymorphSpellInfo freezingPolymorphInfoLvl1;
        [Header("Summon Spells Info")]
        [SerializeField] private SummonSpellInfo summonSpiderSummonInfoLvl1;
        
        public List<IInventoryItem> AllItems { get; private set; } = new List<IInventoryItem>();
        public static ItemContainer Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            FillItemsList();
        }

        private void FillItemsList()
        {
            AllItems.Add(new Apple(appleItemInfo, appleConsumableHealingInfo));
            AllItems.Add(new WarHelmet(warHelmetItemInfo, warHelmetArtefactInfo));
        }

        public FireBallItem GetFireBallLvl1()
        {
            return new FireBallItem(fireBallItemInfoLvl1, fireBallLvl1, fireBallDamageLvl1, fireBallPeriodicDamageLvl1);
        }
    }
}