using System;
using System.Collections;
using System.Collections.Generic;
using __Scripts;
using GolemEntity;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

public class UserInputTest : MonoBehaviour
{
    [SerializeField] private GameObject panelGolemType;
    [SerializeField] private GameObject panelGolemSpec;
    [SerializeField] private GameObject panelGolemStats;
    [SerializeField] private GameObject GolemPrefab;
    [SerializeField] private Vector3 spawnPoint;
    [SerializeField] private Text[] textes;

    private GolemType _golemType;
    private Specialization _specialization;
    public Golem Golem { get; private set; }
    private int _lvl = 1;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            panelGolemType.SetActive(true);
            panelGolemSpec.SetActive(false);
            panelGolemStats.SetActive(false);
            _lvl = 1;
        }

        if (Golem != null)
        {
            FillUI();
        }
        
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _lvl++;
            Golem?.ChangeBaseStatsProportionally(10);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && _lvl > 1)
        {
            _lvl--;
            Golem?.ChangeBaseStatsProportionally(-10);
        }
    }

    public void SetGolemType(Text text)
    {
        panelGolemType.SetActive(false);
        
        _golemType = (GolemType) ToEnum(text.text, typeof(GolemType));
        
        panelGolemSpec.SetActive(true);
    }

    public void SetGolemSpec(Text text)
    {
        _specialization = (Specialization) ToEnum(text.text, typeof(Specialization));

        panelGolemSpec.SetActive(false);
        
        CreateGolem();
        
        panelGolemStats.SetActive(true);
    }

    private void CreateGolem()
    {
        Golem = new Golem(_golemType, _specialization);
        GameObject newGolem = Instantiate(GolemPrefab, spawnPoint, Quaternion.identity);
        newGolem.GetComponent<GameCharacterState>().golem = Golem;
    }

    private void FillUI()
    {
        textes[0].text = $"Тип голема {_golemType.ToString()}";
        textes[1].text = $"Специализация {_specialization.ToString()}";
        textes[2].text = $"Уровень {_lvl}";
        textes[3].text = $"Сила {Golem.GetBaseStats().Strength}";
        textes[4].text = $"Ловкость {Golem.GetBaseStats().Agility}";
        textes[5].text = $"Интеллект {Golem.GetBaseStats().Intelligence}";
        textes[6].text = $"Дистанция атаки {Golem.GetExtraStats().AttackRange}";
        textes[7].text = $"Скорость атаки {Golem.GetExtraStats().AttackSpeed}";
        textes[8].text = $"Шанс уклонения {Golem.GetExtraStats().AvoidChance}";
        textes[9].text = $"Физический урон {Golem.GetExtraStats().DamagePerHeat}";
        textes[10].text = $"Защита {Golem.GetExtraStats().Defence}";
        textes[11].text = $"Шанс блока магии {Golem.GetExtraStats().DodgeChance}";
        textes[12].text = $"Здоровье {Golem.GetExtraStats().Health}";
        textes[13].text = $"Точность удара {Golem.GetExtraStats().HitAccuracy}";
        textes[14].text = $"Точность магии {Golem.GetExtraStats().MagicAccuracy}";
        textes[15].text = $"Магический урон {Golem.GetExtraStats().MagicDamage}";
        textes[16].text = $"Сопротивление магии {Golem.GetExtraStats().MagicResistance}";
        textes[17].text = $"Мана {Golem.GetExtraStats().ManaPool}";
        textes[18].text = $"Скорость передвижения {Golem.GetExtraStats().MoveSpeed}";
        textes[19].text = $"Регенерация здоровья {Golem.GetExtraStats().RegenerationRate}";
        textes[20].text = $"Выносливость {Golem.GetExtraStats().Stamina}";
    }

    private static Enum ToEnum(string value, Type enumType)
    {
        return (Enum)Enum.Parse(enumType, value, true);
    }
}