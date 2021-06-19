using System.Collections.Generic;
using UnityEngine;

public class GameStatPanel : MonoBehaviour
{
    [SerializeField] private GameObject gameStatTemplatePrefab;
    [SerializeField] private Transform content;
    private List<GameObject> _gameStatTemplates = new List<GameObject>();

    private void Update()
    {
        if (_gameStatTemplates.Count < Game.AllGolems.Count)
        {
            CreateTemplates();
            FillAllTemplates();
        }
    }

    private void CreateTemplates()
    {
        for (int i = _gameStatTemplates.Count; i < Game.AllGolems.Count; i++)
        {
            var gameStatTemplate = Instantiate(gameStatTemplatePrefab, content);
            _gameStatTemplates.Add(gameStatTemplate);
        }
    }

    private void FillAllTemplates()
    {
        for (int i = 0; i < _gameStatTemplates.Count; i++)
        {
            _gameStatTemplates[i].GetComponentInParent<GameStatTemplate>().FillValues(Game.AllGolems[i].Type,
                Game.AllGolems[i].Spec, Game.AllGolems[i].RoundStatistics.Damage,
                Game.AllGolems[i].RoundStatistics.Kills, Game.AllGolems[i].RoundStatistics.Wins,
                Game.AllGolems[i].ColorGroup);
        }
    }
}
