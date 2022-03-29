using System.Collections.Generic;
using System.Linq;
using __Scripts.CharacterEntity;
using __Scripts.GameLoop;
using UnityEngine;

namespace __Scripts.Optimization
{
    public class HeroViewBoxController : MonoBehaviour
    {
        [SerializeField] private GameObject[] heroBoxes;
        [SerializeField] private RenderTexture[] portraitTextures;
        public static HeroViewBoxController Instance;

        private void Awake()
        {
            Instance = this;
        }

        public RenderTexture GetHeroPortrait(CharacterType type)
        {
            return portraitTextures[(int) type];
        }

        public void ActivateBox(CharacterType type)
        {
            heroBoxes[(int) type].SetActive(true);
        }
        
        public void DeactivateRedundantBoxes()
        {
            var types = GetFreeTypes();

            for (var i = 0; i < heroBoxes.Length; i++)
            {
                DeactivateConcreteBox(types, i);
            }
        }

        private void DeactivateConcreteBox(IEnumerable<CharacterType> types, int i)
        {
            foreach (var unused in types.Where(type => (int) type == i))
            {
                heroBoxes[i].SetActive(false);
            }
        }

        private static List<CharacterType> GetFreeTypes()
        {
            return Game.FreeTypes.Select(type => (CharacterType) Game.ToEnum(type, typeof(CharacterType))).ToList();
        }
    }
}