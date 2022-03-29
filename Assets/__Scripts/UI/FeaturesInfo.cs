using System;
using System.Collections;
using __Scripts.CharacterEntity;
using __Scripts.GameLoop;
using UnityEngine;
using UnityEngine.EventSystems;

namespace __Scripts.UI
{
    public class FeaturesInfo : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private GameObject heroFeaturesInfoPanel;
        [SerializeField] private Transform hintsPanel;
        [SerializeField] private MainCharacterParameter parameter;
        [SerializeField] private FeatureType featureType;
        private GameObject _infoPanel;
        private Coroutine _coroutine;
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
            _coroutine = StartCoroutine(ShowInfoPanelAfterDelay());
        }

        private IEnumerator ShowInfoPanelAfterDelay()
        {
            if (_infoPanel)
                Destroy(_infoPanel.gameObject);
            
            yield return new WaitForSeconds(1);
            
            if (_infoPanel)
                Destroy(_infoPanel.gameObject);
            
            _infoPanel = Instantiate(heroFeaturesInfoPanel,
                new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z), Quaternion.identity,
                hintsPanel);
            var panel = heroFeaturesInfoPanel.GetComponent<HeroFeaturesInfoPanel>();
            SetValues(panel);
        }
        
        private void SetValues(HeroFeaturesInfoPanel panel)
        {
            switch (featureType)
            {
                case FeatureType.Character:
                    SetTypeFeatures(panel);
                    break;
                case FeatureType.Specialization:
                    SetSpecFeatures(panel);
                    break;
                case FeatureType.All:
                    SetAllFeatures(panel);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SetAllFeatures(HeroFeaturesInfoPanel panel)
        {
            panel.FillValues(
                CharacterStatsService.GetCharacterFeatures(PlayerCharacterSelector.Instance.GetSelectedType(),
                    PlayerCharacterSelector.Instance.GetSelectedSpec()), _infoPanel.transform);
        }

        private void SetTypeFeatures(HeroFeaturesInfoPanel panel)
        {
            switch (parameter)
            {
                case MainCharacterParameter.Strength:
                    panel.FillValues(
                        CharacterStatsService.GetCharacterFeatures(PlayerCharacterSelector.Instance.Characters[0]),
                        _infoPanel.transform);
                    break;
                case MainCharacterParameter.Agility:
                    panel.FillValues(
                        CharacterStatsService.GetCharacterFeatures(PlayerCharacterSelector.Instance.Characters[1]),
                        _infoPanel.transform);
                    break;
                case MainCharacterParameter.Intelligence:
                    panel.FillValues(
                        CharacterStatsService.GetCharacterFeatures(PlayerCharacterSelector.Instance.Characters[2]),
                        _infoPanel.transform);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void SetSpecFeatures(HeroFeaturesInfoPanel panel)
        {
            switch (parameter)
            {
                case MainCharacterParameter.Strength:
                    panel.FillValues(
                        CharacterStatsService.GetCharacterFeatures(PlayerCharacterSelector.Instance.Specializations[0]),
                        _infoPanel.transform);
                    break;
                case MainCharacterParameter.Agility:
                    panel.FillValues(
                        CharacterStatsService.GetCharacterFeatures(PlayerCharacterSelector.Instance.Specializations[1]),
                        _infoPanel.transform);
                    break;
                case MainCharacterParameter.Intelligence:
                    panel.FillValues(
                        CharacterStatsService.GetCharacterFeatures(PlayerCharacterSelector.Instance.Specializations[2]),
                        _infoPanel.transform);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            if (_coroutine != null)
            {
                StopCoroutine(_coroutine);
            }
            
            if (_infoPanel)
                Destroy(_infoPanel.gameObject);
        }
        
        private enum FeatureType
        {
            Character,
            Specialization,
            All
        }
    }
}