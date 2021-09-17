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
        public float zoom = 3.25f; 
        public float zoomMax = 10; 
        public float zoomMin = 3;
    }
}
