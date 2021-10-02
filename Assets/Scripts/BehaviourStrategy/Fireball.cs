using UnityEngine;

namespace BehaviourStrategy
{
    public class Fireball : DefaultSpell
    {
        //конкретные спеллы также должны наследовать интерфейс IInventoryItem, который будет добавлен позже
        private void Start()
        {
            Timer = 100f;
        }

        private void Update()
        {
            Timer += Time.deltaTime;
        }

        public override void CastSpell()
        {
            transform.LookAt(Target);
            //врубить анимацию, которая вызовет ивент завершения. в этот момент персонаж не уклоняется
        }

        #region AnimationEvents

        public void OnSpellCasted()
        {
            //основная логика спелла здесь
            
            //спелл проверяет, какой у него уровень и в соотвтествии с этим определяются дальнейшие действия
        }

        #endregion
    }
}