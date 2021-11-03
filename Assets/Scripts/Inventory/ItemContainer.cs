using System;
using System.Collections.Generic;
using Inventory.Abstracts;
using Inventory.Info;
using Inventory.Info.Spells;
using Inventory.Items;
using UnityEngine;

namespace Inventory
{
    public class ItemContainer : MonoBehaviour
    {
        [Header("InventoryItemInfo")]
        [SerializeField] private InventoryItemInfo appleItemInfo;
        [SerializeField] private InventoryItemInfo beerItemInfo;
        [SerializeField] private InventoryItemInfo pepperItemInfo;
        [SerializeField] private InventoryItemInfo warHelmetItemInfo;
        [SerializeField] private InventoryItemInfo fireBallItemInfo;
        [Header("ArtefactInfo")]
        [SerializeField] private ArtefactInfo warHelmetArtefactInfo;
        public ArtefactInfo WarHelmetArtefactIn0 => warHelmetArtefactInfo;
        [Header("ConsumableInfo")]
        [SerializeField] private ArtefactInfo _consumableInfo;
        [Header("PotionInfo")]
        [SerializeField] private ArtefactInfo _potionInfo;
        [Header("SpellsInfo")]
        [SerializeField] private SpellInfo fireBallLvl1;
        [Header("BuffSpellsInfo")] 
        [SerializeField] private BuffSpellInfo _dsd;
        [Header("DamageSpellsInfo")]
        [SerializeField] private DamageSpellInfo fireBallDamageLvl1;
        [Header("DebuffSpellsInfo")] 
        [SerializeField] private DebuffSpellInfo _desi;
        [Header("HealSpellsInfo")] 
        [SerializeField] private HealSpellInfo _hsi;
        [Header("PeriodicDamageSpellsInfo")] 
        [SerializeField] private PeriodicDamageSpellInfo fireBallPeriodicDamageLvl1;
        [Header("PolymorphSpells")] 
        [SerializeField] private PolymorphSpellInfo _psi;
        [Header("SummonSpellsInfo")]
        [SerializeField] private SummonSpellInfo _ssi;
        
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
            AllItems.Add(new Apple(appleItemInfo));
            AllItems.Add(new Beer(beerItemInfo));
            AllItems.Add(new Pepper(pepperItemInfo));
            AllItems.Add(new WarHelmet(warHelmetItemInfo, warHelmetArtefactInfo));
        }

        public FireBallItem GetFireBallLvl1()
        {
            return new FireBallItem(fireBallItemInfo, fireBallLvl1, fireBallDamageLvl1, fireBallPeriodicDamageLvl1);
        }
    }
}