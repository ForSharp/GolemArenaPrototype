using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using __Scripts.Inventory;
using __Scripts.Inventory.Abstracts;
using CharacterEntity;
using CharacterEntity.State;
using GameLoop;
using Inventory;
using Optimization;
using UnityEngine;
using Random = UnityEngine.Random;

namespace __Scripts.GameLoop
{
    public static class Game
    {
        public enum GameStage
        {
            MainMenu,
            Battle,
            BetweenBattles
        }
        
        public static GameStage Stage { get; set; }
        public static List<CharacterState> AllCharactersInSession { get; }
        public static List<ChampionState> AllChampionsInSession { get; }
        public static List<string> FreeTypes { get; }
        public static List<string> FreeSpecs { get; }
        
        public static int Round { get; private set; }

        public static bool RoundEnded { get; private set; }
        public static event Action StartBattle;
        public static event Action OpenMainMenu;
        public static event Action EndGame;

        private const int MaxRoundNumber = 4;

        static Game()
        {
            AllCharactersInSession = new List<CharacterState>();
            AllChampionsInSession = new List<ChampionState>();
            FreeTypes = Enum.GetNames(typeof(CharacterType)).Reverse().Skip(1).Reverse().ToList();
            FreeSpecs = Enum.GetNames(typeof(Specialization)).Reverse().Skip(1).Reverse().ToList();

            Stage = GameStage.MainMenu;
            
            EventContainer.PlayerCharacterCreated += CreateBotCharacters;
            EventContainer.WinBattle += SetEndOfRound;
            //EventContainer.NewRound += PrepareNewRound;

            Round = 1;
        }

        public static void OnOpenMainMenu()
        {
            OpenMainMenu?.Invoke();
        }

        public static void AddCharacterToAllCharactersList(CharacterState character)
        {
            AllCharactersInSession.Add(character);

            if (character is ChampionState champion)
            {
                AllChampionsInSession.Add(champion);
                FreeTypes.Remove(champion.Type);
                FreeSpecs.Remove(champion.Spec);
            }
            
        }

        public static void RemoveDeadCreep(CharacterState creep)
        {
            AllCharactersInSession.Remove(creep);
        }

        public static event Action AllChampionsAreReady;
        
        private static void CreateBotCharacters()
        {
            for (var i = 0; i < 4; i++)
            {
                Spawner.Instance.SpawnChampion(GetRandomCharacter(), GetRandomSpecialization());
            }
            
            HeroViewBoxController.Instance.DeactivateRedundantBoxes();

            AllChampionsAreReady?.Invoke();
        }

        private static void EndCurrentGame()
        {
            OnEndGame();
            Stage = GameStage.MainMenu;
        }

        private static IEnumerator PrepareNewRound()
        {
            yield return new WaitForSeconds(5);

            foreach (var character in AllChampionsInSession)
            {
                character.gameObject.SetActive(true);
            }
            
            SetRoundRates();
            ItemDispenser.DispenseItemsRoundRewards();

            foreach (var character in AllChampionsInSession)
            {
                if (character != Player.PlayerCharacter)
                {
                    //ItemOutfitter.EquipItems(character);
                    //PotionDrinker.DrinkAllPotions(character);
                }
                ItemOutfitter.EquipItems(character);
                PotionDrinker.DrinkAllPotions(character);
            }
            
            foreach (var character in AllCharactersInSession)
            {
                if (character is ChampionState champion)
                    champion.PrepareAfterNewRound();
                else
                {
                    //Object.Destroy(character.gameObject);
                }
            }
            
            OnClearEffects();
            
            
            
            CoroutineManager.StartRoutine(StartBattleAfterDelay(10));
        }

        private static void ActivateChampionIfNeed()
        {
            foreach (var champion in AllChampionsInSession)
            {
                if (!champion.gameObject.activeSelf)
                {
                    champion.gameObject.SetActive(true);
                    champion.PrepareAfterNewRoundForException();
                }
            }
            
        }

        private static IEnumerator StartBattleAfterDelay(float delay)
        {
            yield return new WaitForSeconds(delay);
            
            ActivateChampionIfNeed();
            
            OnStartBattle();
        }
        
        public static void OnStartBattle()
        {
            StartBattle?.Invoke();
            Stage = GameStage.Battle;
            RoundEnded = false;
            
        }
        
        public static event Action ClearEffects;

        private static void OnClearEffects()
        {
            ClearEffects?.Invoke();
        }

        private static void SetEndOfRound(CharacterState winner)
        {
            RoundEnded = true;
            Stage = GameStage.BetweenBattles;
            Round++;

            if (Round > MaxRoundNumber)
            {
                EndCurrentGame();
            }
            else
            {
                CoroutineManager.StartRoutine(PrepareNewRound());
            }
            
        }

        private static void SetRoundRates()
        {
            
            var statistics = AllChampionsInSession.Select(character => character.RoundStatistics).ToList();

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
        
        public static IEnumerable<CharacterState> GetEnemies(CharacterState state)
        {
            return AllCharactersInSession.Where(character => !character.IsDead)
                .Where(group => group.Group != state.Group);
        }

        public static IEnumerable<CharacterState> GetFriends(CharacterState state)
        {
            return AllCharactersInSession.Where(character => !character.IsDead)
                .Where(group => group.Group == state.Group);
        }

        public static CharacterType GetRandomCharacter()
        {
            return (CharacterType) ToEnum(FreeTypes[Random.Range(0, FreeTypes.Count)], typeof(CharacterType));
        }

        public static Specialization GetRandomSpecialization()
        {
            return (Specialization) ToEnum(FreeSpecs[Random.Range(0, FreeSpecs.Count)],
                typeof(Specialization));
        }

        public static ChampionState GetCharacterByInventory(IInventory inventory)
        {
            return AllChampionsInSession.Find(character =>
                character.InventoryHelper.InventoryOrganization.inventory == inventory);
        }
        
        public static Enum ToEnum(string value, Type enumType)
        {
            return (Enum) Enum.Parse(enumType, value, true);
        }

        private static void OnEndGame()
        {
            EndGame?.Invoke();
        }

        
    }
}