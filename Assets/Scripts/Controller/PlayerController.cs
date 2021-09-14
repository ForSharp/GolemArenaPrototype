using System;
using System.Collections;
using System.Collections.Generic;
using GameLoop;
using UnityEngine;

namespace Controller
{
    public enum PlayMode
    {
        Standard,
        Rts,
        Cinematic
    }

    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private GameObject standardPanel;
        [SerializeField] private GameObject rtsPanel;
        
        public static PlayMode PlayMode = PlayMode.Cinematic;
        public static bool AIControl { get; private set; }

        private void Awake()
        {
            AIControl = true;
        }

        private void OnEnable()
        {
            Game.StartBattle += SetStandard;
        }

        private void OnDisable()
        {
            Game.StartBattle -= SetStandard;
        }

        private void SetStandard()
        {
            PlayMode = PlayMode.Standard;
        }
        
        private void Update()
        {
            switch (PlayMode)
            {
                case PlayMode.Standard:
                    break;
                case PlayMode.Rts:
                    break;
                case PlayMode.Cinematic:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        public void SwitchMode(string mode)
        {
            switch (mode)
            {
                case "Standard":
                    PlayMode = PlayMode.Standard;
                    SetStandardPanel();
                    break;
                case "Rts":
                    PlayMode = PlayMode.Rts;
                    SetRtsPanel();
                    break;
                case "Cinematic":
                    PlayMode = PlayMode.Cinematic;
                    SetCinematicPanel();
                    break;
            }
        }

        private void HandleJoystickInput()
        {
            
        }
        
        private void SetStandardPanel()
        {
            standardPanel.SetActive(true);
            rtsPanel.SetActive(false);
        }

        private void SetRtsPanel()
        {
            standardPanel.SetActive(false);
            rtsPanel.SetActive(true);
        }

        private void SetCinematicPanel()
        {
            standardPanel.SetActive(false);
            rtsPanel.SetActive(true);
        }
    }
}