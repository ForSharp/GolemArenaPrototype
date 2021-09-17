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
         [SerializeField] private CameraSettings cameraSettings = new CameraSettings();
    
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
             
             cameraSettings.limit = Mathf.Abs(cameraSettings.limit);
             if(cameraSettings.limit > 90) cameraSettings.limit = 90;
             cameraSettings.offset = new Vector3(cameraSettings.offset.x, cameraSettings.offset.y, -Mathf.Abs(cameraSettings.zoomMax)/2);
             trackingTarget = Player.PlayerCharacter.transform;
    
             transform.position = Vector3.MoveTowards(transform.position, trackingTarget.position + cameraSettings.offset,
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
                 cameraSettings.offset.z += cameraSettings.zoom;
             }
             else if (Input.GetAxis("Mouse ScrollWheel") < 0)
             {
                 cameraSettings.offset.z -= cameraSettings.zoom;
             }
             
             cameraSettings.offset.z = Mathf.Clamp(cameraSettings.offset.z * multiplier, -Mathf.Abs(cameraSettings.zoomMax), -Mathf.Abs(cameraSettings.zoomMin));

             if (!rts)
             {
                 _x = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * cameraSettings.sensitivity;
                 _y += Input.GetAxis("Mouse Y") * cameraSettings.sensitivity;
                 _y = Mathf.Clamp (_y, -cameraSettings.limit, cameraSettings.limit);
                 transform.localEulerAngles = new Vector3(-_y, _x, 0);
             }
             
             transform.position = transform.localRotation * cameraSettings.offset + Player.PlayerCharacter.transform.position;
         }
         
         private void SetRtsMovementValues()
         {
             _playMode = PlayMode.Rts;
         }

         private void SetRtsMovement()
         {
             MoveCameraAroundHero(true,2);
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
