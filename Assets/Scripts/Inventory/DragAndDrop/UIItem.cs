using System.Timers;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Inventory.DragAndDrop
{
    public class UIItem : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        private Canvas _mainCanvas;
        private RectTransform _rectTransform;
        private CanvasGroup _canvasGroup;
        private int _parentIndex;
        private Transform _parentSlotTransform;

        private void Start()
        {
            _rectTransform = GetComponent<RectTransform>();
            _mainCanvas = GetComponentInParent<Canvas>();
            _canvasGroup = GetComponent<CanvasGroup>();
            _parentSlotTransform = _rectTransform.parent;
        }

        public void OnDrag(PointerEventData eventData)
        {
            _rectTransform.anchoredPosition += eventData.delta / _mainCanvas.scaleFactor;
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
