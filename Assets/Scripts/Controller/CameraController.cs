using UnityEngine;

namespace Controller
{
    public class CameraController : MonoBehaviour
    {
        [SerializeField] private int inverse = -1;
        [SerializeField] private float moveSpeed = 20f;
        [SerializeField] private float scrollSpeed = 100f;
        [SerializeField] private Vector2 limitX = new Vector2(-40, 40);
        [SerializeField] private Vector2 limitY = new Vector2(0, 40);
        [SerializeField] private Vector2 limitZ = new Vector2(-60, 50);
        
        private const float BorderThickness = 10f;
        
        private void Update()
        {
            if (Input.GetMouseButton(1))
            {
                MoveCamera();
            }
            
        }

        private void MoveCamera()
        {
            Vector3 pos = transform.position;
            
            if (Input.mousePosition.y >= Screen.height - BorderThickness)
            {
                pos.z += moveSpeed * Time.deltaTime * inverse;
            }

            if (Input.mousePosition.y <= BorderThickness)
            {
                pos.z -= moveSpeed * Time.deltaTime * inverse;
            }
            
            if (Input.mousePosition.x >= Screen.width - BorderThickness)
            {
                pos.x += moveSpeed * Time.deltaTime * inverse;
            }
            
            if (Input.mousePosition.x <= BorderThickness)
            {
                pos.x -= moveSpeed * Time.deltaTime * inverse;
            }

            float scroll = Input.GetAxis("Mouse ScrollWheel");
            pos.y -= scroll * scrollSpeed * Time.deltaTime;
            
            pos.x = Mathf.Clamp(pos.x, limitX.x, limitX.y);
            pos.y = Mathf.Clamp(pos.y, limitY.x, limitY.y);
            pos.z = Mathf.Clamp(pos.z, limitZ.x, limitZ.y);
            
            transform.position = pos;

        }
    }
}
