using System;
using GolemEntity;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameLoop
{
    public class SpecFeatures: MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private GameObject heroFeaturesInfoPanel;
        [SerializeField] private Transform hintsPanel;
        [SerializeField] private MainCharacterParameter parameter;
        private GameObject _infoPanel;
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            _infoPanel = Instantiate(heroFeaturesInfoPanel,
                new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z), Quaternion.identity,
                hintsPanel);
            var panel = heroFeaturesInfoPanel.GetComponent<HeroFeaturesInfoPanel>();
            SetValues(panel);
        }

        private void SetValues(HeroFeaturesInfoPanel panel)
        {
            switch (parameter)
            {
                case MainCharacterParameter.Strength:
                    panel.FillValues(CharacterStatsService.GetCharacterFeatures(Starter.Instance.Specializations[0]), _infoPanel.transform);
                    break;
                case MainCharacterParameter.Agility:
                    panel.FillValues(CharacterStatsService.GetCharacterFeatures(Starter.Instance.Specializations[1]), _infoPanel.transform);
                    break;
                case MainCharacterParameter.Intelligence:
                    panel.FillValues(CharacterStatsService.GetCharacterFeatures(Starter.Instance.Specializations[2]), _infoPanel.transform);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            Destroy(_infoPanel.gameObject);
        }
    }
}