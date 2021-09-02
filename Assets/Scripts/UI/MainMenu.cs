using UnityEngine;

namespace UI
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject startButton;
        [SerializeField] private GameObject optionsButton;
        [SerializeField] private GameObject exitButton;
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
