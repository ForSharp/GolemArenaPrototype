using UnityEngine;

namespace __Scripts.CharacterEntity
{
	public class AnimationControlAnimalCreep : MonoBehaviour 
	{
		//!!!!! код из ассета
		private string _currentAnimation="";
		
		public void SetAnimation(string animationName){
		
			if (_currentAnimation != "") {
				GetComponent<Animator> ().SetBool (_currentAnimation, false);
			}
			GetComponent<Animator> ().SetBool (animationName, true);
			_currentAnimation = animationName;
		}

		public void SetAnimationIdle(){

			if (_currentAnimation != "") {
				GetComponent<Animator> ().SetBool (_currentAnimation, false);
			}


		}
		public void SetDeathAnimation(int numOfClips){

			int clipIndex = Random.Range(0, numOfClips);
			string animationName = "Death";
			Debug.Log (clipIndex);

			GetComponent<Animator> ().SetInteger (animationName, clipIndex);
		}
		
	}
}
