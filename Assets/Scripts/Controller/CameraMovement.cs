using System;
using System.Collections;
using System.Linq;
using Fight;
using GameLoop;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Controller
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private MovementPath movementPathMainMenu;
        [SerializeField] private MovementPath movementPathBattle;
        [SerializeField] private Transform mainMenuTrackingTarget;
        [SerializeField] private Transform battleTrackingTarget;
        [SerializeField] private Transform trackingTarget;
        
        private PathFollower _cameraPathFollower;

        private void Start()
        {
            _cameraPathFollower = GetComponent<PathFollower>();
            StartCoroutine(FindTargets());
            SetMainMenuMovement();
        }

        private void Update()
        {
            switch (Game.Stage)
            {
                case Game.GameStage.MainMenu:
                    if (trackingTarget != null)
                    {
                        transform.LookAt(trackingTarget);
                    }
                    break;
                case Game.GameStage.Battle:
                    TurnSmoothlyToTarget(trackingTarget, 1);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void OnEnable()
        {
            Game.StartBattle += SetBattleMovement;
            Game.OpenMainMenu += SetMainMenuMovement;
        }

        private void OnDisable()
        {
            Game.StartBattle -= SetBattleMovement;
            Game.OpenMainMenu -= SetMainMenuMovement;
        }

        private GameCharacterState[] GetEnemies()
        {
            return Game.AllGolems.Where(p => p.IsDead == false).ToArray();
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
            _cameraPathFollower.Speed = 0.3f;
            _cameraPathFollower.MaxDistance = 0.1f;
            trackingTarget = mainMenuTrackingTarget;
        }

        private void SetBattleMovement()
        {
            _cameraPathFollower.MovePath = movementPathBattle;
            _cameraPathFollower.MoveType = PathFollower.MovementType.Moving;
            _cameraPathFollower.Speed = 100;
            _cameraPathFollower.MaxDistance = 0.005f;
            trackingTarget = battleTrackingTarget;

            StartCoroutine(ChangeSpeed());
        }

        private IEnumerator ChangeSpeed()
        {
            yield return new WaitForSeconds(1);
            _cameraPathFollower.Speed = 1f;
        }
        
        private void TurnSmoothlyToTarget(Transform target, float speed)
        {
            if (target)
            {
                Vector3 direction = target.transform.position - transform.position;
                Quaternion rotation = Quaternion.LookRotation(direction);
                transform.rotation = Quaternion.Lerp(transform.rotation, rotation,
                    speed * Time.deltaTime);
            }
        }
    }
}
