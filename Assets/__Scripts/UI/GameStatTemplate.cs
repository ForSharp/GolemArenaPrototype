using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace UI
{
    public class GameStatTemplate : MonoBehaviour
    {
        [FormerlySerializedAs("golemTypeText")] [SerializeField] private Text characterTypeText;
        [SerializeField] private Text specializationText;
        [SerializeField] private Text damageText;
        [SerializeField] private Text killsText;
        [SerializeField] private Text winsText;
        [SerializeField] private GameObject backGround;
        private bool _isReadyToUpd;

        private void Update()
        {
            if (_isReadyToUpd)
            {
                characterTypeText.text = characterTypeText.text;
                specializationText.text = specializationText.text;
                damageText.text = damageText.text;
                killsText.text = killsText.text;
                winsText.text = winsText.text;
                backGround.GetComponent<Image>().color = backGround.GetComponent<Image>().color;
            
                _isReadyToUpd = false;
            }
        }

        public void FillValues(string golemType, string specialization, float damage, int kills, int wins, Color color)
        {
            characterTypeText.text = golemType;
            specializationText.text = specialization;
            damageText.text = damage.ToString("#.00");
            killsText.text = kills.ToString();
            winsText.text = GetStarsBasedOnWins(wins);
            backGround.GetComponent<Image>().color = color;

            _isReadyToUpd = true;
        }

        private string GetStarsBasedOnWins(int wins)
        {
            const char star = '★';
            string stars = null;
            for (var i = 0; i < wins; i++)
            {
                stars += star;
            }

            return stars;
        }
    }
}