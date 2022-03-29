using __Scripts.CharacterEntity;
using __Scripts.Optimization;
using UnityEngine;
using UnityEngine.UI;

namespace __Scripts.UI
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