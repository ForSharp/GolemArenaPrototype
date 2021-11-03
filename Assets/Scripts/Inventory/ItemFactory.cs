using System;
using System.Collections.Generic;
using Inventory.Abstracts;
using UnityEngine;

namespace Inventory
{
    public class ItemFactory 
    {
        public List<TInventoryItem> CreateItemsOfTypeForCost<TInventoryItem>(int moneyAmount, out int moneyBalance) where TInventoryItem : IInventoryItem //ItemDispenser посчитает на какую сумму нужно выдать айтемы и вызовет этот метод
        {
            //клиент обладает суммой денег, он под каждый тип предметов выделит часть от этой суммы. на эту сумму он получит рандомных предметов указанного типа. остаток вернется.
            //количество полученных в итоге предметов неограниченно
            
            var items = new List<TInventoryItem>();
            
            //нужно осуществить поиск по всем предметам указанного типа. выбрать рандомный. для первого поиска можно установить минимальную планку в размере 50-80% от moneyAmount
            
            moneyBalance = 0;
            return items;
        }
        
        public List<TInventoryItem> CreateItemsOfTypeForCost<TInventoryItem>(int itemsLimit, int moneyAmount, out int moneyBalance) where TInventoryItem : IInventoryItem //ItemDispenser посчитает на какую сумму нужно выдать айтемы и вызовет этот метод
        {
            //клиент обладает суммой денег, он под каждый тип предметов выделит часть от этой суммы. на эту сумму он получит рандомных предметов указанного типа. остаток вернется.
            //количество полученных в итоге предметов ограниченно itemsLimit (чтобы не накапливалось много ненужных дешевых предметов)
            
            var items = new List<TInventoryItem>();
            
            
            
            moneyBalance = 0;
            return items;
        }
    }
}