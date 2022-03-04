﻿using System;
using __Scripts.Inventory.Abstracts;
using Inventory.Abstracts;
using Inventory.Info;

namespace Inventory.Items
{
    public class WarHelmet : IInventoryItem, IArtefactItem
    {
        public IInventoryItemInfo Info { get; }
        public IInventoryItemState State { get; }
        public ArtefactInfo ArtefactInfo { get; }
        public string Id => Info.Id;

        public WarHelmet(IInventoryItemInfo info, ArtefactInfo artefactInfo)
        {
            Info = info;
            ArtefactInfo = artefactInfo;
            State = new InventoryItemState();
        }
        
        public IInventoryItem Clone()
        {
            var clonedItem = new WarHelmet(Info, ArtefactInfo);
            clonedItem.State.Amount = State.Amount;
            return clonedItem;
        }
    }
}