using UnityEngine;
using UnityEngine.UI;

namespace __Scripts.UI
{
    public class RoundRewardSlot : MonoBehaviour
    {
        [SerializeField] private Image image;
        [SerializeField] private Text amount;

        public void SetInfo(Sprite icon, string itemAmount)
        {
            image.sprite = icon;
            amount.text = itemAmount;
        }
    }
}