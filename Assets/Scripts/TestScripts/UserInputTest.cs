using System;
using Scripts;
using GolemEntity;
using UnityEngine;
using UnityEngine.UI;

public class UserInputTest : MonoBehaviour
{
    [SerializeField] private GameObject panelGolemType;
    [SerializeField] private GameObject panelGolemSpec;
    //[SerializeField] private GameObject panelGolemStats;
    
    //[SerializeField] private Text[] texts;

    private Camera _mainCamera;

    private GolemType _golemType;
    private Specialization _specialization;
    public Golem Golem { get; set; }
    private int _lvl;

    private void Start()
    {
        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G))
        {
            panelGolemType.SetActive(true);
            panelGolemSpec.SetActive(false);
            //panelGolemStats.SetActive(false);
            _lvl = 1;
        }
    
        if (Golem != null)
        {
            //FillUI();
        }
        
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            LvlUp();
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            LvlDown();
        }
    }

    

   

    public void LvlUp()
    {
        _lvl++;
        Golem?.ChangeBaseStatsProportionally(10);
    }
    public void LvlDown()
    {
        if (_lvl > 1)
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
        
        //panelGolemStats.SetActive(true);
    }

    private void CreateGolem()
    {
        GetComponent<Spawner>().SpawnGolem(_golemType, _specialization);
    }

    // private void FillUI()
    // {
    //     //Incorrect results may be displayed due to the addition of new values to extra stats.
    //     texts[0].text = $"Тип голема {_golemType.ToString()}";
    //     texts[1].text = $"Специализация {_specialization.ToString()}";
    //     texts[2].text = $"Уровень {_lvl}";
    //     texts[3].text = $"Сила {Golem.GetBaseStats().Strength}";
    //     texts[4].text = $"Ловкость {Golem.GetBaseStats().Agility}";
    //     texts[5].text = $"Интеллект {Golem.GetBaseStats().Intelligence}";
    //     texts[6].text = $"Дистанция атаки {Golem.GetExtraStats().AttackRange}";
    //     texts[7].text = $"Скорость атаки {Golem.GetExtraStats().AttackSpeed}";
    //     texts[8].text = $"Шанс уклонения {Golem.GetExtraStats().AvoidChance}";
    //     texts[9].text = $"Физический урон {Golem.GetExtraStats().DamagePerHeat}";
    //     texts[10].text = $"Защита {Golem.GetExtraStats().Defence}";
    //     texts[11].text = $"Шанс блока магии {Golem.GetExtraStats().DodgeChance}";
    //     texts[12].text = $"Здоровье {Golem.GetExtraStats().Health}";
    //     texts[13].text = $"Точность удара {Golem.GetExtraStats().HitAccuracy}";
    //     texts[14].text = $"Точность магии {Golem.GetExtraStats().MagicAccuracy}";
    //     texts[15].text = $"Магический урон {Golem.GetExtraStats().MagicDamage}";
    //     texts[16].text = $"Сопротивление магии {Golem.GetExtraStats().MagicResistance}";
    //     texts[17].text = $"Мана {Golem.GetExtraStats().ManaPool}";
    //     texts[18].text = $"Скорость передвижения {Golem.GetExtraStats().MoveSpeed}";
    //     texts[19].text = $"Регенерация здоровья {Golem.GetExtraStats().RegenerationHealth}";
    //     texts[20].text = $"Выносливость {Golem.GetExtraStats().Stamina}";
    // }

    private static Enum ToEnum(string value, Type enumType)
    {
        return (Enum)Enum.Parse(enumType, value, true);
    }
}