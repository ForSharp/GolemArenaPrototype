using System;
using System.Collections;
using System.Collections.Generic;
using __Scripts;
using GolemEntity;
using UnityEngine;
using UnityEngine.UI;

public class UserInputTest : MonoBehaviour
{
    [SerializeField] private GameObject panelGolemType;
    [SerializeField] private GameObject panelGolemSpec;
    [SerializeField] private GameObject panelGolemStats;
    [SerializeField] private Text[] textes;

    private GolemType _golemType;
    private Specialization _specialization;
    private Golem _golem;
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

        if (_golem != null)
        {
            FillUI();
        }
        
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            _lvl++;
            _golem?.ChangeBaseStatsProportionally(10);
        }
        if (Input.GetKeyDown(KeyCode.DownArrow) && _lvl > 1)
        {
            _lvl--;
            _golem?.ChangeBaseStatsProportionally(-10);
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

    private void ShowResult()
    {
        Debug.Log(_golemType.ToString());
        Debug.Log(_specialization.ToString());
    }

    private void CreateGolem()
    {
        _golem = new Golem(_golemType, _specialization);
    }

    private void FillUI()
    {
        textes[0].text = $"Тип голема {_golemType.ToString()}";
        textes[1].text = $"Специализация {_specialization.ToString()}";
        textes[2].text = $"Уровень {_lvl}";
        textes[3].text = $"Сила {_golem.GetBaseStats().Strength}";
        textes[4].text = $"Ловкость {_golem.GetBaseStats().Agility}";
        textes[5].text = $"Интеллект {_golem.GetBaseStats().Intelligence}";
        textes[6].text = $"Дистанция атаки {_golem.GetExtraStats().AttackRange}";
        textes[7].text = $"Скорость атаки {_golem.GetExtraStats().AttackSpeed}";
        textes[8].text = $"Шанс уклонения {_golem.GetExtraStats().AvoidChance}";
        textes[9].text = $"Физический урон {_golem.GetExtraStats().DamagePerHeat}";
        textes[10].text = $"Защита {_golem.GetExtraStats().Defence}";
        textes[11].text = $"Шанс блока магии {_golem.GetExtraStats().DodgeChance}";
        textes[12].text = $"Здоровье {_golem.GetExtraStats().Health}";
        textes[13].text = $"Точность удара {_golem.GetExtraStats().HitAccuracy}";
        textes[14].text = $"Точность магии {_golem.GetExtraStats().MagicAccuracy}";
        textes[15].text = $"Магический урон {_golem.GetExtraStats().MagicDamage}";
        textes[16].text = $"Сопротивление магии {_golem.GetExtraStats().MagicResistance}";
        textes[17].text = $"Мана {_golem.GetExtraStats().ManaPool}";
        textes[18].text = $"Скорость передвижения {_golem.GetExtraStats().MoveSpeed}";
        textes[19].text = $"Регенерация здоровья {_golem.GetExtraStats().RegenerationRate}";
        textes[20].text = $"Выносливость {_golem.GetExtraStats().Stamina}";
    }
    
    private Enum ToEnum(string value, Type enumType)
    {
        return (Enum)Enum.Parse(enumType, value, true);
    }
}