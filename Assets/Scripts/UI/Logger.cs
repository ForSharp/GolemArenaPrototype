using System;
using System.Collections;
using System.Collections.Generic;
using GameLoop;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class Logger : MonoBehaviour
    {
        [SerializeField] private GameObject loggerTextPrefab;
        private Queue<GameObject> _textsPool = new Queue<GameObject>();

        private void Start()
        {
            EventContainer.FightEvent += PrintFightInfo;
            StartCoroutine(DeleteOldInfo());
        }

        private void OnDisable()
        {
            EventContainer.FightEvent -= PrintFightInfo;
        }

        private void PrintFightInfo(object sender, EventArgs args)
        {
            var fightArgs = (FightEventArgs) args;
            var text = Instantiate(loggerTextPrefab, gameObject.transform);

            HandleInfo(fightArgs, text);
            _textsPool.Enqueue(text);
        }

        private IEnumerator DeleteOldInfo()
        {
            yield return new WaitForSeconds(2);
            if (_textsPool.Count > 0)
            {
                Destroy(_textsPool.Peek());
                _textsPool.Dequeue();
            }

            StartCoroutine(DeleteOldInfo());
        }

        private static void HandleInfo(FightEventArgs fightArgs, GameObject text)
        {
            var obj = text.GetComponent<Text>();
            if (fightArgs.IsAvoiding)
            {
                obj.text = fightArgs.Target + "avoids attack from" + fightArgs.AttackHitEventArgs
                    .AttackerName;
                obj.color = Color.blue;
                
            }
            else if (fightArgs.IsAttackFromBehind)
            {
                obj.GetComponent<Text>().text = fightArgs.Target +
                                                $"takes {fightArgs.AttackHitEventArgs.DamagePerHit} damage from behind from" +
                                                fightArgs.AttackHitEventArgs
                                                    .AttackerName;
                obj.color = Color.red;
            }
            else
            {
                obj.GetComponent<Text>().text = fightArgs.Target +
                                                $"takes {fightArgs.AttackHitEventArgs.DamagePerHit} damage from " + fightArgs
                                                    .AttackHitEventArgs
                                                    .AttackerName;
                obj.color = Color.green;
            }
        }
    }
}
