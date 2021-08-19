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
            if (Input.GetKeyDown(KeyCode.I))
            {
                Game.Stage = Game.GameStage.Battle;
            }

        }

        private static Enum ToEnum(string value, Type enumType)
        {
            return (Enum)Enum.Parse(enumType, value, true);
        }
    }
}