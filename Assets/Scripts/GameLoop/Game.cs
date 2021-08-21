﻿using System;
using System.Collections.Generic;
using System.Linq;
using Fight;
using GolemEntity;
using Random = UnityEngine.Random;

namespace GameLoop
{
    public static class Game
    {
        public enum GameStage
        {
            MainMenu,
            Battle
        }
        
        public static GameStage Stage { get; set; }
        public static List<GameCharacterState> AllGolems { get; }
        public static List<string> FreeTypes { get; }
        public static List<string> FreeSpecs { get; }

        public static event Action StartBattle;
        public static event Action OpenMainMenu;
        
        static Game()
        {
            AllGolems = new List<GameCharacterState>();
            FreeTypes = Enum.GetNames(typeof(GolemType)).ToList();
            FreeSpecs = Enum.GetNames(typeof(Specialization)).ToList();

            EventContainer.PlayerCharacterCreated += CreateBotCharacters;
        }

        public static void OnStartBattle()
        {
            StartBattle?.Invoke();
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
                Spawner.Instance.SpawnGolem(GetRandomCharacter(), GetRandomSpecialization(), false);
            }
            
            HeroViewBoxController.Instance.DeactivateRedundantBoxes();
        }

        public static void PrepareNewRound()
        {
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
    }
}