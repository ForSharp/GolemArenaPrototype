using System;
using System.Collections.Generic;
using System.Linq;
using CharacterEntity;
using CharacterEntity.CharacterState;
using CharacterEntity.State;
using Inventory;
using Inventory.Abstracts;
using Optimization;
using Random = UnityEngine.Random;

namespace GameLoop
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
        public static List<string> FreeTypes { get; }
        public static List<string> FreeSpecs { get; }
        
        public static int Round { get; private set; }

        public static bool RoundEnded { get; private set; }
        public static event Action StartBattle;
        public static event Action OpenMainMenu;
        public static event Action EndGame;

        private const int MAXRoundNumber = 4;

        static Game()
        {
            AllCharactersInSession = new List<CharacterState>();
            FreeTypes = Enum.GetNames(typeof(CharacterType)).ToList();
            FreeSpecs = Enum.GetNames(typeof(Specialization)).ToList();

            Stage = GameStage.MainMenu;
            
            EventContainer.PlayerCharacterCreated += CreateBotCharacters;
            EventContainer.WinBattle += SetEndOfRound;
            EventContainer.NewRound += PrepareNewRound;

            Round = 1;
        }

        public static void OnStartBattle()
        {
            StartBattle?.Invoke();
            Stage = GameStage.Battle;
            RoundEnded = false;
        }
        
        public static void OnOpenMainMenu()
        {
            OpenMainMenu?.Invoke();
        }
        
        public static void AddCharacterToAllCharactersList(CharacterState golem)
        {
            AllCharactersInSession.Add(golem);
            FreeTypes.Remove(golem.Type);
            FreeSpecs.Remove(golem.Spec);
        }

        private static void CreateBotCharacters()
        {
            for (var i = 0; i < 4; i++)
            {
                Spawner.Instance.SpawnGolem(GetRandomCharacter(), GetRandomSpecialization());
            }
            
            HeroViewBoxController.Instance.DeactivateRedundantBoxes();
        }

        private static void PrepareNewRound()
        {
            if (Round > MAXRoundNumber)
            {
                OnEndGame();
                Stage = GameStage.MainMenu;
                return;
            }

            foreach (var character in AllCharactersInSession)
            {
                character.gameObject.SetActive(true);
            }
            
            SetRoundRates();
            ItemDispenser.DispenseItems();

            foreach (var character in AllCharactersInSession)
            {
                if (character != Player.PlayerCharacter)
                {
                    //ItemOutfitter.EquipItems(character);
                    PotionDrinker.DrinkAllPotions(character);
                }
                ItemOutfitter.EquipItems(character);
                //PotionDrinker.DrinkAllPotions(character);
            }
            
            foreach (var character in AllCharactersInSession)
            {
                
                character.PrepareAfterNewRound();
            }
            
            OnStartBattle();
            
        }

        private static void SetEndOfRound(CharacterState winner)
        {
            RoundEnded = true;
            Stage = GameStage.BetweenBattles;
            Round++;
        }

        public static void SetRoundRates()
        {
            var statistics = Game.AllCharactersInSession.Select(character => character.RoundStatistics).ToList();

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

        public static CharacterType GetRandomCharacter()
        {
            return (CharacterType) ToEnum(FreeTypes[Random.Range(0, FreeTypes.Count)], typeof(CharacterType));
        }

        public static Specialization GetRandomSpecialization()
        {
            return (Specialization) ToEnum(FreeSpecs[Random.Range(0, FreeSpecs.Count)],
                typeof(Specialization));
        }

        public static CharacterState GetCharacterByInventory(IInventory inventory)
        {
            return AllCharactersInSession.Find(character =>
                character.InventoryHelper.inventoryOrganization.Inventory == inventory);
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