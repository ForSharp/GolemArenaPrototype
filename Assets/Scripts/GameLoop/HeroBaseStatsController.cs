using GolemEntity;
using UnityEngine;
using UnityEngine.UI;

namespace GameLoop
{
    public class HeroBaseStatsController : MonoBehaviour
    {
        [SerializeField] private Slider strengthSlider;
        [SerializeField] private Slider agilitySlider;
        [SerializeField] private Slider intelligenceSlider;
        [SerializeField] private Slider strengthTypeSpecSlider;
        [SerializeField] private Slider agilityTypeSpecSlider;
        [SerializeField] private Slider intelligenceTypeSpecSlider;

        public void SetTypeStats(GolemType type)
        {
            strengthSlider.value = CharacterStatsService.GetBaseStats(type).Strength;
            agilitySlider.value = CharacterStatsService.GetBaseStats(type).Agility;
            intelligenceSlider.value = CharacterStatsService.GetBaseStats(type).Intelligence;
        }

        public void SetTypeSpecStats(GolemType type, Specialization spec)
        {
            strengthTypeSpecSlider.value = CharacterStatsService.GetBaseStats(type, spec).Strength;
            agilityTypeSpecSlider.value = CharacterStatsService.GetBaseStats(type, spec).Agility;
            intelligenceTypeSpecSlider.value = CharacterStatsService.GetBaseStats(type, spec).Intelligence;
        }
    }
}