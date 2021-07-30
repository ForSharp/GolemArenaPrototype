using System;
using System.Collections.Generic;
using System.Linq;
using GolemEntity;
using UnityEngine;

namespace GameLoop
{
    public class HeroViewBoxController : MonoBehaviour
    {
        [SerializeField] private GameObject[] heroBoxes;
        [SerializeField] private RenderTexture[] portraitTextures;
        public static HeroViewBoxController Instance;

        private void Start()
        {
            Instance = this;
        }

        public RenderTexture GetHeroPortrait(GolemType type)
        {
            return portraitTextures[(int) type];
        }

        public void DeactivateRedundantBoxes()
        {
            var types = GetRedundantTypes();

            for (var i = 0; i < heroBoxes.Length; i++)
            {
                DeactivateConcreteBox(types, i);
            }
        }

        private void DeactivateConcreteBox(IEnumerable<GolemType> types, int i)
        {
            foreach (var unused in types.Where(type => (int) type == i))
            {
                heroBoxes[i].SetActive(false);
            }
        }

        private static List<GolemType> GetRedundantTypes()
        {
            return Game.FreeTypes.Select(type => (GolemType) Game.ToEnum(type, typeof(GolemType))).ToList();
        }
    }
}