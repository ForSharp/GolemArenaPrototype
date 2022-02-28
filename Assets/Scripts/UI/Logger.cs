using System;
using System.Collections;
using CharacterEntity.CharacterState;
using CharacterEntity.State;
using GameLoop;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Logger : MonoBehaviour
    {
        [SerializeField] private GameObject[] loggerTexts;
        private readonly string[] _textsPool = new string[7];

        private static int _activeTexts;
        
        private void Start()
        {
            for (int i = 0; i < loggerTexts.Length; i++)
            {
                _textsPool[i] = "";
            }
            
            EventContainer.FightEvent += PrintFightInfo;
            EventContainer.MagicDamageReceived += PrintFightInfo;
            StartCoroutine(ClearOldInfo());
        }

        private void OnDisable()
        {
            EventContainer.FightEvent -= PrintFightInfo;
            EventContainer.MagicDamageReceived -= PrintFightInfo;
        }

        private void PrintFightInfo(CharacterState sender, CharacterState target, float damage,
            bool isPeriodic)
        {
            if (isPeriodic)
                return;
            
            if (_activeTexts < loggerTexts.Length - 1)
            {
                AddNewInfo(sender, target, damage);
            }
            else
            {
                MoveNext();
                AddNewInfo(sender, target, damage);
            }
        }
        
        private void PrintFightInfo(object sender, EventArgs args)
        {
            var fightArgs = (FightEventArgs)args;
            if (_activeTexts < loggerTexts.Length - 1)
            {
                AddNewInfo(fightArgs);
            }
            else
            {
                MoveNext();
                AddNewInfo(fightArgs);
            }
        }

        private void AddNewInfo(CharacterState sender, CharacterState target, float damage)
        {
            _textsPool[_activeTexts] = HandleInfo(sender, target, damage, loggerTexts[_activeTexts]);
            _activeTexts++;
        }

        private void AddNewInfo(FightEventArgs fightArgs)
        {
            _textsPool[_activeTexts] = HandleInfo(fightArgs, loggerTexts[_activeTexts]);
            _activeTexts++;
        }

        private void MoveNext()
        {
            for (int i = 0; i < _activeTexts; i++)
            {
                _textsPool[i] = _textsPool[i + 1];
            }

            _textsPool[_activeTexts] = "";
            if (_activeTexts > 0)
                _activeTexts--;
            
            RefreshTextsPosition();
        }

        private IEnumerator ClearOldInfo()
        {
            yield return new WaitForSeconds(3);
            MoveNext();
            StartCoroutine(ClearOldInfo());
        }

        private void RefreshTextsPosition()
        {
            for (int i = 0; i < _textsPool.Length; i++)
            {
                loggerTexts[i].GetComponent<Text>().text = _textsPool[i];
            }
        }

        private static string HandleInfo(CharacterState sender, CharacterState target, float damage, GameObject text)
        {
            var obj = text.GetComponent<Text>();
            
            obj.GetComponent<Text>().text = $"<color=yellow><b>{target.Type}</b> takes <b>{damage:#.}</b> magic damage from <b>{sender.Type}</b></color>";
            
            return obj.text;
        }
        
        private static string HandleInfo(FightEventArgs fightArgs, GameObject text)
        {
            var obj = text.GetComponent<Text>();
            if (fightArgs.IsAvoiding)
            {
                obj.text =
                    $"<color=blue><b>{fightArgs.Target}</b> avoids attack from <b>{fightArgs.AttackHitEventArgs.AttackerName}</b></color>";
            }
            else if (fightArgs.IsAttackFromBehind)
            {
                obj.GetComponent<Text>().text =
                    $"<color=red><b>{fightArgs.Target}</b> takes <b>{fightArgs.AttackHitEventArgs.DamagePerHit:#.}</b> critical damage from <b>{fightArgs.AttackHitEventArgs.AttackerName}</b></color>";
            }
            else
            {
                obj.GetComponent<Text>().text = $"<color=gray><b>{fightArgs.Target}</b> takes <b>{fightArgs.AttackHitEventArgs.DamagePerHit:#.}</b> damage from <b>{fightArgs.AttackHitEventArgs.AttackerName}</b></color>";
            }

            return obj.text;
        }
    }
}