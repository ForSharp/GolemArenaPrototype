using UI;
using UnityEngine;

namespace GameLoop
{
    public class ChoiceHeroPanel : MonoBehaviour
    {
        public HeroPortraitController heroStrengthPortrait;
        public HeroPortraitController heroAgilityPortrait;
        public HeroPortraitController heroIntelligencePortrait;
        public HeroBaseStatsController heroStrengthStats;
        public HeroBaseStatsController heroAgilityStats;
        public HeroBaseStatsController heroIntelligenceStats;
        public HeroTypeText heroStrengthText;
        public HeroTypeText heroAgilityText;
        public HeroTypeText heroIntelligenceText;
        public GameObject selectedHeroStrength;
        public GameObject selectedHeroAgility;
        public GameObject selectedHeroIntelligence;
        public GameObject specializationPanel;
        public GameObject selectedSpecStrength;
        public GameObject selectedSpecAgility;
        public GameObject selectedSpecIntelligence;
        public HeroTypeText specStrengthText;
        public HeroTypeText specAgilityText;
        public HeroTypeText specIntelligenceText;
    }
}