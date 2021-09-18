using System;
using System.Linq;
using Fight;
using GameLoop;
using UnityEngine;

namespace Controller
{
     public class CameraMovement : MonoBehaviour
     {
         [SerializeField] private MovementPath movementPathMainMenu;
         [SerializeField] private MovementPath movementPathBattle;
         [SerializeField] private Transform mainMenuTrackingTarget;
         [SerializeField] private Transform battleTrackingTarget;
         [SerializeField] private Transform trackingTarget;
         [SerializeField] private CameraMoveAroundSettings cameraMoveAroundSettings = new CameraMoveAroundSettings();
         [SerializeField] private CameraRtsSettings cameraRtsSettings = new CameraRtsSettings();
         
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
         }
    
         private void SetCameraMode()
         {
             switch (_playMode)
             {
                 case PlayMode.Standard:
                     SetStandardMovement();
                     break;
                 case PlayMode.Rts:
                     SetRtsMovement();
                     break;
                 case PlayMode.Cinematic:
                     if (Game.Stage == Game.GameStage.MainMenu)
                     {
                         transform.LookAt(mainMenuTrackingTarget);
                     }
                     else if (Game.Stage == Game.GameStage.Battle || Game.Stage == Game.GameStage.BetweenBattles)
                     {
                         TurnSmoothlyToTarget(trackingTarget, 1);
                     }
                     break;
                 default:
                     throw new ArgumentOutOfRangeException();
             }
         }
         
         private void OnEnable()
         {
             PlayerController.StandardCamera += SetStandardMovementValues;
             PlayerController.RtsCamera += SetRtsMovementValues;
             PlayerController.CinematicCamera += SetCinematicMovement;
             Game.OpenMainMenu += SetMainMenuMovement;
         }
    
         private void OnDisable()
         {
             PlayerController.StandardCamera -= SetStandardMovementValues;
             PlayerController.RtsCamera -= SetRtsMovementValues;
             PlayerController.CinematicCamera -= SetCinematicMovement;
             Game.OpenMainMenu -= SetMainMenuMovement;
         }
    
         private void SetStandardMovementValues()
         {
             _playMode = PlayMode.Standard;
             _cameraPathFollower.MoveType = PathFollower.MovementType.None;
             
             cameraMoveAroundSettings.limit = Mathf.Abs(cameraMoveAroundSettings.limit);
             if(cameraMoveAroundSettings.limit > 90) cameraMoveAroundSettings.limit = 90;
             cameraMoveAroundSettings.offset = new Vector3(cameraMoveAroundSettings.offset.x, cameraMoveAroundSettings.offset.y, -Mathf.Abs(cameraMoveAroundSettings.zoomMax)/2);
             trackingTarget = Player.PlayerCharacter.transform;
    
             transform.position = Vector3.MoveTowards(transform.position, trackingTarget.position + cameraMoveAroundSettings.offset,
                 Time.deltaTime * 100);
         }
         
         private void SetStandardMovement()
         {
             MoveCameraAroundHero(false);
         }

         private void MoveCameraAroundHero(bool rts, float multiplier = 1)
         {
             if (Input.GetAxis("Mouse ScrollWheel") > 0)
             {
                 cameraMoveAroundSettings.offset.z += cameraMoveAroundSettings.zoom;
             }
             else if (Input.GetAxis("Mouse ScrollWheel") < 0)
             {
                 cameraMoveAroundSettings.offset.z -= cameraMoveAroundSettings.zoom;
             }
             
             cameraMoveAroundSettings.offset.z = Mathf.Clamp(cameraMoveAroundSettings.offset.z * multiplier, -Mathf.Abs(cameraMoveAroundSettings.zoomMax), -Mathf.Abs(cameraMoveAroundSettings.zoomMin));

             if (!rts)
             {
                 _x = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * cameraMoveAroundSettings.sensitivity;
                 _y += Input.GetAxis("Mouse Y") * cameraMoveAroundSettings.sensitivity;
                 _y = Mathf.Clamp (_y, -cameraMoveAroundSettings.limit, cameraMoveAroundSettings.limit);
                 transform.localEulerAngles = new Vector3(-_y, _x, 0);
             }
             
             transform.position = transform.localRotation * cameraMoveAroundSettings.offset + Player.PlayerCharacter.transform.position;
         }
         
         private void SetRtsMovementValues()
         {
             _playMode = PlayMode.Rts;
         }

         private void SetRtsMovement()
         {
             //MoveCameraAroundHero(true,2);
             
             Vector3 pos = transform.position;
            
             if (Input.mousePosition.y >= Screen.height - cameraRtsSettings.borderThickness)
             {
                 pos.z += cameraRtsSettings.moveSpeed * Time.deltaTime;
             }

             if (Input.mousePosition.y <= cameraRtsSettings.borderThickness)
             {
                 pos.z -= cameraRtsSettings.moveSpeed * Time.deltaTime;
             }
            
             if (Input.mousePosition.x >= Screen.width - cameraRtsSettings.borderThickness)
             {
                 pos.x += cameraRtsSettings.moveSpeed * Time.deltaTime;
             }
            
             if (Input.mousePosition.x <= cameraRtsSettings.borderThickness)
             {
                 pos.x -= cameraRtsSettings.moveSpeed * Time.deltaTime;
             }

             float scroll = Input.GetAxis("Mouse ScrollWheel");
             pos.y -= scroll * cameraRtsSettings.scrollSpeed * Time.deltaTime;
            
             pos.x = Mathf.Clamp(pos.x, cameraRtsSettings.limitX.x, cameraRtsSettings.limitX.y);
             pos.y = Mathf.Clamp(pos.y, cameraRtsSettings.limitY.x, cameraRtsSettings.limitY.y);
             pos.z = Mathf.Clamp(pos.z, cameraRtsSettings.limitZ.x, cameraRtsSettings.limitZ.y);
            
             transform.position = pos;
             
             transform.LookAt(Player.PlayerCharacter.transform);
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
         }

         public void SetTarget(GameCharacterState state)
         {
             if (_cameraPathFollower.MoveType != PathFollower.MovementType.None)
             {
                 if (!Equals(trackingTarget, state.transform))
                 {
                     trackingTarget = state.transform;
                 }
             }
         }
         
         private GameCharacterState[] GetTargets()
         {
             return Game.AllGolems.Where(p => p.IsDead == false).ToArray();
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
