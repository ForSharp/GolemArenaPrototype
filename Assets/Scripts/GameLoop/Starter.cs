using System;
using GolemEntity;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace GameLoop
{
    public class Starter : MonoBehaviour
    {
        private const int CharactersCount = 21;
        [SerializeField] private ChoiceHeroPanel panel;
        private MainCharacterParameter _selectedHero;
        private MainCharacterParameter _selectedSpec;
        private bool _isSpecSelected;
        public GolemType[] Characters { get; private set; }
        public Specialization[] Specializations { get; private set; }

        public static Starter Instance;
        
        private void Start()
        {
            Instance = this;
            Characters = GetThreeCharacters();
            Specializations = GetThreeSpecializations();
            
            SetStartRawImages();
        }

        private void SetStartRawImages()
        {
            SetHeroTextures();

            SetHeroStats();

            SetHeroTexts();
            
            SetSpecTexts();
        }

        private void SetHeroTextures()
        {
            panel.heroStrengthPortrait.SetTexture(Characters[0]);
            panel.heroAgilityPortrait.SetTexture(Characters[1]);
            panel.heroIntelligencePortrait.SetTexture(Characters[2]);
        }

        private void SetHeroStats()
        {
            panel.heroStrengthStats.SetTypeStats(Characters[0]);
            panel.heroAgilityStats.SetTypeStats(Characters[1]);
            panel.heroIntelligenceStats.SetTypeStats(Characters[2]);
        }

        private void SetHeroTexts()
        {
            panel.heroStrengthText.SetTypeText(Characters[0]);
            panel.heroAgilityText.SetTypeText(Characters[1]);
            panel.heroIntelligenceText.SetTypeText(Characters[2]);
        }

        private void SetSpecTexts()
        {
            panel.specStrengthText.SetTypeText(Specializations[0]);
            panel.specAgilityText.SetTypeText(Specializations[1]);
            panel.specIntelligenceText.SetTypeText(Specializations[2]);
        }
        
        public void OnHeroStrengthClicked()
        {
            panel.selectedHeroStrength.SetActive(true);
            panel.selectedHeroAgility.SetActive(false);
            panel.selectedHeroIntelligence.SetActive(false);
            
            panel.specializationPanel.SetActive(true);
            _selectedHero = MainCharacterParameter.Strength;
            if (_isSpecSelected)
                SetHeroSpecStats(Characters[0]);
        }
        
        public void OnHeroAgilityClicked()
        {
            panel.selectedHeroStrength.SetActive(false);
            panel.selectedHeroAgility.SetActive(true);
            panel.selectedHeroIntelligence.SetActive(false);
            
            panel.specializationPanel.SetActive(true);
            _selectedHero = MainCharacterParameter.Agility;
            if (_isSpecSelected)
                SetHeroSpecStats(Characters[1]);
        }
        
        public void OnHeroIntelligenceClicked()
        {
            panel.selectedHeroStrength.SetActive(false);
            panel.selectedHeroAgility.SetActive(false);
            panel.selectedHeroIntelligence.SetActive(true);
            
            panel.specializationPanel.SetActive(true);
            _selectedHero = MainCharacterParameter.Intelligence;
            if (_isSpecSelected)
                SetHeroSpecStats(Characters[2]);
        }

        public void OnSpecStrengthClicked()
        {
            panel.selectedSpecStrength.SetActive(true);
            panel.selectedSpecAgility.SetActive(false);
            panel.selectedSpecIntelligence.SetActive(false);
            
            SetHeroSpecStats(Specializations[0]);
            _selectedSpec = MainCharacterParameter.Strength;
            _isSpecSelected = true;
        }
        
        public void OnSpecAgilityClicked()
        {
            panel.selectedSpecStrength.SetActive(false);
            panel.selectedSpecAgility.SetActive(true);
            panel.selectedSpecIntelligence.SetActive(false);
            
            SetHeroSpecStats(Specializations[1]);
            _selectedSpec = MainCharacterParameter.Agility;
            _isSpecSelected = true;
        }
        
        public void OnSpecIntelligenceClicked()
        {
            panel.selectedSpecStrength.SetActive(false);
            panel.selectedSpecAgility.SetActive(false);
            panel.selectedSpecIntelligence.SetActive(true);
            
            SetHeroSpecStats(Specializations[2]);
            _selectedSpec = MainCharacterParameter.Intelligence;
            _isSpecSelected = true;
        }

        private void SetHeroSpecStats(GolemType type)
        {
            switch (_selectedSpec)
            {
                case MainCharacterParameter.Strength:
                    switch (_selectedHero)
                    {
                        case MainCharacterParameter.Strength:
                            panel.heroStrengthStats.SetTypeSpecStats(type, Specializations[0]);
                            panel.heroAgilityStats.DeleteSpecBonus();
                            panel.heroIntelligenceStats.DeleteSpecBonus();
                            break;
                        case MainCharacterParameter.Agility:
                            panel.heroStrengthStats.DeleteSpecBonus();
                            panel.heroAgilityStats.SetTypeSpecStats(type, Specializations[0]);
                            panel.heroIntelligenceStats.DeleteSpecBonus();
                            break;
                        case MainCharacterParameter.Intelligence:
                            panel.heroStrengthStats.DeleteSpecBonus();
                            panel.heroAgilityStats.DeleteSpecBonus();
                            panel.heroIntelligenceStats.SetTypeSpecStats(type, Specializations[0]);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;
                case MainCharacterParameter.Agility:
                    switch (_selectedHero)
                    {
                        case MainCharacterParameter.Strength:
                            panel.heroStrengthStats.SetTypeSpecStats(type, Specializations[1]);
                            panel.heroAgilityStats.DeleteSpecBonus();
                            panel.heroIntelligenceStats.DeleteSpecBonus();
                            break;
                        case MainCharacterParameter.Agility:
                            panel.heroStrengthStats.DeleteSpecBonus();
                            panel.heroAgilityStats.SetTypeSpecStats(type, Specializations[1]);
                            panel.heroIntelligenceStats.DeleteSpecBonus();
                            break;
                        case MainCharacterParameter.Intelligence:
                            panel.heroStrengthStats.DeleteSpecBonus();
                            panel.heroAgilityStats.DeleteSpecBonus();
                            panel.heroIntelligenceStats.SetTypeSpecStats(type, Specializations[1]);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;
                case MainCharacterParameter.Intelligence:
                    switch (_selectedHero)
                    {
                        case MainCharacterParameter.Strength:
                            panel.heroStrengthStats.SetTypeSpecStats(type, Specializations[2]);
                            panel.heroAgilityStats.DeleteSpecBonus();
                            panel.heroIntelligenceStats.DeleteSpecBonus();
                            break;
                        case MainCharacterParameter.Agility:
                            panel.heroStrengthStats.DeleteSpecBonus();
                            panel.heroAgilityStats.SetTypeSpecStats(type, Specializations[2]);
                            panel.heroIntelligenceStats.DeleteSpecBonus();
                            break;
                        case MainCharacterParameter.Intelligence:
                            panel.heroStrengthStats.DeleteSpecBonus();
                            panel.heroAgilityStats.DeleteSpecBonus();
                            panel.heroIntelligenceStats.SetTypeSpecStats(type, Specializations[2]);
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        
        private void SetHeroSpecStats(Specialization spec)
        {
            switch (_selectedHero)
            {
                case MainCharacterParameter.Strength:
                    panel.heroStrengthStats.SetTypeSpecStats(Characters[0], spec);
                    panel.heroAgilityStats.DeleteSpecBonus();
                    panel.heroIntelligenceStats.DeleteSpecBonus();
                    break;
                case MainCharacterParameter.Agility:
                    panel.heroStrengthStats.DeleteSpecBonus();
                    panel.heroAgilityStats.SetTypeSpecStats(Characters[1], spec);
                    panel.heroIntelligenceStats.DeleteSpecBonus();
                    break;
                case MainCharacterParameter.Intelligence:
                    panel.heroStrengthStats.DeleteSpecBonus();
                    panel.heroAgilityStats.DeleteSpecBonus();
                    panel.heroIntelligenceStats.SetTypeSpecStats(Characters[2], spec);
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
        
        private static GolemType[] GetThreeCharacters()
        {
            return new[]
            {
                GetCharacterWithRightMainParameter(MainCharacterParameter.Strength),
                GetCharacterWithRightMainParameter(MainCharacterParameter.Agility),
                GetCharacterWithRightMainParameter(MainCharacterParameter.Intelligence)
            };
        }

        private static GolemType GetCharacterWithRightMainParameter(MainCharacterParameter parameter)
        {
            while (true)
            {
                var character = GetRandomCharacter();
                if (CharacterStatsService.GetMainCharacterParameter(character) != parameter) continue;
                return character;
            }
        }

        private static GolemType GetRandomCharacter()
        {
            return (GolemType) Random.Range(0, CharactersCount);
        }

        private static Specialization[] GetThreeSpecializations()
        {
            return new[]
            {
                GetSpecializationWithRightMainParameter(MainCharacterParameter.Strength),
                GetSpecializationWithRightMainParameter(MainCharacterParameter.Agility),
                GetSpecializationWithRightMainParameter(MainCharacterParameter.Intelligence)
            };
        }

        private static Specialization GetSpecializationWithRightMainParameter(MainCharacterParameter parameter)
        {
            while (true)
            {
                var specialization = GetRandomSpecialization();
                if (CharacterStatsService.GetMainCharacterParameter(specialization) != parameter) continue;
                return specialization;
            }
        }
        
        private static Specialization GetRandomSpecialization()
        {
            return (Specialization) Random.Range(0, CharactersCount);
        }
    }
}
