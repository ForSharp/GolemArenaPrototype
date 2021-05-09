﻿using __Scripts;
using __Scripts.GolemEntity;
using UnityEngine;

public class TestGolem : MonoBehaviour
{
    private Golem _golem;
    private void CreateGolem()
    {
        _golem = new Golem(GolemType.IronGolem, Specialization.Warrior);
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
        //ShowSync("green");
        //ShowAsync();
    }

    private void ShowSync(string color)
    {
        Debug.Log($"<color={color}>{_golem.GetGolemBaseStats()}</color>");
        Debug.Log($"<color={color}>{_golem.GetGolemExtraStats()}</color>");
        Debug.Log($"<color={color}>{_golem.GetGolemExtraStats()}</color>");
    }

    private void ShowAsync()
    {
        _golem.ShowGolemBaseStats();
        _golem.ShowGolemExtraStats();
    }
}