using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameLoop
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
        [SerializeField] private bool allowLookAt;
        [SerializeField] private Transform trackingTarget;
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
        public Transform TrackingTarget {
            get => trackingTarget;
            set => trackingTarget = value;
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

            if (allowLookAt)
            {
                if (trackingTarget != null)
                {
                    transform.LookAt(trackingTarget);
                }
            }

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