﻿using CharacterEntity.ExtraStats;
using UnityEngine;

namespace Inventory.Info
{
    [CreateAssetMenu(fileName = "ConsumableBuffInfo", menuName = "Gameplay/Items/Create New ConsumableBuffInfo")]
    public class ConsumableBuffInfo : ScriptableObject
    {
        [SerializeField] private ExtraStatsParameter[] affectsExtraStats;
        [SerializeField] private float buffDuration;

        public ExtraStatsParameter[] AffectsExtraStats => affectsExtraStats;
        public float BuffDuration => buffDuration;
    }
}