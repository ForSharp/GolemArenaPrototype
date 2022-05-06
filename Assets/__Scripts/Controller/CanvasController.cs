using UnityEngine;
using UnityEngine.EventSystems;

namespace __Scripts.Controller
{
    public class CanvasController : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public bool CanMoveCamera { get; private set; } = true;

        public void OnPointerEnter(PointerEventData eventData)
        {
            CanMoveCamera = false;
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            CanMoveCamera = true;
        }
    }
}
