using System;
using System.Collections;
using System.Linq;
using Fight;
using GameLoop;
using UI;
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
        private bool _defaultTargetChanging = true;

        public static CameraMovement Instance { get; private set; }
        
        private void Awake()
        {
            Instance = this;
        }

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

            if (NeedToDecreaseSpeed())
            {
                DecreaseSpeed();
            }

            bool NeedToDecreaseSpeed()
            {
                return _cameraPathFollower.Speed > 1 && Vector3.Distance(transform.position, Vector3.zero) < 15;
            }
        }

        private void OnEnable()
        {
            Game.StartBattle += SetBattleMovement;
            Game.OpenMainMenu += SetMainMenuMovement;
            EventContainer.GolemDied += ChangeTargetIfNeed;
        }

        private void OnDisable()
        {
            Game.StartBattle -= SetBattleMovement;
            Game.OpenMainMenu -= SetMainMenuMovement;
            EventContainer.GolemDied -= ChangeTargetIfNeed;
        }

        public void SetTarget(GameCharacterState state)
        {
            _defaultTargetChanging = false;
            if (!Equals(trackingTarget, state.transform))
            {
                trackingTarget = state.transform;
                Debug.Log($"{state.Type} {Time.deltaTime}");
            }
        }

        public void SetDefaultTargetChanging()
        {
            _defaultTargetChanging = true;
        }
        
        private void ChangeTargetIfNeed()
        {
            if (battleTrackingTarget.GetComponent<GameCharacterState>().IsDead)
            {
                StartCoroutine(FindTargets());
            }
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
                
                Debug.Log($"Zero characters {Time.deltaTime}");
            }

            if (target.Length > 0)
            {
                if (_defaultTargetChanging)
                {
                    var randomTarget = target[Random.Range(0, target.Length)];
                    battleTrackingTarget = randomTarget.transform;
                    
                    Debug.Log($"{randomTarget.Type} {Time.deltaTime}");
                    
                    yield return new WaitForSeconds(5);
                    StartCoroutine(FindTargets());
                }
                else if (!_defaultTargetChanging)
                {
                    yield return new WaitForSeconds(1);
                    StartCoroutine(FindTargets());
                }
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
        }

        private void DecreaseSpeed()
        {
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
