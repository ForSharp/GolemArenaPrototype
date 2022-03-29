using System.Collections;
using UnityEngine;

namespace __Scripts.Optimization
{
    public sealed class CoroutineManager : MonoBehaviour
    {
        private static CoroutineManager _instance
        {
            get
            {
                if (m_instance == null)
                {
                    var go = new GameObject("[COROUTINE MANAGER]");
                    m_instance = go.AddComponent<CoroutineManager>();
                    DontDestroyOnLoad(go);
                }

                return m_instance;
            }
        }

        private static CoroutineManager m_instance;

        public static Coroutine StartRoutine(IEnumerator coroutine)
        {
            return _instance.StartCoroutine(coroutine);
        }

        public static void StopRoutine(Coroutine coroutine)
        {
            _instance.StopCoroutine(coroutine);
        }
    }
}
