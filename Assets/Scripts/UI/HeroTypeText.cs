using GolemEntity;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class HeroTypeText : MonoBehaviour
    {
        [SerializeField] private Text heroText;

        public void SetTypeText(GolemType type)
        {
            heroText.text = type.ToString();
        }
        
        public void SetTypeText(Specialization spec)
        {
            heroText.text = spec.ToString();
        }
    }
}
