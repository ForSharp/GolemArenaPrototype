using System.Collections.Generic;
using System.Linq;
using FightState;
using GameLoop;
using UnityEngine;

namespace Inventory
{
    public class ItemDispenser
    {
        private void SetRoundRates()
        {
            var statistics = Game.AllGolems.Select(character => character.RoundStatistics).ToList();

            var sortedStatistics = statistics.OrderBy(stat => stat.RoundDamage).ToList();

            for (var i = 0; i < sortedStatistics.Count; i++)
            {
                sortedStatistics[i].RoundRate += 5;
                sortedStatistics[i].RoundRate += i;
                sortedStatistics[i].RoundRate += sortedStatistics[i].RoundKills;

                if (sortedStatistics[i].WinLastRound)
                {
                    sortedStatistics[i].RoundRate += 3;
                }
            }
        }

        private void DispenseItems()
        {
            foreach (var character in Game.AllGolems)
            {
                var money = character.RoundStatistics.RoundRate * 100;
                
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
                //можно баланс вернуть в раундрейт, поделив на 100, а можно ничего с ним не делать.
                //на зелья нет лимита по предметам, так что их насыпет сполна
            }
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
                var item = artefactsThatCanGet[Random.Range(0, artefactsThatCanGet.Count)];
                moneyForArtefacts -= item.Info.Price;
                character.Items.Add(item);
                index++;
            }

            artefactBalance = moneyForArtefacts;
        }

        private static void DispenseSpells(int moneyForSpells, GameCharacterState character, out int spellsBalance)
        {
            const int maxSpellsCountThatCanGet = 3;
            const int lowestSpellPrice = 50;
            var index = 0;
            while (index < maxSpellsCountThatCanGet && moneyForSpells >= lowestSpellPrice)
            {
                var spellsThatCanGet = ItemContainer.Instance.GetAllSpellsLvl1().Where(item =>
                    item.Info.Price <= moneyForSpells).ToList();
                var item = spellsThatCanGet[Random.Range(0, spellsThatCanGet.Count)];
                moneyForSpells -= item.Info.Price;
                character.Items.Add(item);
                index++;
            }

            spellsBalance = moneyForSpells;
        }
        
        private static void DispenseConsumables(int moneyForConsumables, GameCharacterState character, out int consumablesBalance)
        {
            const int maxConsumablesCountThatCanGet = 10;
            const int lowestConsumablePrice = 50;
            var index = 0;
            while (index < maxConsumablesCountThatCanGet && moneyForConsumables >= lowestConsumablePrice)
            {
                var consumablesThatCanGet = ItemContainer.Instance.GetAllConsumables().Where(item =>
                    item.Info.Price <= moneyForConsumables).ToList();
                var item = consumablesThatCanGet[Random.Range(0, consumablesThatCanGet.Count)];
                moneyForConsumables -= item.Info.Price;
                character.Items.Add(item);
                index++;
            }

            consumablesBalance = moneyForConsumables;
        }
        
        private static void DispensePotions(int moneyForPotions, GameCharacterState character, out int potionsBalance)
        {
            const int lowestSpellPrice = 50;
            while (moneyForPotions >= lowestSpellPrice)
            {
                var potionsThatCanGet = ItemContainer.Instance.GetAllPotions().Where(item =>
                    item.Info.Price <= moneyForPotions).ToList();
                var item = potionsThatCanGet[Random.Range(0, potionsThatCanGet.Count)];
                moneyForPotions -= item.Info.Price;
                character.Items.Add(item);
            }

            potionsBalance = moneyForPotions;
        }
    }
}