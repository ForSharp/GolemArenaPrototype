using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatPanel : MonoBehaviour
{
    [SerializeField] private GameObject gameStatTemplatePrefab;
    [SerializeField] private Transform content;
    private List<GameObject> _gameStatTemplates = new List<GameObject>();

    private void Update()
    {
        if (_gameStatTemplates.Count < Game.AllGolemsCount)
        {
            CreateTemplates();
        }
    }

    private void CreateTemplates()
    {
        for (int i = _gameStatTemplates.Count; i < Game.AllGolemsCount; i++)
        {
            var gameStatTemplate = Instantiate(gameStatTemplatePrefab, content);
            _gameStatTemplates.Add(gameStatTemplate);
        }
    }
}
