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

        private void Start()
        {
            SetStartRawImages();
        }

        private void SetStartRawImages()
        {
            var characters = GetThreeCharacters(); 
            
            panel.heroStrengthPortrait.SetTexture(characters[0]);
            panel.heroAgilityPortrait.SetTexture(characters[1]);
            panel.heroIntelligencePortrait.SetTexture(characters[2]);
            
            panel.heroStrengthStats.SetTypeStats(characters[0]);
            panel.heroAgilityStats.SetTypeStats(characters[1]);
            panel.heroIntelligenceStats.SetTypeStats(characters[2]);
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
    }
}
