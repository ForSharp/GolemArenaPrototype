using UnityEngine;
using UnityEngine.UI;

namespace SpellSystem
{
    public class ActiveUISpell: MonoBehaviour
    {
        [SerializeField] private Image reminder;
        [SerializeField] private Button spellChanger;
        public bool IsActive { get; private set; }

        public void ActivateSpell()
        {
            IsActive = true;
            reminder.gameObject.SetActive(false);
        }

        public void DeactivateSpell()
        {
            IsActive = false;
            reminder.gameObject.SetActive(true);
        }
    }
}