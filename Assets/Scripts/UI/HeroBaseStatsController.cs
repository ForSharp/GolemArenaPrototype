using GolemEntity;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HeroBaseStatsController : MonoBehaviour
    {
        [SerializeField] private Slider strengthSlider;
        [SerializeField] private Slider agilitySlider;
        [SerializeField] private Slider intelligenceSlider;
        [SerializeField] private Slider strengthTypeSpecSlider;
        [SerializeField] private Slider agilityTypeSpecSlider;
        [SerializeField] private Slider intelligenceTypeSpecSlider;
        public GolemType GolemType { get; private set; }
        public Specialization Specialization { get; private set; }

        public void SetTypeStats(GolemType type)
        {
            GolemType = type;
            strengthSlider.value = CharacterStatsService.GetBaseStats(type).strength;
            agilitySlider.value = CharacterStatsService.GetBaseStats(type).agility;
            intelligenceSlider.value = CharacterStatsService.GetBaseStats(type).intelligence;
        }

        public void SetTypeSpecStats(GolemType type, Specialization spec)
        {
            GolemType = type;
            Specialization = spec;
            strengthTypeSpecSlider.value = CharacterStatsService.GetBaseStats(type, spec).strength;
            agilityTypeSpecSlider.value = CharacterStatsService.GetBaseStats(type, spec).agility;
            intelligenceTypeSpecSlider.value = CharacterStatsService.GetBaseStats(type, spec).intelligence;
        }

        public void DeleteSpecBonus()
        {
            strengthTypeSpecSlider.value = 0;
            agilityTypeSpecSlider.value = 0;
            intelligenceTypeSpecSlider.value = 0;
        }
    }
}