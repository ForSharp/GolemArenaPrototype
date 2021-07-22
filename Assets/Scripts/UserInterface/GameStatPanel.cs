using System.Collections.Generic;
using GameLoop;
using UnityEngine;

namespace UserInterface
{
    public class GameStatPanel : MonoBehaviour
    {
        [SerializeField] private GameObject gameStatTemplatePrefab;
        [SerializeField] private Transform content;
        private List<GameObject> _gameStatTemplates = new List<GameObject>();
        private readonly Game _game = new Game();
        
        private void Update()
        {
            if (_gameStatTemplates.Count < _game.AllGolems.Count)
            {
                CreateTemplates();
            }
            
        }

        private void LateUpdate()
        {
            FillAllTemplates();
        }

        private void CreateTemplates()
        {
            for (int i = _gameStatTemplates.Count; i < _game.AllGolems.Count; i++)
            {
                var gameStatTemplate = Instantiate(gameStatTemplatePrefab, content);
                _gameStatTemplates.Add(gameStatTemplate);
            }
        }

        private void FillAllTemplates()
        {
            for (int i = 0; i < _gameStatTemplates.Count; i++)
            {
                _gameStatTemplates[i].GetComponentInParent<GameStatTemplate>().FillValues(_game.AllGolems[i].Type,
                    _game.AllGolems[i].Spec, _game.AllGolems[i].RoundStatistics.Damage,
                    _game.AllGolems[i].RoundStatistics.Kills, _game.AllGolems[i].RoundStatistics.Wins,
                    _game.AllGolems[i].ColorGroup);
            }
        }
    }
}
