using __Scripts.Controller;
using UnityEngine;
using UnityEngine.EventSystems;

namespace __Scripts.Inventory.DragAndDrop
{
    public class UIItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        private Canvas _mainCanvasController;
        private RectTransform _rectTransform;
        private CanvasGroup _canvasGroup;
        private int _parentIndex;
        private Transform _parentSlotTransform;

        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            _mainCanvasController = GetComponentInParent<Canvas>();
            _canvasGroup = GetComponent<CanvasGroup>();
            _parentSlotTransform = _rectTransform.parent;
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta / _mainCanvasController.scaleFactor;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            _parentIndex = _parentSlotTransform.GetSiblingIndex();
            _parentSlotTransform.SetAsLastSibling();
            
            _canvasGroup.blocksRaycasts = false;
        }

        public void OnEndDrag(PointerEventData eventData)
        {
            transform.localPosition = Vector3.zero;
            _canvasGroup.blocksRaycasts = true;
            
            _parentSlotTransform.SetSiblingIndex(_parentIndex);
        }
    }
}
