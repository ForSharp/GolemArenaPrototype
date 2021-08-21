using System;
using System.Collections.Generic;
using UnityEngine;

namespace Controller
{
    public class PathFollower : MonoBehaviour
    {
        public enum MovementType
        {
            Moving,
            Lerp
        }

        [SerializeField] private MovementType movementType;
        [SerializeField] private float speed = 1;
        [SerializeField] private float maxDistance = 0.1f;
        [SerializeField] private MovementPath movementPath;
        private IEnumerator<Transform> _pointInPath;

        public MovementPath MovePath
        {
            get => movementPath;
            set
            {
                movementPath = value;
                _pointInPath = movementPath.GetNextPathPoint();
                _pointInPath.MoveNext();
            }
        }
        public MovementType MoveType {
            get => movementType;
            set => movementType = value;
        }
        public float Speed
        {
            get => speed;
            set
            {
                if (value > 0)
                {
                    speed = value;
                }
                else if (value < 0)
                {
                    speed = -value;
                }
                else
                {
                    speed = 1;
                }
            }
        }
        public float MaxDistance
        {
            get => maxDistance;
            set
            {
                if (value > 0)
                {
                    maxDistance = value;
                }
                else if (value < 0)
                {
                    maxDistance = -value;
                }
                else
                {
                    maxDistance = 1;
                }
            }
        }
        private void Start()
        {
            if (movementPath == null)
            {
                Debug.Log("Need to set the path");
                return;
            }

            _pointInPath = movementPath.GetNextPathPoint();
            _pointInPath.MoveNext();

            if (_pointInPath.Current == null)
            {
                Debug.Log("Need points");
                return;
            }

            transform.position = _pointInPath.Current.position;
        }

        private void Update()
        {
            if (!HasPoint())
                return;

            if (CloseEnoughToMoveNext())
            {
                _pointInPath.MoveNext();
            }

            MoveObject();

            bool HasPoint()
            {
                return _pointInPath != null && _pointInPath.Current != null;
            }
            
            bool CloseEnoughToMoveNext()
            {
                return GetDistanceSquare() < maxDistance * maxDistance;
            }
        }

        private void MoveObject()
        {
            switch (movementType)
            {
                case MovementType.Moving:
                    transform.position = Vector3.MoveTowards(transform.position, _pointInPath.Current.position,
                        Time.deltaTime * speed);
                    break;
                case MovementType.Lerp:
                    transform.position = Vector3.Lerp(transform.position, _pointInPath.Current.position,
                        Time.deltaTime * speed);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private float GetDistanceSquare()
        {
            return (transform.position - _pointInPath.Current.position).sqrMagnitude;
        }
    }
}