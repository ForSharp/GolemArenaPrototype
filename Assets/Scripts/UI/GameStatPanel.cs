using System.Collections.Generic;
using GameLoop;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class GameStatPanel : MonoBehaviour
    {
        [SerializeField] private Text roundNumber;
        [SerializeField] private GameObject gameStatTemplatePrefab;
        [SerializeField] private Transform content;

        private CanvasGroup _canvasGroup;
        private bool _isVisible;
        private readonly List<GameObject> _gameStatTemplates = new List<GameObject>();
        private bool _isEndGame;

        private void Start()
        {
            _canvasGroup = GetComponent<CanvasGroup>();
            _canvasGroup.alpha = 0;
        }

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
            if (Input.GetKeyDown(KeyCode.Tab))
            {
                if (!_isVisible)
                {
                    ShowPanel();
                }
                else if (_isVisible)
                {
                    HidePanel();
                }
            }
            
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
        
        private void HidePanel()
        {
            _canvasGroup.alpha = 0;
            _isVisible = false;
        }

        private void ShowPanel()
        {
            _canvasGroup.alpha = 1;
            _isVisible = true;
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
                    Game.AllGolems[i].Spec, Game.AllGolems[i].roundStatistics.RoundDamage,
                    Game.AllGolems[i].roundStatistics.RoundKills, Game.AllGolems[i].roundStatistics.Wins,
                    Game.AllGolems[i].ColorGroup);
            }
        }
        
        private void FillAllTemplatesGameOver()
        {
            _isEndGame = true;
            for (int i = 0; i < _gameStatTemplates.Count; i++)
            {
                _gameStatTemplates[i].GetComponentInParent<GameStatTemplate>().FillValues(Game.AllGolems[i].Type,
                    Game.AllGolems[i].Spec, Game.AllGolems[i].roundStatistics.Damage,
                    Game.AllGolems[i].roundStatistics.Kills, Game.AllGolems[i].roundStatistics.Wins,
                    Game.AllGolems[i].ColorGroup);
            }
            
            ShowPanel();
        }
    }
}
