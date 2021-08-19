using System;
using System.Collections;
using System.Linq;
using Fight;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameLoop
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private MovementPath movementPathMainMenu;
        [SerializeField] private MovementPath movementPathBattle;
        [SerializeField] private Transform mainMenuTrackingTarget;
        [SerializeField] private Transform battleTrackingTarget;

        private PathFollower _cameraPathFollower;

        private void Start()
        {
            _cameraPathFollower = GetComponent<PathFollower>();
            StartCoroutine(FindTargets());
        }

        private void Update()
        {
            ChangeMovementPath();
        }

        private void ChangeMovementPath()
        {
            switch (Game.Stage)
            {
                case Game.GameStage.MainMenu:
                    SetMainMenuMovement();
                    break;
                case Game.GameStage.Battle:
                    SetBattleMovement();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private GameCharacterState[] GetEnemies()
        {
            return FindObjectsOfType<GameCharacterState>().Where(p => p.IsDead == false).ToArray();
        }

        private IEnumerator FindTargets()
        {
            var target = GetEnemies();

            if (target.Length == 0)
            {
                yield return new WaitForSeconds(1);
                battleTrackingTarget = null;
                StartCoroutine(FindTargets());
            }

            if (target.Length > 0)
            {
                battleTrackingTarget = target[Random.Range(0, target.Length)].transform;
                yield return new WaitForSeconds(15);
                StartCoroutine(FindTargets());
            }
        }
        
        
        private void SetMainMenuMovement()
        {
            _cameraPathFollower.MovePath = movementPathMainMenu;
            _cameraPathFollower.MoveType = PathFollower.MovementType.Lerp;
            _cameraPathFollower.TrackingTarget = mainMenuTrackingTarget;
        }

        private void SetBattleMovement()
        {
            _cameraPathFollower.MovePath = movementPathBattle;
            //_cameraPathFollower.MoveType = PathFollower.MovementType.Moving;
            _cameraPathFollower.TrackingTarget = battleTrackingTarget;
        }
        
    }
}
