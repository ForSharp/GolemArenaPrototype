using System;
using System.Collections.Generic;
using System.Linq;
using __Scripts.Inventory.Abstracts;
using __Scripts.Inventory.Abstracts.Spells;
using __Scripts.Inventory.Info;
using __Scripts.Inventory.Info.Spells;
using __Scripts.Inventory.Items.ArtefactItems;
using __Scripts.Inventory.Items.ConsumableItems;
using __Scripts.Inventory.Items.PotionItems;
using __Scripts.Inventory.Items.SpellItems;
using UnityEngine;

namespace __Scripts.Inventory
{
    public class ItemContainer : MonoBehaviour
    {
        [Header("Inventory Item Info")] [SerializeField]
        private InventoryItemInfo clothChestItemInfo;

        public ArtefactItem ClothChestItem => new ArtefactItem(clothChestItemInfo, clothChestArtefactInfo);

        [SerializeField] private InventoryItemInfo leatherChestItemInfo;
        public ArtefactItem LeatherChestItem => new ArtefactItem(leatherChestItemInfo, leatherChestArtefactInfo);

        [SerializeField] private InventoryItemInfo plateChestItemInfo;
        public ArtefactItem PlateChestItem => new ArtefactItem(plateChestItemInfo, plateChestArtefactInfo);

        [SerializeField] private InventoryItemInfo clothCloakItemInfo;
        public ArtefactItem ClothCloakItem => new ArtefactItem(clothCloakItemInfo, clothCloakArtefactInfo);

        [SerializeField] private InventoryItemInfo leatherCloakItemInfo;
        public ArtefactItem LeatherCloakItem => new ArtefactItem(leatherCloakItemInfo, leatherCloakArtefactInfo);

        [SerializeField] private InventoryItemInfo plateCloakItemInfo;
        public ArtefactItem PlateCloakItem => new ArtefactItem(plateCloakItemInfo, plateCloakArtefactInfo);

        [SerializeField] private InventoryItemInfo plateShoulderItemInfo;
        public ArtefactItem PlateShoulderItem => new ArtefactItem(plateShoulderItemInfo, plateShoulderArtefactInfo);

        [SerializeField] private InventoryItemInfo clothHelmItemInfo;
        public ArtefactItem ClothHelmItem => new ArtefactItem(clothHelmItemInfo, clothHelmArtefactInfo);

        [SerializeField] private InventoryItemInfo leatherHelmItemInfo;
        public ArtefactItem LeatherHelmItem => new ArtefactItem(leatherHelmItemInfo, leatherHelmArtefactInfo);

        [SerializeField] private InventoryItemInfo plateHelmItemInfo;
        public ArtefactItem PlateHelmItem => new ArtefactItem(plateHelmItemInfo, plateHelmArtefactInfo);

        [SerializeField] private InventoryItemInfo clothGlovesItemInfo;
        public ArtefactItem ClothGlovesItem => new ArtefactItem(clothGlovesItemInfo, clothGlovesArtefactInfo);

        [SerializeField] private InventoryItemInfo leatherGlovesItemInfo;
        public ArtefactItem LeatherGlovesItem => new ArtefactItem(leatherGlovesItemInfo, leatherGlovesArtefactInfo);

        [SerializeField] private InventoryItemInfo plateGlovesItemInfo;
        public ArtefactItem PlateGlovesItem => new ArtefactItem(plateGlovesItemInfo, plateGlovesArtefactInfo);

        [SerializeField] private InventoryItemInfo clothBootsItemInfo;
        public ArtefactItem ClothBootsItem => new ArtefactItem(clothBootsItemInfo, clothBootsArtefactInfo);

        [SerializeField] private InventoryItemInfo leatherBootsItemInfo;
        public ArtefactItem LeatherBootsItem => new ArtefactItem(leatherBootsItemInfo, leatherBootsArtefactInfo);

        [SerializeField] private InventoryItemInfo plateBootsItemInfo;
        public ArtefactItem PlateBootsItem => new ArtefactItem(plateBootsItemInfo, plateBootsArtefactInfo);

        [SerializeField] private InventoryItemInfo clothPantsItemInfo;
        public ArtefactItem ClothPantsItem => new ArtefactItem(clothPantsItemInfo, clothPantsArtefactInfo);

        [SerializeField] private InventoryItemInfo leatherPantsItemInfo;
        public ArtefactItem LeatherPantsItem => new ArtefactItem(leatherPantsItemInfo, leatherPantsArtefactInfo);

        [SerializeField] private InventoryItemInfo platePantsItemInfo;
        public ArtefactItem PlatePantsItem => new ArtefactItem(platePantsItemInfo, platePantsArtefactInfo);

        [SerializeField] private InventoryItemInfo clothChestEItemInfo;
        public ArtefactItem ClothChestEItem => new ArtefactItem(clothChestEItemInfo, clothChestEArtefactInfo);

        [SerializeField] private InventoryItemInfo leatherChestEItemInfo;
        public ArtefactItem LeatherChestEItem => new ArtefactItem(leatherChestEItemInfo, leatherChestEArtefactInfo);

        [SerializeField] private InventoryItemInfo plateChestEItemInfo;
        public ArtefactItem PlateChestEItem => new ArtefactItem(plateChestEItemInfo, plateChestEArtefactInfo);

        [SerializeField] private InventoryItemInfo clothCloakEItemInfo;
        public ArtefactItem ClothCloakEItem => new ArtefactItem(clothCloakEItemInfo, clothCloakEArtefactInfo);

        [SerializeField] private InventoryItemInfo leatherCloakEItemInfo;
        public ArtefactItem LeatherCloakEItem => new ArtefactItem(leatherCloakEItemInfo, leatherCloakEArtefactInfo);

        [SerializeField] private InventoryItemInfo plateCloakEItemInfo;
        public ArtefactItem PlateCloakEItem => new ArtefactItem(plateCloakEItemInfo, plateCloakEArtefactInfo);

        [SerializeField] private InventoryItemInfo plateShoulderEItemInfo;
        public ArtefactItem PlateShoulderEItem => new ArtefactItem(plateShoulderEItemInfo, plateShoulderEArtefactInfo);

        [SerializeField] private InventoryItemInfo clothHelmEItemInfo;
        public ArtefactItem ClothHelmEItem => new ArtefactItem(clothHelmEItemInfo, clothHelmEArtefactInfo);

        [SerializeField] private InventoryItemInfo leatherHelmEItemInfo;
        public ArtefactItem LeatherHelmEItem => new ArtefactItem(leatherHelmEItemInfo, leatherHelmEArtefactInfo);

        [SerializeField] private InventoryItemInfo plateHelmEItemInfo;
        public ArtefactItem PlateHelmEItem => new ArtefactItem(plateHelmEItemInfo, plateHelmEArtefactInfo);

        [SerializeField] private InventoryItemInfo clothGlovesEItemInfo;
        public ArtefactItem ClothGlovesEItem => new ArtefactItem(clothGlovesEItemInfo, clothGlovesEArtefactInfo);

        [SerializeField] private InventoryItemInfo leatherGlovesEItemInfo;
        public ArtefactItem LeatherGlovesEItem => new ArtefactItem(leatherGlovesEItemInfo, leatherGlovesEArtefactInfo);

        [SerializeField] private InventoryItemInfo plateGlovesEItemInfo;
        public ArtefactItem PlateGlovesEItem => new ArtefactItem(plateGlovesEItemInfo, plateGlovesEArtefactInfo);

        [SerializeField] private InventoryItemInfo clothBootsEItemInfo;
        public ArtefactItem ClothBootsEItem => new ArtefactItem(clothBootsEItemInfo, clothBootsEArtefactInfo);

        [SerializeField] private InventoryItemInfo leatherBootsEItemInfo;
        public ArtefactItem LeatherBootsEItem => new ArtefactItem(leatherBootsEItemInfo, leatherBootsEArtefactInfo);

        [SerializeField] private InventoryItemInfo plateBootsEItemInfo;
        public ArtefactItem PlateBootsEItem => new ArtefactItem(plateBootsEItemInfo, plateBootsEArtefactInfo);

        [SerializeField] private InventoryItemInfo clothPantsEItemInfo;
        public ArtefactItem ClothPantsEItem => new ArtefactItem(clothPantsEItemInfo, clothPantsEArtefactInfo);

        [SerializeField] private InventoryItemInfo leatherPantsEItemInfo;
        public ArtefactItem LeatherPantsEItem => new ArtefactItem(leatherPantsEItemInfo, leatherPantsEArtefactInfo);

        [SerializeField] private InventoryItemInfo platePantsEItemInfo;
        public ArtefactItem PlatePantsEItem => new ArtefactItem(platePantsEItemInfo, platePantsEArtefactInfo);

        [SerializeField] private InventoryItemInfo immortalChestItemInfo;
        public ArtefactItem ImmortalChestItem => new ArtefactItem(immortalChestItemInfo, immortalChestArtefactInfo);

        [SerializeField] private InventoryItemInfo immortalHelmItemInfo;
        public ArtefactItem ImmortalHelmItem => new ArtefactItem(immortalHelmItemInfo, immortalHelmArtefactInfo);

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

        [SerializeField] private InventoryItemInfo appleItemInfo;
        public ConsumableHealingItem Apple => new ConsumableHealingItem(appleItemInfo, appleConsumableHealingInfo);

        [SerializeField] private InventoryItemInfo chocolateItemInfo;
        public ConsumableBuffItem Chocolate => new ConsumableBuffItem(chocolateItemInfo, chocolateConsumableBuffInfo);
        
        [SerializeField] private InventoryItemInfo hamItemInfo;
        public ConsumableBuffItem Ham => new ConsumableBuffItem(hamItemInfo, hamConsumableBuffInfo);
        
        [SerializeField] private InventoryItemInfo mushroomItemInfo;
        public ConsumableBuffItem Mushroom => new ConsumableBuffItem(mushroomItemInfo, mushroomConsumableBuffInfo);
        
        [SerializeField] private InventoryItemInfo carrotItemInfo;
        public ConsumableBuffItem Carrot => new ConsumableBuffItem(carrotItemInfo, carrotConsumableBuffInfo);
        
        [SerializeField] private InventoryItemInfo avocadoItemInfo;
        public ConsumableBuffItem Avocado => new ConsumableBuffItem(avocadoItemInfo, avocadoConsumableBuffInfo);
        
        [SerializeField] private InventoryItemInfo cheesecakeItemInfo;
        public ConsumableBuffItem Cheesecake => new ConsumableBuffItem(cheesecakeItemInfo, cheesecakeConsumableBuffInfo);
        
        [SerializeField] private InventoryItemInfo cheeseItemInfo;
        public ConsumableBuffItem Cheese => new ConsumableBuffItem(cheeseItemInfo, cheeseConsumableBuffInfo);
        
        [SerializeField] private InventoryItemInfo chickenItemInfo;
        public ConsumableBuffItem Chicken => new ConsumableBuffItem(chickenItemInfo, chickenConsumableBuffInfo);
        
        [SerializeField] private InventoryItemInfo cakeItemInfo;
        public ConsumableBuffItem Cake => new ConsumableBuffItem(cakeItemInfo, cakeConsumableBuffInfo);
        
        [SerializeField] private InventoryItemInfo cookiesItemInfo;
        public ConsumableBuffItem Cookies => new ConsumableBuffItem(cookiesItemInfo, cookiesConsumableBuffInfo);
        
        [SerializeField] private InventoryItemInfo cornItemInfo;
        public ConsumableBuffItem Corn => new ConsumableBuffItem(cornItemInfo, cornConsumableBuffInfo);
        
        [SerializeField] private InventoryItemInfo croissantItemInfo;
        public ConsumableBuffItem Croissant => new ConsumableBuffItem(croissantItemInfo, croissantConsumableBuffInfo);
        
        [SerializeField] private InventoryItemInfo grapesItemInfo;
        public ConsumableBuffItem Grapes => new ConsumableBuffItem(grapesItemInfo, grapesConsumableBuffInfo);
        
        [SerializeField] private InventoryItemInfo schnitzelItemInfo;
        public ConsumableBuffItem Schnitzel => new ConsumableBuffItem(schnitzelItemInfo, schnitzelConsumableBuffInfo);
        
        [SerializeField] private InventoryItemInfo amanitaItemInfo;
        public ConsumableBuffItem Amanita => new ConsumableBuffItem(amanitaItemInfo, amanitaConsumableBuffInfo);
        
        [SerializeField] private InventoryItemInfo kiwiItemInfo;
        public ConsumableBuffItem Kiwi => new ConsumableBuffItem(kiwiItemInfo, kiwiConsumableBuffInfo);
        
        [SerializeField] private InventoryItemInfo redPepperItemInfo;
        public ConsumableBuffItem RedPepper => new ConsumableBuffItem(redPepperItemInfo, redPepperConsumableBuffInfo);
        
        [SerializeField] private InventoryItemInfo iceCreamItemInfo;
        public ConsumableBuffItem IceCream => new ConsumableBuffItem(iceCreamItemInfo, iceCreamConsumableBuffInfo);
        
        [SerializeField] private InventoryItemInfo borthItemInfo;
        public ConsumableBuffItem Borth => new ConsumableBuffItem(borthItemInfo, borthConsumableBuffInfo);
        
        [SerializeField] private InventoryItemInfo honeyItemInfo;
        public ConsumableBuffItem Honey => new ConsumableBuffItem(honeyItemInfo, honeyConsumableBuffInfo);
        
        [SerializeField] private InventoryItemInfo pieItemInfo;
        public ConsumableBuffItem Pie => new ConsumableBuffItem(pieItemInfo, pieConsumableBuffInfo);
        
        [SerializeField] private InventoryItemInfo sausageItemInfo;
        public ConsumableBuffItem Sausage => new ConsumableBuffItem(sausageItemInfo, sausageConsumableBuffInfo);
        
        
        [Header("Artefact Info")] 
        [SerializeField] private ArtefactInfo clothChestArtefactInfo;
        [SerializeField] private ArtefactInfo leatherChestArtefactInfo;
        [SerializeField] private ArtefactInfo plateChestArtefactInfo;
        [SerializeField] private ArtefactInfo clothCloakArtefactInfo;
        [SerializeField] private ArtefactInfo leatherCloakArtefactInfo;
        [SerializeField] private ArtefactInfo plateCloakArtefactInfo;
        [SerializeField] private ArtefactInfo plateShoulderArtefactInfo;
        [SerializeField] private ArtefactInfo clothHelmArtefactInfo;
        [SerializeField] private ArtefactInfo leatherHelmArtefactInfo;
        [SerializeField] private ArtefactInfo plateHelmArtefactInfo;
        [SerializeField] private ArtefactInfo clothGlovesArtefactInfo;
        [SerializeField] private ArtefactInfo leatherGlovesArtefactInfo;
        [SerializeField] private ArtefactInfo plateGlovesArtefactInfo;
        [SerializeField] private ArtefactInfo clothBootsArtefactInfo;
        [SerializeField] private ArtefactInfo leatherBootsArtefactInfo;
        [SerializeField] private ArtefactInfo plateBootsArtefactInfo;
        [SerializeField] private ArtefactInfo clothPantsArtefactInfo;
        [SerializeField] private ArtefactInfo leatherPantsArtefactInfo;
        [SerializeField] private ArtefactInfo platePantsArtefactInfo;
        [SerializeField] private ArtefactInfo clothChestEArtefactInfo;
        [SerializeField] private ArtefactInfo leatherChestEArtefactInfo;
        [SerializeField] private ArtefactInfo plateChestEArtefactInfo;
        [SerializeField] private ArtefactInfo clothCloakEArtefactInfo;
        [SerializeField] private ArtefactInfo leatherCloakEArtefactInfo;
        [SerializeField] private ArtefactInfo plateCloakEArtefactInfo;
        [SerializeField] private ArtefactInfo plateShoulderEArtefactInfo;
        [SerializeField] private ArtefactInfo clothHelmEArtefactInfo;
        [SerializeField] private ArtefactInfo leatherHelmEArtefactInfo;
        [SerializeField] private ArtefactInfo plateHelmEArtefactInfo;
        [SerializeField] private ArtefactInfo clothGlovesEArtefactInfo;
        [SerializeField] private ArtefactInfo leatherGlovesEArtefactInfo;
        [SerializeField] private ArtefactInfo plateGlovesEArtefactInfo;
        [SerializeField] private ArtefactInfo clothBootsEArtefactInfo;
        [SerializeField] private ArtefactInfo leatherBootsEArtefactInfo;
        [SerializeField] private ArtefactInfo plateBootsEArtefactInfo;
        [SerializeField] private ArtefactInfo clothPantsEArtefactInfo;
        [SerializeField] private ArtefactInfo leatherPantsEArtefactInfo;
        [SerializeField] private ArtefactInfo platePantsEArtefactInfo;
        [SerializeField] private ArtefactInfo immortalChestArtefactInfo;
        [SerializeField] private ArtefactInfo immortalHelmArtefactInfo;

        [Header("Consumable Healing Info")] 
        [SerializeField] private ConsumableHealingInfo appleConsumableHealingInfo;

        [Header("Consumable Buff Info")] 
        [SerializeField] private ConsumableBuffInfo chocolateConsumableBuffInfo;
        [SerializeField] private ConsumableBuffInfo hamConsumableBuffInfo;
        [SerializeField] private ConsumableBuffInfo mushroomConsumableBuffInfo;
        [SerializeField] private ConsumableBuffInfo carrotConsumableBuffInfo;
        [SerializeField] private ConsumableBuffInfo avocadoConsumableBuffInfo;
        [SerializeField] private ConsumableBuffInfo cheesecakeConsumableBuffInfo;
        [SerializeField] private ConsumableBuffInfo cheeseConsumableBuffInfo;
        [SerializeField] private ConsumableBuffInfo chickenConsumableBuffInfo;
        [SerializeField] private ConsumableBuffInfo cakeConsumableBuffInfo;
        [SerializeField] private ConsumableBuffInfo cookiesConsumableBuffInfo;
        [SerializeField] private ConsumableBuffInfo cornConsumableBuffInfo;
        [SerializeField] private ConsumableBuffInfo croissantConsumableBuffInfo;
        [SerializeField] private ConsumableBuffInfo grapesConsumableBuffInfo;
        [SerializeField] private ConsumableBuffInfo schnitzelConsumableBuffInfo;
        [SerializeField] private ConsumableBuffInfo amanitaConsumableBuffInfo;
        [SerializeField] private ConsumableBuffInfo kiwiConsumableBuffInfo;
        [SerializeField] private ConsumableBuffInfo redPepperConsumableBuffInfo;
        [SerializeField] private ConsumableBuffInfo iceCreamConsumableBuffInfo;
        [SerializeField] private ConsumableBuffInfo borthConsumableBuffInfo;
        [SerializeField] private ConsumableBuffInfo honeyConsumableBuffInfo;
        [SerializeField] private ConsumableBuffInfo pieConsumableBuffInfo;
        [SerializeField] private ConsumableBuffInfo sausageConsumableBuffInfo;
        
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
            return new List<IInventoryItem>()
            {
                ClothChestItem,
                LeatherChestItem,
                PlateChestItem,
                ClothCloakItem,
                LeatherCloakItem,
                PlateCloakItem,
                PlateShoulderItem,
                ClothHelmItem,
                LeatherHelmItem,
                PlateHelmItem,
                ClothGlovesItem,
                LeatherGlovesItem,
                PlateGlovesItem,
                ClothBootsItem,
                LeatherBootsItem,
                PlateBootsItem,
                ClothPantsItem,
                LeatherPantsItem,
                PlatePantsItem,
                ClothChestEItem,
                LeatherChestEItem,
                PlateChestEItem,
                ClothCloakEItem,
                LeatherCloakEItem,
                PlateCloakEItem,
                PlateShoulderEItem,
                ClothHelmEItem,
                LeatherHelmEItem,
                PlateHelmEItem,
                ClothGlovesEItem,
                LeatherGlovesEItem,
                PlateGlovesEItem,
                ClothBootsEItem,
                LeatherBootsEItem,
                PlateBootsEItem,
                ClothPantsEItem,
                LeatherPantsEItem,
                PlatePantsEItem,
                ImmortalChestItem,
                ImmortalHelmItem,

                FireBallItemLvl1,
                FreezingItemLvl1,
                GraceItemLvl1,
                SnowstormItemLvl1,
                SummonSpiderItemLvl1,
                
                //PotionFlatSmall,
                //PotionFlatMedium, 
                //PotionFlatLarge,
                PotionMultiplySmall,
                PotionMultiplyMedium,
                PotionMultiplyLarge,
                PotionUltimateAgility,
                PotionUltimateIntelligence,
                PotionUltimateStrength,
                
                Apple,
                Chocolate,
                Ham,
                Mushroom,
                Carrot,
                Avocado,
                Cheesecake,
                Cheese,
                Chicken,
                Cake,
                Cookies,
                Corn,
                Croissant,
                Grapes,
                Schnitzel,
                Amanita,
                Kiwi,
                RedPepper,
                IceCream,
                Borth,
                Honey,
                Pie,
                Sausage
                
            };
        }

        public IInventoryItem GetItemById(string itemId)
        {
            var allItems = GetAllItems();
            return allItems.FirstOrDefault(item => item.Info.Id == itemId);
        }

        public List<IInventoryItem> GetAllConsumables()
        {
            var allItems = GetAllItems();

            //return allItems.Where(item => item.Info.ItemType == ItemType.Consumable).ToList();

            return allItems.Where(item => item is IConsumableBuffItem || item is IConsumableHealingItem).ToList();
        }

        public List<IInventoryItem> GetAllArtefacts()
        {
            var allItems = GetAllItems();

            //return allItems.Where(item => item.Info.ItemType == ItemType.Artefact).ToList();
            
            return allItems.Where(item => item is IArtefactItem).ToList();
        }

        public List<IInventoryItem> GetAllPotions()
        {
            var allItems = GetAllItems();
            
            //return allItems.Where(item => item.Info.ItemType == ItemType.Potion).ToList();
            return allItems.Where(item =>
                item is IPotionFlatItem || item is IPotionMultiplyItem || item is IPotionUltimateItem).ToList();
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

        public ISpellItem GetUpgradedSpell(ISpellItem learnedSpell, int previousSpellLvl)
        {
            switch (learnedSpell)
            {
                case FireBallItem _:
                    if (previousSpellLvl == 1)
                    {
                        return FireBallItemLvl2;
                    }
                    else if (previousSpellLvl == 2)
                    {
                        return FireBallItemLvl3;
                    }

                    break;
                case FreezingItem _:
                    if (previousSpellLvl == 1)
                    {
                        return FreezingItemLvl2;
                    }
                    else if (previousSpellLvl == 2)
                    {
                        return FreezingItemLvl3;
                    }

                    break;
                case GraceItem _:
                    if (previousSpellLvl == 1)
                    {
                        return GraceItemLvl2;
                    }
                    else if (previousSpellLvl == 2)
                    {
                        return GraceItemLvl3;
                    }

                    break;
                case SnowstormItem _:
                    if (previousSpellLvl == 1)
                    {
                        return SnowstormItemLvl2;
                    }
                    else if (previousSpellLvl == 2)
                    {
                        return SnowstormItemLvl3;
                    }

                    break;
                case SummonSpiderItem _:
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