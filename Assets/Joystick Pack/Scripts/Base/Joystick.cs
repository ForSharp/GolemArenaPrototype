using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Joystick_Pack.Scripts.Base
{
    public class Joystick : MonoBehaviour, IPointerDownHandler, IDragHandler, IPointerUpHandler
    {
        public float Horizontal => (snapX) ? SnapFloat(_input.x, AxisOptions.Horizontal) : _input.x;
        public float Vertical => (snapY) ? SnapFloat(_input.y, AxisOptions.Vertical) : _input.y;
        public Vector2 Direction => new Vector2(Horizontal, Vertical);

        private float HandleRange
        {
            get => handleRange;
            set => handleRange = Mathf.Abs(value);
        }

        private float DeadZone
        {
            get => deadZone;
            set => deadZone = Mathf.Abs(value);
        }

        public AxisOptions AxisOptions
        {
            get => AxisOptions;
            set => axisOptions = value;
        }

        public bool SnapX
        {
            get => snapX;
            set => snapX = value;
        }

        public bool SnapY
        {
            get => snapY;
            set => snapY = value;
        }

        [SerializeField] private float handleRange = 1;
        [SerializeField] private float deadZone = 0;
        [SerializeField] private AxisOptions axisOptions = AxisOptions.Both;
        [SerializeField] private bool snapX = false;
        [SerializeField] private bool snapY = false;

        [SerializeField] protected RectTransform background = null;
        [SerializeField] private RectTransform handle = null;
        private RectTransform _baseRect = null;

        private Canvas _canvas;
        private Camera _cam;

        private Vector2 _input = Vector2.zero;

        protected virtual void Start()
        {
            HandleRange = handleRange;
            DeadZone = deadZone;
            _baseRect = GetComponent<RectTransform>();
            _canvas = GetComponentInParent<Canvas>();
            if (_canvas == null)
                Debug.LogError("The Joystick is not placed inside a canvas");

            var center = new Vector2(0.5f, 0.5f);
            background.pivot = center;
            handle.anchorMin = center;
            handle.anchorMax = center;
            handle.pivot = center;
            handle.anchoredPosition = Vector2.zero;
        }

        public virtual void OnPointerDown(PointerEventData eventData)
        {
            OnDrag(eventData);
        }

        public void OnDrag(PointerEventData eventData)
        {
            _cam = null;
            if (_canvas.renderMode == RenderMode.ScreenSpaceCamera)
                _cam = _canvas.worldCamera;

            var position = RectTransformUtility.WorldToScreenPoint(_cam, background.position);
            var radius = background.sizeDelta / 2;
            _input = (eventData.position - position) / (radius * _canvas.scaleFactor);
            FormatInput();
            HandleInput(_input.magnitude, _input.normalized, radius, _cam);
            handle.anchoredPosition = _input * radius * handleRange;
        }

        protected virtual void HandleInput(float magnitude, Vector2 normalised, Vector2 radius, Camera cam)
        {
            if (magnitude > deadZone)
            {
                if (magnitude > 1)
                    _input = normalised;
            }
            else
                _input = Vector2.zero;
        }

        private void FormatInput()
        {
            switch (axisOptions)
            {
                case AxisOptions.Horizontal:
                    _input = new Vector2(_input.x, 0f);
                    break;
                case AxisOptions.Vertical:
                    _input = new Vector2(0f, _input.y);
                    break;
                case AxisOptions.Both:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private float SnapFloat(float value, AxisOptions snapAxis)
        {
            if (value == 0)
                return value;

            if (axisOptions == AxisOptions.Both)
            {
                var angle = Vector2.Angle(_input, Vector2.up);
                switch (snapAxis)
                {
                    case AxisOptions.Horizontal when angle < 22.5f || angle > 157.5f:
                        return 0;
                    case AxisOptions.Horizontal:
                        return (value > 0) ? 1 : -1;
                    case AxisOptions.Vertical when angle > 67.5f && angle < 112.5f:
                        return 0;
                    case AxisOptions.Vertical:
                        return (value > 0) ? 1 : -1;
                    default:
                        return value;
                }
            }
            else
            {
                if (value > 0)
                    return 1;
                if (value < 0)
                    return -1;
            }

            return 0;
        }

        public virtual void OnPointerUp(PointerEventData eventData)
        {
            _input = Vector2.zero;
            handle.anchoredPosition = Vector2.zero;
        }

        protected Vector2 ScreenPointToAnchoredPosition(Vector2 screenPosition)
        {
            if (!RectTransformUtility.ScreenPointToLocalPointInRectangle(_baseRect, screenPosition, _cam,
                    out var localPoint)) return Vector2.zero;
            var sizeDelta = _baseRect.sizeDelta;
            var pivotOffset = _baseRect.pivot * sizeDelta;
            return localPoint - (background.anchorMax * sizeDelta) + pivotOffset;
        }
    }

    public enum AxisOptions
    {
        Both,
        Horizontal,
        Vertical
    }
}