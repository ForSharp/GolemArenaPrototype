using Animation.RPG_Character_Animation_Pack.Code;
using UnityEngine;

namespace Animation.RPG_Character_Animation_Pack.SuperCharacterController.Code
{
	/// <summary>
	/// Rotates a this transform to align it towards the target transform's position.
	/// </summary>
	public class Gravity : MonoBehaviour{
		[SerializeField] Transform planet;

		void Update(){
			Vector3 dir = (transform.position - planet.position).normalized;
			GetComponent<RPGCharacterMovementController>().RotateGravity(dir);
			transform.rotation = Quaternion.FromToRotation(transform.up, dir) * transform.rotation;
		}
	}
}