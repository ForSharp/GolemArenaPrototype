using System;
using Fight;
using GameLoop;
using GolemEntity;
using UI;
using UnityEngine;

namespace Controller
{
    public class UserInputTest : MonoBehaviour
    {
        [SerializeField] private GolemStatsPanel statsPanel;
        
        private GolemType _golemType;
        private Specialization _specialization;

        private void Update()
        {
            
            if (Input.GetMouseButtonDown(0))
            {
                TryShowHeroStates();
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

        private void TryShowHeroStates()
        {
            var ray = Camera.main.ScreenPointToRay(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                var coll = hit.collider;
                if (coll.TryGetComponent(out GameCharacterState state))
                {
                    statsPanel.gameObject.SetActive(true);
                    statsPanel.HandleClick(state);
                }
                else if (!statsPanel.inPanel)
                {
                    statsPanel.gameObject.SetActive(false);
                    CameraMovement.Instance.SetDefaultTargetChanging();
                }
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
    }
}