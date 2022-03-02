using System;
using System.Collections.Generic;
using UnityEngine;

namespace Controller
{
    public class MovementPath : MonoBehaviour
    {
        private enum PathTypes
        {
            Linear,
            Loop
        }

        [SerializeField] private PathTypes pathType;
        [SerializeField] private Transform[] pathElements;
        private int _movementDirection = 1;
        private int _targetPoint;

        public void OnDrawGizmos()
        {
            if (!HasAtLeastTwoPoints())
                return;
            
            DrawLinesBetweenPoints();

            if (pathType == PathTypes.Loop)
            {
                DrawLinesFromStartingPointToLast();
            }
            
            bool HasAtLeastTwoPoints()
            {
                return pathElements != null && pathElements.Length >= 2;
            }
        }

        public IEnumerator<Transform> GetNextPathPoint()
        {
            if (!HasPoints())
                yield break;

            while (true)
            {
                yield return pathElements[_targetPoint];
                
                if(pathElements.Length == 1)
                    continue;

                _targetPoint += _movementDirection;
                
                switch (pathType)
                {
                    case PathTypes.Linear:
                        if (_targetPoint <= 0)
                        {
                            _movementDirection = 1;
                        }
                        else if (_targetPoint >= pathElements.Length - 1)
                        {
                            _movementDirection = -1;
                        }
                        break;
                    case PathTypes.Loop:
                        if (_targetPoint >= pathElements.Length)
                        {
                            _targetPoint = 0;
                        }

                        if (_targetPoint < 0)
                        {
                            _targetPoint = pathElements.Length - 1;
                        }
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
            
            bool HasPoints()
            {
                return pathElements != null && pathElements.Length >= 1;
            }
        }
        
        private void DrawLinesFromStartingPointToLast()
        {
            Gizmos.DrawLine(pathElements[0].position, pathElements[pathElements.Length - 1].position);
        }
        private void DrawLinesBetweenPoints()
        {
            for (int i = 1; i < pathElements.Length; i++)
            {
                Gizmos.DrawLine(pathElements[i - 1].position, pathElements[i].position);
            }
        }
    }
}
