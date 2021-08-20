using UnityEngine;

namespace GameLoop
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField] private GameObject startButton;
        [SerializeField] private GameObject optionsButton;
        [SerializeField] private GameObject exitButton;
        [SerializeField] private GameObject chooseHeroPanel;
        [SerializeField] private GameObject statsPanel;

        public void StartGame()
        {
            chooseHeroPanel.SetActive(true);
            statsPanel.SetActive(true);
            this.gameObject.SetActive(false);
        }

        public void Options()
        {
            //DoSomething
        }

        public void ExitGame()
        {
            Application.Quit();
        }
    }
}
