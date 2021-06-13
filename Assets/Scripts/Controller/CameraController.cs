using UnityEngine;

namespace Scripts
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private float mouseSensitivity;

        private Transform _parent;
        
        private void Start()
        {
            _parent = transform.parent;
            Cursor.lockState = CursorLockMode.Locked;
        }

        private void FixedUpdate()
        {
            RotateView();
        }

        private void RotateView()
        {
            float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;
            
            _parent.Rotate(Vector3.up, mouseX);
        }
    }
}