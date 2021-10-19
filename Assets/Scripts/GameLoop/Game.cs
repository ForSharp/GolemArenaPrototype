using System;
using System.Collections.Generic;
using System.Linq;
using FightState;
using GolemEntity;
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
        public static List<GameCharacterState> AllGolems { get; }
        public static List<string> FreeTypes { get; }
        public static List<string> FreeSpecs { get; }
        
        public static int Round { get; private set; }

        public static bool RoundEnded { get; private set; }
        public static event Action StartBattle;
        public static event Action OpenMainMenu;
        public static event Action EndGame;
        
        static Game()
        {
            AllGolems = new List<GameCharacterState>();
            FreeTypes = Enum.GetNames(typeof(GolemType)).ToList();
            FreeSpecs = Enum.GetNames(typeof(Specialization)).ToList();

            Stage = GameStage.MainMenu;
            
            EventContainer.PlayerCharacterCreated += CreateBotCharacters;
            EventContainer.WinBattle += CheckEndOfRound;
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
        
        public static void AddToAllGolems(GameCharacterState golem)
        {
            AllGolems.Add(golem);
            FreeTypes.Remove(golem.Type);
            FreeSpecs.Remove(golem.Spec);
        }

        private static void CreateBotCharacters()
        {
            for (int i = 0; i < 4; i++)
            {
                Spawner.Instance.SpawnGolem(GetRandomCharacter(), GetRandomSpecialization());
            }
            
            HeroViewBoxController.Instance.DeactivateRedundantBoxes();
        }

        public static void PrepareNewRound()
        {
            if (Round > 4)
            {
                OnEndGame();
                Stage = GameStage.MainMenu;
                return;
            }

            foreach (var character in AllGolems)
            {
                character.gameObject.SetActive(true);
                character.PrepareAfterNewRound();
            }
            
            OnStartBattle();
            
        }
        
        
        public static void CheckEndOfRound(GameCharacterState winner)
        {
            RoundEnded = true;
            Game.Stage = GameStage.BetweenBattles;
            Round++;
        }
        

        public static GolemType GetRandomCharacter()
        {
            return (GolemType) ToEnum(FreeTypes[Random.Range(0, FreeTypes.Count)], typeof(GolemType));
        }

        public static Specialization GetRandomSpecialization()
        {
            return (Specialization) ToEnum(FreeSpecs[Random.Range(0, FreeSpecs.Count)],
                typeof(Specialization));
        }

        public static Enum ToEnum(string value, Type enumType)
        {
            return (Enum) Enum.Parse(enumType, value, true);
        }

        public static void OnEndGame()
        {
            EndGame?.Invoke();
        }
    }
}