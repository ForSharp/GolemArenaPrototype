using System;
using GameLoop;
using GolemEntity;
using UnityEngine;
using UnityEngine.UI;

namespace Controller
{
    public class UserInputTest : MonoBehaviour
    {
        [SerializeField] private GameObject panelGolemType;
        [SerializeField] private GameObject panelGolemSpec;

        private GolemType _golemType;
        private Specialization _specialization;

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                panelGolemType.SetActive(true);
                panelGolemSpec.SetActive(false);
            }
        }
    
        public void SetGolemType(Text text)
        {
            panelGolemType.SetActive(false);
        
            _golemType = (GolemType) ToEnum(text.text, typeof(GolemType));
        
            panelGolemSpec.SetActive(true);
        }

        public void SetGolemSpec(Text text)
        {
            _specialization = (Specialization) ToEnum(text.text, typeof(Specialization));

            panelGolemSpec.SetActive(false);
        
            CreateGolem();
        }

        private void CreateGolem()
        {
            GetComponent<Spawner>().SpawnGolem(_golemType, _specialization);
        }

        private static Enum ToEnum(string value, Type enumType)
        {
            return (Enum)Enum.Parse(enumType, value, true);
        }
    }
}