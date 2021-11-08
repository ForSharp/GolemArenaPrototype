using System.Collections.Generic;
using System.Linq;
using FightState;
using GameLoop;
using UnityEngine;

namespace Inventory
{
    public static class ItemDispenser
    {
        public static void DispenseItems()
        {
            foreach (var character in Game.AllGolems)
            {
                DispenseAllTypesOfItemsToCurrentCharacter(character);
            }
        }

        private static void DispenseAllTypesOfItemsToCurrentCharacter(GameCharacterState character)
        {
            var money = character.RoundStatistics.RoundRate * 100 * Game.Round;

            var moneyForArtefacts = money / 4;
            money -= moneyForArtefacts;
            DispenseArtefacts(moneyForArtefacts, character, out var artefactBalance);
            money += artefactBalance;

            var moneyForSpells = money / 3;
            money -= moneyForSpells;
            DispenseSpells(moneyForSpells, character, out var spellsBalance);
            money += spellsBalance;

            var moneyForConsumables = money / 2;
            money -= moneyForConsumables;
            DispenseConsumables(moneyForConsumables, character, out var consumablesBalance);
            money += consumablesBalance;

            DispensePotions(money, character, out var potionsBalance);
        }

        private static void DispenseArtefacts(int moneyForArtefacts, GameCharacterState character, out int artefactBalance)
        {
            const int maxArtefactsCountThatCanGet = 3;
            const int lowestArtefactPrice = 50;
            var index = 0;
            while (index < maxArtefactsCountThatCanGet && moneyForArtefacts >= lowestArtefactPrice)
            {
                var artefactsThatCanGet = ItemContainer.Instance.GetAllArtefacts().Where(item =>
                    item.Info.Price <= moneyForArtefacts).ToList();
                if (artefactsThatCanGet.Count > 0)
                {
                    var artefactItem = artefactsThatCanGet[Random.Range(0, artefactsThatCanGet.Count)];
                    moneyForArtefacts -= artefactItem.Info.Price;
                    character.Items.Add(artefactItem);
                }
                index++;
            }

            artefactBalance = moneyForArtefacts;
        }

        private static void DispenseSpells(int moneyForSpells, GameCharacterState character, out int spellsBalance)
        {
            const int maxSpellsCountThatCanGet = 3;
            const int lowestSpellPrice = 500;
            var index = 0;
            while (index < maxSpellsCountThatCanGet && moneyForSpells >= lowestSpellPrice)
            {
                var spellsThatCanGet = ItemContainer.Instance.GetAllSpellsLvl1().Where(item =>
                    item.Info.Price <= moneyForSpells).ToList();
                if (spellsThatCanGet.Count > 0)
                {
                    var spellItem = spellsThatCanGet[Random.Range(0, spellsThatCanGet.Count)];
                    moneyForSpells -= spellItem.Info.Price;
                    character.Items.Add(spellItem);
                }
                index++;
            }

            spellsBalance = moneyForSpells;
        }
        
        private static void DispenseConsumables(int moneyForConsumables, GameCharacterState character, out int consumablesBalance)
        {
            const int maxConsumablesCountThatCanGet = 10;
            const int lowestConsumablePrice = 10;
            var index = 0;
            while (index < maxConsumablesCountThatCanGet && moneyForConsumables >= lowestConsumablePrice)
            {
                var consumablesThatCanGet = ItemContainer.Instance.GetAllConsumables().Where(item =>
                    item.Info.Price <= moneyForConsumables).ToList();
                if (consumablesThatCanGet.Count > 0)
                {
                    var consumableItem = consumablesThatCanGet[Random.Range(0, consumablesThatCanGet.Count)];
                    moneyForConsumables -= consumableItem.Info.Price;
                    character.Items.Add(consumableItem);
                }
                index++;
            }

            consumablesBalance = moneyForConsumables;
        }
        
        private static void DispensePotions(int moneyForPotions, GameCharacterState character, out int potionsBalance)
        {
            const int lowestPotionPrice = 100;
            while (moneyForPotions >= lowestPotionPrice)
            {
                var potionsThatCanGet = ItemContainer.Instance.GetAllPotions().Where(item =>
                    item.Info.Price <= moneyForPotions).ToList();
                if (potionsThatCanGet.Count > 0)
                {
                    var potionItem = potionsThatCanGet[Random.Range(0, potionsThatCanGet.Count)];
                    moneyForPotions -= potionItem.Info.Price;
                    character.Items.Add(potionItem);
                }
            }

            potionsBalance = moneyForPotions;
        }
    }
}