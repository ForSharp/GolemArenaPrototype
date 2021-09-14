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
         [SerializeField] private CameraSettings cameraSettings = new CameraSettings();
    
         private PathFollower _cameraPathFollower;
         private bool _defaultTargetChanging = true;
         private float X, Y;
    
         public static CameraMovement Instance { get; private set; }
         
         private void Awake()
         {
             Instance = this;
         }
    
         private void Start()
         {
             _cameraPathFollower = GetComponent<PathFollower>();
             //StartCoroutine(FindTargets());
             SetMainMenuMovement();
         }
    
         private void Update()
         {
             SetCameraMode();
         }
    
         private void SetCameraMode()
         {
             switch (PlayerController.PlayMode)
             {
                 case PlayMode.Standard:
                     SetRotateAroundMovement();
                     break;
                 case PlayMode.Rts:
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
    
         private void SetRotateAroundValues()
         {
             _cameraPathFollower.MoveType = PathFollower.MovementType.None;
             
             cameraSettings.limit = Mathf.Abs(cameraSettings.limit);
             if(cameraSettings.limit > 90) cameraSettings.limit = 90;
             cameraSettings.offset = new Vector3(cameraSettings.offset.x, cameraSettings.offset.y, -Mathf.Abs(cameraSettings.zoomMax)/2);
             trackingTarget = Player.PlayerCharacter.transform;
    
             transform.position = Vector3.MoveTowards(transform.position, trackingTarget.position + cameraSettings.offset,
                 Time.deltaTime * 100);
         }
         
         private void SetRotateAroundMovement()
         {
             if (transform.position.y < 0)
             {
                 transform.position = new Vector3(transform.position.x, 0, transform.position.z);
             }
             
             if(Input.GetAxis("Mouse ScrollWheel") > 0) cameraSettings.offset.z += cameraSettings.zoom;
             else if(Input.GetAxis("Mouse ScrollWheel") < 0) cameraSettings.offset.z -= cameraSettings.zoom;
             cameraSettings.offset.z = Mathf.Clamp(cameraSettings.offset.z, -Mathf.Abs(cameraSettings.zoomMax), -Mathf.Abs(cameraSettings.zoomMin));
    
             X = transform.localEulerAngles.y + Input.GetAxis("Mouse X") * cameraSettings.sensitivity;
             Y += Input.GetAxis("Mouse Y") * cameraSettings.sensitivity;
             Y = Mathf.Clamp (Y, -cameraSettings.limit, cameraSettings.limit);
             transform.localEulerAngles = new Vector3(-Y, X, 0);
             //transform.position = transform.localRotation * cameraSettings.offset + trackingTarget.position;
             transform.position = transform.localRotation * cameraSettings.offset + Player.PlayerCharacter.transform.position;
         }
    
         public void SetTarget(GameCharacterState state)
         {
             if (_cameraPathFollower.MoveType != PathFollower.MovementType.None)
             {
                 _defaultTargetChanging = false;
                 if (!Equals(trackingTarget, state.transform))
                 {
                     trackingTarget = state.transform;
                 }
             }
         }
    
         public void SetDefaultTargetChanging()
         {
             _defaultTargetChanging = true;
         }
         
         private void ChangeTargetIfNeed(RoundStatistics killer)
         {
             // if (trackingTarget.TryGetComponent(out GameCharacterState state))
             // {
             //     if (state.IsDead)
             //         StartCoroutine(FindTargets());
             // }
         }
         
         private GameCharacterState[] GetTargets()
         {
             return Game.AllGolems.Where(p => p.IsDead == false).ToArray();
         }
    
         // private IEnumerator FindTargets()
         // {
         //     var target = GetTargets();
         //
         //     if (target.Length == 0 || PlayerController.PlayMode != PlayMode.Cinematic)
         //     {
         //         yield return new WaitForSeconds(1);
         //         trackingTarget = null;
         //         StartCoroutine(FindTargets());
         //     }
         //
         //     if (target.Length > 0)
         //     {
         //         if (_defaultTargetChanging)
         //         {
         //             var randomTarget = target[Random.Range(0, target.Length)];
         //             trackingTarget = randomTarget.transform;
         //             
         //             yield return new WaitForSeconds(5);
         //             StartCoroutine(FindTargets());
         //         }
         //         else if (!_defaultTargetChanging)
         //         {
         //             yield return new WaitForSeconds(1);
         //             StartCoroutine(FindTargets());
         //         }
         //     }
         // }
         
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
             if (PlayerController.PlayMode == PlayMode.Cinematic)
             {
                 _cameraPathFollower.MovePath = movementPathBattle;
                 _cameraPathFollower.MoveType = PathFollower.MovementType.Moving;
                 _cameraPathFollower.Speed = 1f;
                 _cameraPathFollower.MaxDistance = 0.005f;
                 trackingTarget = battleTrackingTarget;
                 _defaultTargetChanging = true;
             }
             else if (PlayerController.PlayMode == PlayMode.Standard)
             {
                 SetRotateAroundValues();
             }
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
