using System;
using GameLoop;
using GolemEntity;
using UnityEngine;

namespace Controller
{
    public class UserInputTest : MonoBehaviour
    {
        private GolemType _golemType;
        private Specialization _specialization;

        private void Update()
        {
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

        public void StartButtonClick()
        {
            if (Game.Stage != Game.GameStage.Battle)
            {
                Game.Stage = Game.GameStage.Battle;
                Game.OnStartBattle();
            }
        }

        private static Enum ToEnum(string value, Type enumType)
        {
            return (Enum)Enum.Parse(enumType, value, true);
        }
    }
}