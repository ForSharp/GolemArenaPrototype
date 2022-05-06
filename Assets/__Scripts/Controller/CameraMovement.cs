using System;
using System.Linq;
using __Scripts.CharacterEntity.State;
using __Scripts.GameLoop;
using UnityEngine;

namespace __Scripts.Controller
{
    public class CameraMovement : MonoBehaviour
    {
        [SerializeField] private MovementPath movementPathMainMenu;
        [SerializeField] private MovementPath movementPathBattle;
        [SerializeField] private Transform mainMenuTrackingTarget;
        [SerializeField] private Transform battleTrackingTarget;
        [SerializeField] private Transform trackingTarget;
        [SerializeField] private CameraMoveAroundSettings cameraMoveAroundSettings = new CameraMoveAroundSettings();
        [SerializeField] private DynamicJoystick joystick;

        private PathFollower _cameraPathFollower;
        private float _x, _y;
        private PlayMode _playMode = PlayMode.Cinematic;

        public static CameraMovement Instance { get; private set; }

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            _cameraPathFollower = GetComponent<PathFollower>();
            SetMainMenuMovement();
        }

        private void Update()
        {
            SetCameraMode();
            CorrectPosition();
        }

        private void SetCameraMode()
        {
            switch (_playMode)
            {
                case PlayMode.Standard:
                    SetStandardMovement();
                    break;
                case PlayMode.Cinematic:
                    if (Game.Stage == Game.GameStage.MainMenu)
                    {
                        joystick.gameObject.SetActive(false);
                        transform.LookAt(mainMenuTrackingTarget);
                    }
                    else if (Game.Stage == Game.GameStage.Battle || Game.Stage == Game.GameStage.BetweenBattles)
                    {
                        joystick.gameObject.SetActive(false);
                        TurnSmoothlyToTarget(trackingTarget, 1);
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void CorrectPosition()
        {
            if (transform.position.y < 1)
            {
                transform.position = new Vector3(transform.position.x, 1, transform.position.z);
            }
        }

        private void OnEnable()
        {
            PlayerController.StandardCamera += SetStandardMovementValues;
            PlayerController.CinematicCamera += SetCinematicMovement;
            Game.OpenMainMenu += SetMainMenuMovement;
        }

        private void OnDisable()
        {
            PlayerController.StandardCamera -= SetStandardMovementValues;
            PlayerController.CinematicCamera -= SetCinematicMovement;
            Game.OpenMainMenu -= SetMainMenuMovement;
        }

        private void SetStandardMovementValues()
        {
            joystick.gameObject.SetActive(true);
            
            _playMode = PlayMode.Standard;
            _cameraPathFollower.MoveType = PathFollower.MovementType.None;

            cameraMoveAroundSettings.limit = Mathf.Abs(cameraMoveAroundSettings.limit);
            if (cameraMoveAroundSettings.limit > 90) cameraMoveAroundSettings.limit = 90;
            cameraMoveAroundSettings.offset = new Vector3(cameraMoveAroundSettings.offset.x,
                cameraMoveAroundSettings.offset.y, -Mathf.Abs(cameraMoveAroundSettings.zoomMax) / 2);
            trackingTarget = Player.PlayerCharacter.transform;

            transform.position = Vector3.MoveTowards(transform.position,
                trackingTarget.position + cameraMoveAroundSettings.offset,
                Time.deltaTime * 100);
        }

        private void SetStandardMovement()
        {
            MoveCameraAroundHero();
            //MoveCameraByJoystick();
        }

        private void MoveCameraAroundHero(float multiplier = 1)
        {
            if (Input.GetAxis("Mouse ScrollWheel") > 0)
            {
                cameraMoveAroundSettings.offset.z += cameraMoveAroundSettings.zoom;
            }
            else if (Input.GetAxis("Mouse ScrollWheel") < 0)
            {
                cameraMoveAroundSettings.offset.z -= cameraMoveAroundSettings.zoom;
            }

            cameraMoveAroundSettings.offset.z = Mathf.Clamp(cameraMoveAroundSettings.offset.z * multiplier,
                -Mathf.Abs(cameraMoveAroundSettings.zoomMax), -Mathf.Abs(cameraMoveAroundSettings.zoomMin));
            
            if (Input.GetMouseButton(1))
            {
                _x = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * cameraMoveAroundSettings.sensitivity;
                _y += Input.GetAxis("Mouse Y") * cameraMoveAroundSettings.sensitivity;
                _y = Mathf.Clamp(_y, -cameraMoveAroundSettings.limit, cameraMoveAroundSettings.limit);
                transform.localEulerAngles = new Vector3(-_y, _x, 0);
            }
            
            transform.position = transform.localRotation * cameraMoveAroundSettings.offset +
                                 Player.PlayerCharacter.transform.position;

            transform.position = new Vector3(transform.position.x, Player.PlayerCharacter.transform.position.y + 4, transform.position.z);
            //TurnSmoothlyToTarget(Player.PlayerCharacter.transform, 5);
        }

        private void MoveCameraByJoystick()
        {
            TurnSmoothlyToTarget(Player.PlayerCharacter.transform, 5);
            
            var moveVector = Vector3.zero;
            
            moveVector.x = joystick.Horizontal;
            moveVector.z = joystick.Vertical;
            moveVector.y = joystick.Vertical;
            
            if (Vector3.Angle(Vector3.forward, moveVector) > 1 || Vector3.Angle(Vector3.forward, moveVector) == 0)
            {
                var direction = Vector3.RotateTowards(transform.forward, moveVector, 1, 0);
                transform.rotation = Quaternion.LookRotation(direction);
            }
            
            //transform.LookAt(Player.PlayerCharacter.transform);
        }
        
        private void SetCinematicMovement()
        {
            _playMode = PlayMode.Cinematic;
            if (Game.Stage == Game.GameStage.MainMenu)
            {
                SetMainMenuMovement();
            }
            else if (Game.Stage == Game.GameStage.Battle || Game.Stage == Game.GameStage.BetweenBattles)
            {
                SetBattleCinematicMovement();
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

        private void SetBattleCinematicMovement()
        {
            _cameraPathFollower.MovePath = movementPathBattle;
            _cameraPathFollower.MoveType = PathFollower.MovementType.Moving;
            _cameraPathFollower.Speed = 1f;
            _cameraPathFollower.MaxDistance = 0.005f;
            trackingTarget = battleTrackingTarget;

            SetTarget(Player.PlayerCharacter);
        }

        public void SetTarget(CharacterState state)
        {
            if (_cameraPathFollower.MoveType != PathFollower.MovementType.None)
            {
                if (!Equals(trackingTarget, state.transform))
                {
                    trackingTarget = state.transform;
                }
            }
        }

        private CharacterState[] GetTargets()
        {
            return Game.AllCharactersInSession.Where(p => p.IsDead == false).ToArray();
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