﻿using UnityEngine;
#if ENABLE_INPUT_SYSTEM
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.UI;
#endif

namespace Modern_UI_Pack.Scripts.Demo
{
    public class InputSystemChecker : MonoBehaviour
    {
        void Awake()
        {
#if ENABLE_INPUT_SYSTEM
            StandaloneInputModule tempModule = gameObject.GetComponent<StandaloneInputModule>();
            Destroy(tempModule);
            InputSystemUIInputModule newModule = gameObject.AddComponent<InputSystemUIInputModule>();
            newModule.enabled = false;
            newModule.enabled = true;
#endif
        }
    }
}