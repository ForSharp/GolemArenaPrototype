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
        [SerializeField] private Slider qualityLevel;
        [SerializeField] private Text roundsText;
        [SerializeField] private Text moneyText;
        [SerializeField] private Text enemiesText;
        [SerializeField] private Text soundVolumeText;
        [SerializeField] private Text qualityLevelText;
        
        public static Settings instance;
        public int RoundsQuantity { get; private set; } //1-10
        public int StartMoney { get; private set;  } //0-10000
        public int EnemiesQuantity { get; private set;  } //1-20
        public float SoundVolume { get; private set; } //0-100
        public int QualityLevel { get; private set; } //1-6
        
        private void Awake()
        {
            instance = this;
        }

        private void Start()
        {
            LoadSavedSetting();
        }

        private void ChangeQuality(int qualityLvl)
        {
            QualitySettings.SetQualityLevel(qualityLvl, true);
        }

        private void LoadSavedSetting()
        {
            RoundsQuantity = PlayerPrefs.GetInt("RoundsQuantity");
            StartMoney = PlayerPrefs.GetInt("StartMoney");
            EnemiesQuantity = PlayerPrefs.GetInt("EnemiesQuantity");
            SoundVolume = PlayerPrefs.GetFloat("SoundVolume");
            QualityLevel = PlayerPrefs.GetInt("QualityLevel");
            
            roundsQuantity.value = RoundsQuantity;
            startMoney.value = StartMoney;
            enemiesQuantity.value = EnemiesQuantity;
            soundVolume.value = SoundVolume;
            qualityLevel.value = QualityLevel;

            roundsText.text = RoundsQuantity.ToString();
            moneyText.text = StartMoney.ToString();
            enemiesText.text = EnemiesQuantity.ToString();
            soundVolumeText.text = SoundVolume.ToString("F0");
            qualityLevelText.text = GetQualityLevel(QualityLevel);

            if (QualitySettings.GetQualityLevel() != QualityLevel)
            {
                ChangeQuality(QualityLevel);
            }
        }

        private string GetQualityLevel(int level)
        {
            switch (level)
            {
                case 0:
                    return "Very Low";
                case 1:
                    return "Low";
                case 2:
                    return "Medium";
                case 3:
                    return "High";
                case 4:
                    return "Very High";
                case 5:
                    return "Ultra";
                default:
                    return "Untitled";
            }    
            
        }
        
        public void SaveSettings()
        {
            PlayerPrefs.SetInt("RoundsQuantity", (int) roundsQuantity.value);
            PlayerPrefs.SetInt("StartMoney", (int) startMoney.value);
            PlayerPrefs.SetInt("EnemiesQuantity", (int) enemiesQuantity.value);
            PlayerPrefs.SetFloat("SoundVolume", soundVolume.value);
            PlayerPrefs.SetInt("QualityLevel", (int) qualityLevel.value);
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
        
        public void UpdateQualityLevelText()
        {
            qualityLevelText.text = GetQualityLevel((int)qualityLevel.value);
        }

        public void ShowSettingsPanel()
        {
            settingsPanel.SetActive(true);

            LoadSavedSetting();
        }

        public void HideSettingsPanel()
        {
            settingsPanel.SetActive(false);
        }
    }
}
