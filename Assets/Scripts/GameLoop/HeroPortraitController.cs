using GolemEntity;
using UnityEngine;
using UnityEngine.UI;

namespace GameLoop
{
    public class HeroPortraitController : MonoBehaviour
    {
        [SerializeField] private RawImage rawImage;

        public void SetTexture(GolemType type)
        {
            rawImage.texture = HeroViewBoxController.Instance.GetHeroPortrait(type);
        }
    }
}