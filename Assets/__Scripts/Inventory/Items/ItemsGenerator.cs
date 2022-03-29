using System;
using __Scripts.CharacterEntity.ExtraStats;
using UnityEngine;
using Random = UnityEngine.Random;

namespace __Scripts.Inventory.Items
{
    public class ItemsGenerator : MonoBehaviour
    {
        private void Start()
        {
            // var result = "";
            // for (int i = 0; i < 50; i++)
            // {
            //     result += "\n" + GetResultArtefact();
            // }
            // Debug.Log(result);

            var result = ""; 

            for (int i = 0; i < 25; i++)
            {
                var info = GetOneStat(out var price);
                result += "\n" + price + " " + info;
            }
            Debug.Log(result);
        }
        

        private string GetResultArtefact()
        {
            var statsQuantity = Random.Range(1, 5);

            var totalPrice = 0;

            var info = "";

            
            
            for (int i = 0; i < statsQuantity; i++)
            {
                info += GetOneStatChange(out var price);
                totalPrice += price;
            }
            
            
            return totalPrice + info + " price: " + totalPrice;
            
            
        }

        private string GetOneStat(out int price)
        {
            var statNumb = Random.Range(1, 15);
            price = 0;

            var isMultiply = Random.Range(0, 2) != 0;

            var rate = Random.Range(20, 101);

            var duration = Random.Range(10, 31);

            if (isMultiply)
            {
                price += rate * 10 * duration / 10;
            }
            else
            {
                price += rate * 2 * duration / 10;
            }

            var rateType = isMultiply ? "мульти" : "плоский";
            
            return $" {GetEnum(statNumb)}, {rateType}, {rate}, {duration} sec";
        }
        
        private string GetOneStatChange(out int price)
        {
            var statNumb = Random.Range(1, 15);
            price = 100;

            var isMultiply = Random.Range(0, 2) != 0;

            var rate = Random.Range(5, 51);

            if (isMultiply)
            {
                price += rate * 40;
            }
            else
            {
                price += rate * 10;
            }

            var rateType = isMultiply ? "мульти" : "плоский";
            
            return $" {GetEnum(statNumb)}, {rateType}, {rate}";
        }

        private string GetEnum(int numb)
        {
            switch (numb)
            {
                case 1:
                    return ExtraStats.AttackSpeed.ToString();
                case 2:
                    return ExtraStats.AvoidChance.ToString();
                case 3:
                    return ExtraStats.DamagePerHeat.ToString();
                case 4:
                    return ExtraStats.Defence.ToString();
                case 5:
                    return ExtraStats.DodgeChance.ToString();
                case 6:
                    return ExtraStats.Health.ToString();
                case 7:
                    return ExtraStats.HitAccuracy.ToString();
                case 8:
                    return ExtraStats.MagicAccuracy.ToString();
                case 9:
                    return ExtraStats.MagicPower.ToString();
                case 10:
                    return ExtraStats.MagicResistance.ToString();
                case 11:
                    return ExtraStats.ManaPool.ToString();
                case 12:
                    return ExtraStats.MoveSpeed.ToString();
                case 13:
                    return ExtraStats.RegenerationHealth.ToString();
                case 14:
                    return ExtraStats.RegenerationMana.ToString();
                default:
                    throw new Exception();
            }
        }
    }
}
