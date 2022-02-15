using System;
using System.Collections.Generic;
using System.Linq;
using Inventory.Abstracts;
using Inventory.Abstracts.Spells;
using Inventory.Info;
using Inventory.Info.Spells;
using Inventory.Items;
using Inventory.Items.ConsumableItems;
using Inventory.Items.PotionItems;
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
        
        [SerializeField] private InventoryItemInfo fireBallItemInfo;
        public FireBallItem FireBallItemLvl1 => new FireBallItem(fireBallItemInfo, fireBallLvl1, fireBallDamageLvl1,
            fireBallPeriodicDamageLvl1);
        public FireBallItem FireBallItemLvl2 => new FireBallItem(fireBallItemInfo, fireBallLvl2, fireBallDamageLvl2,
            fireBallPeriodicDamageLvl2);
        public FireBallItem FireBallItemLvl3 => new FireBallItem(fireBallItemInfo, fireBallLvl3, fireBallDamageLvl3,
            fireBallPeriodicDamageLvl3);

        [SerializeField] private InventoryItemInfo graceBuffItemInfo;
        public GraceItem GraceItemLvl1 => new GraceItem(graceBuffItemInfo, graceLvl1, 
            graceBuffHealLvl1, graceBuffLvl1);
        public GraceItem GraceItemLvl2 => new GraceItem(graceBuffItemInfo, graceLvl2, 
            graceBuffHealLvl2, graceBuffLvl2);
        public GraceItem GraceItemLvl3 => new GraceItem(graceBuffItemInfo, graceLvl3, 
            graceBuffHealLvl3, graceBuffLvl3);

        [SerializeField] private InventoryItemInfo snowstormItemInfo;
        public SnowstormItem SnowstormItemLvl1 => new SnowstormItem(snowstormItemInfo, snowstormLvl1, 
            snowstormPeriodicDamageLvl1, snowstormDebuffLvl1);
        public SnowstormItem SnowstormItemLvl2 => new SnowstormItem(snowstormItemInfo, snowstormLvl2, 
            snowstormPeriodicDamageLvl2, snowstormDebuffLvl2);
        public SnowstormItem SnowstormItemLvl3 => new SnowstormItem(snowstormItemInfo, snowstormLvl3, 
            snowstormPeriodicDamageLvl3, snowstormDebuffLvl3);

        [SerializeField] private InventoryItemInfo summonSpiderInfo;
        public SummonSpiderItem SummonSpiderItemLvl1 =>
            new SummonSpiderItem(summonSpiderInfo, summonSpiderLvl1, summonSpiderSummonInfoLvl1);
        public SummonSpiderItem SummonSpiderItemLvl2 =>
            new SummonSpiderItem(summonSpiderInfo, summonSpiderLvl2, summonSpiderSummonInfoLvl2);
        public SummonSpiderItem SummonSpiderItemLvl3 =>
            new SummonSpiderItem(summonSpiderInfo, summonSpiderLvl3, summonSpiderSummonInfoLvl3);

        [SerializeField] private InventoryItemInfo freezingItemInfo;
        public FreezingItem FreezingItemLvl1 =>
            new FreezingItem(freezingItemInfo, freezingLvl1, freezingPolymorphInfoLvl1);
        public FreezingItem FreezingItemLvl2 =>
            new FreezingItem(freezingItemInfo, freezingLvl2, freezingPolymorphInfoLvl2);
        public FreezingItem FreezingItemLvl3 =>
            new FreezingItem(freezingItemInfo, freezingLvl3, freezingPolymorphInfoLvl3);
        
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
        [SerializeField] private SpellInfo fireBallLvl2;
        [SerializeField] private SpellInfo fireBallLvl3;
        [SerializeField] private SpellInfo graceLvl1;
        [SerializeField] private SpellInfo graceLvl2;
        [SerializeField] private SpellInfo graceLvl3;
        [SerializeField] private SpellInfo snowstormLvl1;
        [SerializeField] private SpellInfo snowstormLvl2;
        [SerializeField] private SpellInfo snowstormLvl3;
        [SerializeField] private SpellInfo summonSpiderLvl1;
        [SerializeField] private SpellInfo summonSpiderLvl2;
        [SerializeField] private SpellInfo summonSpiderLvl3;
        [SerializeField] private SpellInfo freezingLvl1;
        [SerializeField] private SpellInfo freezingLvl2;
        [SerializeField] private SpellInfo freezingLvl3;
        [Header("Buff Spells Info")] 
        [SerializeField] private BuffSpellInfo graceBuffLvl1;
        [SerializeField] private BuffSpellInfo graceBuffLvl2;
        [SerializeField] private BuffSpellInfo graceBuffLvl3;
        [Header("Damage Spells Info")]
        [SerializeField] private DamageSpellInfo fireBallDamageLvl1;
        [SerializeField] private DamageSpellInfo fireBallDamageLvl2;
        [SerializeField] private DamageSpellInfo fireBallDamageLvl3;
        [Header("Debuff Spells Info")] 
        [SerializeField] private DebuffSpellInfo snowstormDebuffLvl1;
        [SerializeField] private DebuffSpellInfo snowstormDebuffLvl2;
        [SerializeField] private DebuffSpellInfo snowstormDebuffLvl3;
        [Header("Heal Spells Info")] 
        [SerializeField] private HealSpellInfo graceBuffHealLvl1;
        [SerializeField] private HealSpellInfo graceBuffHealLvl2;
        [SerializeField] private HealSpellInfo graceBuffHealLvl3;
        [Header("Periodic Damage Spells Info")] 
        [SerializeField] private PeriodicDamageSpellInfo fireBallPeriodicDamageLvl1;
        [SerializeField] private PeriodicDamageSpellInfo fireBallPeriodicDamageLvl2;
        [SerializeField] private PeriodicDamageSpellInfo fireBallPeriodicDamageLvl3;
        [SerializeField] private PeriodicDamageSpellInfo snowstormPeriodicDamageLvl1;
        [SerializeField] private PeriodicDamageSpellInfo snowstormPeriodicDamageLvl2;
        [SerializeField] private PeriodicDamageSpellInfo snowstormPeriodicDamageLvl3;
        [Header("Polymorph Spells")] 
        [SerializeField] private PolymorphSpellInfo freezingPolymorphInfoLvl1;
        [SerializeField] private PolymorphSpellInfo freezingPolymorphInfoLvl2;
        [SerializeField] private PolymorphSpellInfo freezingPolymorphInfoLvl3;
        [Header("Summon Spells Info")]
        [SerializeField] private SummonSpellInfo summonSpiderSummonInfoLvl1;
        [SerializeField] private SummonSpellInfo summonSpiderSummonInfoLvl2;
        [SerializeField] private SummonSpellInfo summonSpiderSummonInfoLvl3;
        
        public static ItemContainer Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }
        
        public List<IInventoryItem> GetAllItems()
        {
            var allItems = new List<IInventoryItem>();
            allItems.Add(Apple);
            allItems.Add(WarHelmet);
            allItems.Add(Chocolate);
            allItems.Add(FireBallItemLvl1);
            allItems.Add(FreezingItemLvl1);
            allItems.Add(GraceItemLvl1);
            allItems.Add(SnowstormItemLvl1);
            allItems.Add(SummonSpiderItemLvl1);
            allItems.Add(PotionFlatSmall);
            allItems.Add(PotionFlatMedium);
            allItems.Add(PotionFlatLarge);
            allItems.Add(PotionMultiplySmall);
            allItems.Add(PotionMultiplyMedium);
            allItems.Add(PotionMultiplyLarge);
            allItems.Add(PotionUltimateAgility);
            allItems.Add(PotionUltimateIntelligence);
            allItems.Add(PotionUltimateStrength);

            return allItems;

            // return new List<IInventoryItem>()
            // {
            //     Apple,
            //     WarHelmet,
            //     Chocolate,
            //     FireBallItemLvl1,
            //     FreezingItemLvl1,
            //     GraceItemLvl1,
            //     SnowstormItemLvl1,
            //     SummonSpiderItemLvl1,
            //     PotionFlatSmall,
            //     PotionFlatMedium, 
            //     PotionFlatLarge,
            //     PotionMultiplySmall,
            //     PotionMultiplyMedium,
            //     PotionMultiplyLarge,
            //     PotionUltimateAgility,
            //     PotionUltimateIntelligence,
            //     PotionUltimateStrength
            // };
        }
        
        public List<IInventoryItem> GetAllConsumables()
        {
            var allItems = GetAllItems();

            return allItems.Where(item => item is IConsumableBuffItem || item is IConsumableHealingItem).ToList();
        }
        
        public List<IInventoryItem> GetAllArtefacts()
        {
            var allItems = GetAllItems();

            return allItems.Where(item => item is IArtefactItem).ToList();
        }
        
        public List<IInventoryItem> GetAllPotions()
        {
            var allItems = GetAllItems();

            return allItems.Where(item => item is IPotionFlatItem || item is IPotionMultiplyItem || item is IPotionUltimateItem).ToList();
        }
        
        public List<IInventoryItem> GetAllSpellsLvl1()
        {
            var allItems = GetAllItems();
            var allSpells = new List<IInventoryItem>();

            foreach (var item in allItems)
            {
                if (!(item is ISpellItem spell)) continue;
                if (spell.SpellInfo.SpellLvl == 1)
                {
                    allSpells.Add(item);
                }
            }

            return allSpells;
        }

        public List<IInventoryItem> GetAllSpells()
        {
            var allItems = GetAllItems();
            return allItems.Where(item => item is ISpellItem).ToList();
        }

        public FireBallItem GetFireBallLvl1()
        {
            return new FireBallItem(fireBallItemInfo, fireBallLvl1, fireBallDamageLvl1, fireBallPeriodicDamageLvl1);
        }

        public ISpellItem GetUpgradedSpell(ISpellItem learnedSpell, int previousSpellLvl)
        {
            switch (learnedSpell)
            {
                case FireBallItem fireBallItem:
                    if (previousSpellLvl == 1)
                    {
                        return FireBallItemLvl2;
                    }
                    else if (previousSpellLvl == 2)
                    {
                        return FireBallItemLvl3;
                    }
                    break;
                case FreezingItem freezingItem:
                    if (previousSpellLvl == 1)
                    {
                        return FreezingItemLvl2;
                    }
                    else if (previousSpellLvl == 2)
                    {
                        return FreezingItemLvl3;
                    }
                    break;
                case GraceItem graceBuffItem:
                    if (previousSpellLvl == 1)
                    {
                        return GraceItemLvl2;
                    }
                    else if (previousSpellLvl == 2)
                    {
                        return GraceItemLvl3;
                    }
                    break;
                case SnowstormItem snowstormItem:
                    if (previousSpellLvl == 1)
                    {
                        return SnowstormItemLvl2;
                    }
                    else if (previousSpellLvl == 2)
                    {
                        return SnowstormItemLvl3;
                    }
                    break;
                case SummonSpiderItem summonSpiderItem:
                    if (previousSpellLvl == 1)
                    {
                        return SummonSpiderItemLvl2;
                    }
                    else if (previousSpellLvl == 2)
                    {
                        return SummonSpiderItemLvl3;
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(learnedSpell));
            }

            throw new Exception();
        }
    }
}