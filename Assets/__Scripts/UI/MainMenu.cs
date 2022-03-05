using UnityEngine;

namespace __Scripts.UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject chooseHeroPanel;
        [SerializeField] private GameObject statsPanel;
        [SerializeField] private AudioSource buttonClickSound;

        public void StartGame()
        {
            chooseHeroPanel.SetActive(true);
            statsPanel.SetActive(true);
            gameObject.SetActive(false);
            buttonClickSound.Play();
        }

        public void Options()
        {
            //DoSomething
            buttonClickSound.Play();
        }

        public void ExitGame()
        {
            buttonClickSound.Play();
            Application.Quit();
        }
    }
}
