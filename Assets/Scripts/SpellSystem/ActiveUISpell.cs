using Inventory.Abstracts;
using Inventory.Abstracts.Spells;
using UnityEngine;
using UnityEngine.UI;

namespace SpellSystem
{
    public class ActiveUISpell : MonoBehaviour
    {
        [SerializeField] private Button spellChanger;
        [SerializeField] private Image spellIcon;
        [SerializeField] private Text spellLvl;
        [SerializeField] private Image cooldown;
        [SerializeField] private Image imageMark;
        [SerializeField] private Text cooldownText;
        public bool IsActive { get; private set; }
        public bool InCooldown { get; private set; }
        public ISpellItem SpellItem { get; private set; }
        public string SpellId { get; private set; }

        private float _cooldownDuration;
        private float _currentCooldown;

        private void Start()
        {
            spellChanger.gameObject.SetActive(false);
        }

        private void Update()
        {
            if (_currentCooldown > 0)
            {
                _currentCooldown -= Time.deltaTime;
                cooldown.fillAmount = _currentCooldown / _cooldownDuration;
                cooldownText.text = _currentCooldown.ToString("F1");
            }
            else
            {
                EndCooldown();
            }
        }

        public void StartCooldown()
        {
            _currentCooldown = _cooldownDuration;
            InCooldown = true;
        }

        public void EndCooldown()
        {
            _currentCooldown = 0;
            InCooldown = false;
            cooldownText.text = "";
        }

        public void ActivateSpell(ISpellItem spellItem)
        {
            SpellItem = spellItem;
            IsActive = true;
            var item = (IInventoryItem)spellItem;
            spellIcon.sprite = item.Info.SpriteIcon;
            spellChanger.gameObject.SetActive(true);
            spellLvl.text = GetStringSpellLvl(spellItem);
            SpellId = item.Info.Id;
            _cooldownDuration = 60 / spellItem.SpellInfo.Cooldown;
        }

        private string GetStringSpellLvl(ISpellItem spellItem)
        {
            switch (spellItem.SpellInfo.SpellLvl)
            {
                case 1:
                    return "<color=white>★</color><color=black>★★</color>";
                case 2:
                    return "<color=white>★★</color><color=black>★</color>";
                case 3:
                    return "<color=white>★★★</color>";
                default:
                    return "";
            }
        }

        public void DeactivateSpell()
        {
            IsActive = false;
            spellChanger.gameObject.SetActive(false);
            SpellItem = null;
        }

        public void MarkSpell()
        {
            imageMark.gameObject.SetActive(true);
        }

        public void StopMarkSpell()
        {
            imageMark.gameObject.SetActive(false);
        }
    }
}