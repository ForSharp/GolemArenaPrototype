using System;
using UnityEngine;

namespace Controller
{
    [Serializable]
    public class CameraSettings 
    {
        public Vector3 offset;
        public float sensitivity = 3; 
        public float limit = 80; 
        public float zoom = 0.25f; 
        public float zoomMax = 10; 
        public float zoomMin = 3; 
        private float X, Y;
    }
}
