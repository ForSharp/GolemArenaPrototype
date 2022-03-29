using UnityEngine;

namespace Animation.RPG_Character_Animation_Pack.SuperCharacterController.Code.SuperCharacterController.Core
{
    /// <summary>
    /// Extend this class to add in any further data you want to be able to access
    /// pertaining to an object the controller has collided with
    /// </summary>
    public class SuperCollisionType : MonoBehaviour {

        public float StandAngle = 80.0f;
        public float SlopeLimit = 80.0f;
    }
}
