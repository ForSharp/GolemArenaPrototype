using System;
using GolemEntity;
using UnityEngine;
using UnityEngine.EventSystems;

namespace GameLoop
{
    public class HeroFeatures : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        [SerializeField] private GameObject heroFeaturesInfoPanel;
        [SerializeField] private HeroBaseStatsController baseStatsController;
        [SerializeField] private Transform hintsPanel;
        private GameObject _infoPanel;

        public void OnPointerEnter(PointerEventData eventData)
        {
            _infoPanel = Instantiate(heroFeaturesInfoPanel,
                new Vector3(Input.mousePosition.x, Input.mousePosition.y, transform.position.z), Quaternion.identity,
                hintsPanel);
            var panel = heroFeaturesInfoPanel.GetComponent<HeroFeaturesInfoPanel>();
            panel.FillValues(CharacterStatsService.GetCharacterFeatures(baseStatsController.GolemType), _infoPanel.transform);
        }
        
        public void OnPointerExit(PointerEventData eventData)
        {
            Destroy(_infoPanel.gameObject);
        }
    }
}