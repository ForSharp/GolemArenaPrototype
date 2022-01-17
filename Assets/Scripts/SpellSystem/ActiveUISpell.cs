using Inventory.Abstracts;
using Inventory.Abstracts.Spells;
using UnityEngine;
using UnityEngine.UI;

namespace SpellSystem
{
    public class ActiveUISpell: MonoBehaviour
    {
        [SerializeField] private Button spellChanger;
        [SerializeField] private Image spellIcon;
        [SerializeField] private Text spellLvl;
        [SerializeField] private Image cooldown;
        public bool IsActive { get; private set; }
        public bool InCooldown { get; private set; }
        public ISpellItem SpellItem { get; private set; }
        public string SpellId { get; private set; }
        
        private float _cooldownDuration;
        
        private void Start()
        {
            spellChanger.gameObject.SetActive(false);
        }
        
        private void Update()
        {
            if (cooldown.fillAmount > 0)
            {
                cooldown.fillAmount -= _cooldownDuration * Time.deltaTime;
            }

            if (cooldown.fillAmount == 0)
            {
                InCooldown = false;
            }
        }

        public void StartCooldown()
        {
            cooldown.fillAmount = 1;
            InCooldown = true;
        }

        public void EndCooldown()
        {
            cooldown.fillAmount = 0;
            InCooldown = false;
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
    }
}