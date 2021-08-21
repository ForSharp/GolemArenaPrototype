using System;
using GameLoop;
using GolemEntity;
using UnityEngine;

namespace Controller
{
    public class UserInputTest : MonoBehaviour
    {
        [SerializeField] private GameObject panelGolemType;
        [SerializeField] private GameObject panelGolemSpec;

        private GolemType _golemType;
        private Specialization _specialization;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.I) && Game.Stage != Game.GameStage.Battle)
            {
                Game.Stage = Game.GameStage.Battle;
                Game.OnStartBattle();
            }

            if (CanShowMainMenu())
            {
                Game.Stage = Game.GameStage.MainMenu;
                Game.OnOpenMainMenu();
            }

            bool CanShowMainMenu()
            {
                return Input.GetKeyDown(KeyCode.Escape) && Game.Stage != Game.GameStage.MainMenu;
            }
        }

        private static Enum ToEnum(string value, Type enumType)
        {
            return (Enum)Enum.Parse(enumType, value, true);
        }
    }
}