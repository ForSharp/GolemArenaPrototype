using System.Collections;
using UnityEngine;

namespace Optimization
{
    public sealed class CoroutineStarter : MonoBehaviour
    {
        private static CoroutineStarter _instance
        {
            get
            {
                if (m_instance == null)
                {
                    var go = new GameObject("[COROUTINE MANAGER]");
                    m_instance = go.AddComponent<CoroutineStarter>();
                    DontDestroyOnLoad(go);
                }

                return m_instance;
            }
        }

        private static CoroutineStarter m_instance;

        public static void StartRoutine(IEnumerator coroutine)
        {
            _instance.StartCoroutine(coroutine);
        }

        public static void StopRoutine(Coroutine coroutine)
        {
            _instance.StopCoroutine(coroutine);
        }
    }
}
