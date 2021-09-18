using System;
using UnityEngine;

namespace Controller
{
    [Serializable]
    public class CameraRtsSettings
    {
        public float moveSpeed = 20f;
        public float scrollSpeed = 100f;
        public float borderThickness = 10f;
        public Vector2 limitX = new Vector2(-40, 40);
        public Vector2 limitY = new Vector2(10, 40);
        public Vector2 limitZ = new Vector2(-60, 50);
    }
}