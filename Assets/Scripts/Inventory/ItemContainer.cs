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

        [SerializeField] private InventoryItemInfo graceBuffItemInfo;
        public GraceItem GraceItemLvl1 => new GraceItem(graceBuffItemInfo, graceLvl1, 
            graceBuffHealLvl1, graceBuffLvl1);

        [SerializeField] private InventoryItemInfo snowstormItemInfo;
        public SnowstormItem SnowstormItemLvl1 => new SnowstormItem(snowstormItemInfo, snowstormLvl1, 
            snowstormPeriodicDamageLvl1, snowstormDebuffLvl1);

        [SerializeField] private InventoryItemInfo summonSpiderInfo;
        public SummonSpiderItem SummonSpiderItemLvl1 =>
            new SummonSpiderItem(summonSpiderInfo, summonSpiderLvl1, summonSpiderSummonInfoLvl1);

        [SerializeField] private InventoryItemInfo freezingItemInfo;
        public FreezingItem FreezingItemLvl1 =>
            new FreezingItem(freezingItemInfo, freezingLvl1, freezingPolymorphInfoLvl1);
        
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
        [SerializeField] private SpellInfo graceLvl1;
        [SerializeField] private SpellInfo snowstormLvl1;
        [SerializeField] private SpellInfo summonSpiderLvl1;
        [SerializeField] private SpellInfo freezingLvl1;
        [Header("Buff Spells Info")] 
        [SerializeField] private BuffSpellInfo graceBuffLvl1;
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
        
        public static ItemContainer Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }
        
        private List<IInventoryItem> GetAllItems()
        {
            return new List<IInventoryItem>()
            {
                Apple,
                WarHelmet,
                Chocolate,
                FireBallItemLvl1,
                FreezingItemLvl1,
                GraceItemLvl1,
                SnowstormItemLvl1,
                SummonSpiderItemLvl1,
                PotionFlatSmall,
                PotionFlatMedium, 
                PotionFlatLarge,
                PotionMultiplySmall,
                PotionMultiplyMedium,
                PotionMultiplyLarge,
                PotionUltimateAgility,
                PotionUltimateIntelligence,
                PotionUltimateStrength
            };
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

        public static ISpellItem GetUpgradedSpell(ISpellItem learnedSpell, int previousSpellLvl)
        {
            switch (learnedSpell)
            {
                case FireBallItem fireBallItem:
                    if (previousSpellLvl == 1)
                    {
                        //lvl2
                    }
                    else
                    {
                        //lvl3
                    }
                    break;
                case FreezingItem freezingItem:
                    break;
                case GraceItem graceBuffItem:
                    break;
                case SnowstormItem snowstormItem:
                    break;
                case SummonSpiderItem summonSpiderItem:
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(learnedSpell));
            }

            throw new Exception();
        }
    }
}