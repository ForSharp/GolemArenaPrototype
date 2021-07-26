using GolemEntity;
using UnityEngine;
using UnityEngine.UI;

namespace GameLoop
{
    public class HeroPortraitController : MonoBehaviour
    {
        [SerializeField] private RawImage rawImage;
        [SerializeField] private RenderTexture[] portraitTextures;
        
        public void SetTexture(GolemType type)
        {
            rawImage.texture = portraitTextures[(int) type];
        }
    }
}