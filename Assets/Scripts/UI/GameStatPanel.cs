using System.Collections.Generic;
using GameLoop;
using UnityEngine;
using UnityEngine.UI;
using UserInterface;

namespace UI
{
    public class GameStatPanel : MonoBehaviour
    {
        [SerializeField] private Text roundNumber;
        [SerializeField] private GameObject gameStatTemplatePrefab;
        [SerializeField] private Transform content;
        private readonly List<GameObject> _gameStatTemplates = new List<GameObject>();
        private bool _isEndGame = false;

        private void OnEnable()
        {
            Game.EndGame += FillAllTemplatesGameOver;
        }

        private void OnDisable()
        {
            Game.EndGame -= FillAllTemplatesGameOver;
        }

        private void Update()
        {
            if (_gameStatTemplates.Count < Game.AllGolems.Count)
            {
                CreateTemplates();
            }

            if (gameStatTemplatePrefab)
                roundNumber.text = Game.Round.ToString();
        }

        private void LateUpdate()
        {
            if (gameStatTemplatePrefab && !_isEndGame)
                FillAllTemplates();
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
                    Game.AllGolems[i].Spec, Game.AllGolems[i].RoundStatistics.RoundDamage,
                    Game.AllGolems[i].RoundStatistics.RoundKills, Game.AllGolems[i].RoundStatistics.Wins,
                    Game.AllGolems[i].ColorGroup);
            }
        }
        
        private void FillAllTemplatesGameOver()
        {
            _isEndGame = true;
            for (int i = 0; i < _gameStatTemplates.Count; i++)
            {
                _gameStatTemplates[i].GetComponentInParent<GameStatTemplate>().FillValues(Game.AllGolems[i].Type,
                    Game.AllGolems[i].Spec, Game.AllGolems[i].RoundStatistics.Damage,
                    Game.AllGolems[i].RoundStatistics.Kills, Game.AllGolems[i].RoundStatistics.Wins,
                    Game.AllGolems[i].ColorGroup);
            }
        }
    }
}
