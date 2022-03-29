using System;
using __Scripts.GameLoop;
using UnityEngine;
using UnityEngine.UI;

namespace __Scripts.Controller
{
    public class Settings : MonoBehaviour
    {
        [SerializeField] private GameObject settingsPanel;
        [SerializeField] private Slider roundsQuantity;
        [SerializeField] private Slider startMoney;
        [SerializeField] private Slider enemiesQuantity;
        [SerializeField] private Slider soundVolume;
        [SerializeField] private Text roundsText;
        [SerializeField] private Text moneyText;
        [SerializeField] private Text enemiesText;
        [SerializeField] private Text soundVolumeText;

        public static Settings Instance;
        public int RoundsQuantity { get; private set; } //1-10
        public int StartMoney { get; private set;  } //0-10000
        public int EnemiesQuantity { get; private set;  } //1-20
        public float SoundVolume { get; private set; } //0-100

        private void Awake()
        {
            Instance = this;
        }

        private void Start()
        {
            LoadSavedSetting();
        }

        private void LoadSavedSetting()
        {
            RoundsQuantity = PlayerPrefs.GetInt("RoundsQuantity");
            StartMoney = PlayerPrefs.GetInt("StartMoney");
            EnemiesQuantity = PlayerPrefs.GetInt("EnemiesQuantity");
            SoundVolume = PlayerPrefs.GetFloat("SoundVolume");
            
            roundsQuantity.value = RoundsQuantity;
            startMoney.value = StartMoney;
            enemiesQuantity.value = EnemiesQuantity;
            soundVolume.value = SoundVolume;

            roundsText.text = RoundsQuantity.ToString();
            moneyText.text = StartMoney.ToString();
            enemiesText.text = EnemiesQuantity.ToString();
            soundVolumeText.text = SoundVolume.ToString("F0");

        }

        public void SaveSettings()
        {
            PlayerPrefs.SetInt("RoundsQuantity", (int) roundsQuantity.value);
            PlayerPrefs.SetInt("StartMoney", (int) startMoney.value);
            PlayerPrefs.SetInt("EnemiesQuantity", (int) enemiesQuantity.value);
            PlayerPrefs.SetFloat("SoundVolume", soundVolume.value);
            PlayerPrefs.Save();
            
            LoadSavedSetting();
            
            HideSettingsPanel();
        }

        public void UpdateRoundsText()
        {
            roundsText.text = roundsQuantity.value.ToString("F0");
        }
        public void UpdateMoneyText()
        {
            moneyText.text = startMoney.value.ToString("F0");
        }
        public void UpdateEnemiesText()
        {
            enemiesText.text = enemiesQuantity.value.ToString("F0");
        }
        public void UpdateSoundVolumeText()
        {
            soundVolumeText.text = soundVolume.value.ToString("F0");
        }

        public void ShowSettingsPanel()
        {
            settingsPanel.SetActive(true);
            
            roundsText.text = RoundsQuantity.ToString();
            moneyText.text = StartMoney.ToString();
            enemiesText.text = EnemiesQuantity.ToString();
            soundVolumeText.text = SoundVolume.ToString("F0");
        }

        public void HideSettingsPanel()
        {
            settingsPanel.SetActive(false);
        }
    }
}
