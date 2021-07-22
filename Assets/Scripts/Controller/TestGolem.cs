﻿using GolemEntity;
using UnityEngine;

namespace Controller
{
    public class TestGolem : MonoBehaviour
    {
        private Golem _golem;
        private void CreateGolem()
        {
            
            ShowAll();
        }

        private void Start()
        {
            CreateGolem();
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                _golem.ChangeBaseStatsProportionally(10);
                ShowAll();
            }
            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                _golem.ChangeBaseStatsProportionally(-10);
                ShowAll();
            }
        }

        private void ShowAll()
        {
            ShowSync("red");
            ShowSync("blue");
       
        }

        private void ShowSync(string color)
        {
            Debug.Log($"<color={color}>{_golem.GetGolemBaseStats()}</color>");
            Debug.Log($"<color={color}>{_golem.GetGolemExtraStats()}</color>");
        }
    
    }
}