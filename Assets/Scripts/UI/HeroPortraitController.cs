using CharacterEntity;
using Optimization;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HeroPortraitController : MonoBehaviour
    {
        [SerializeField] private RawImage rawImage;

        public void SetTexture(CharacterType type)
        {
            rawImage.texture = HeroViewBoxController.Instance.GetHeroPortrait(type);
        }
    }
}