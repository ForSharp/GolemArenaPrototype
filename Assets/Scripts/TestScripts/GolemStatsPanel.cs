using System.Globalization;
using GolemEntity.ExtraStats;
using UnityEngine;
using UnityEngine.UI;

public class GolemStatsPanel : MonoBehaviour
{
    [SerializeField] private GameObject panel;
    [SerializeField] private Text[] mainInfo;
    [SerializeField] private Text[] extraStatsUI;
    private GameCharacterState _state;
    private GolemExtraStats _stats;
    private bool _needUpd;

    private void Start()
    {
        EventContainer.GolemStatsChanged += UpdateStatsValues;
    }

    private void Update()
    {
        HandleMouseClick();
        
        SetLvl();

        if (_needUpd)
        {
            UpdateStatsNow();
        }
    }

    private void HandleMouseClick()
    {
        if (Input.GetMouseButtonDown(0))
        {
            var ray = Camera.main.ScreenPointToRay(new Vector2(Input.mousePosition.x, Input.mousePosition.y));
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                var coll = hit.collider;
                if (TryGetNewState(coll))
                {
                    _stats = _state.Stats;
                    FillMainInfo();
                    FillTexts();
                    panel.SetActive(true); 
                }
                else
                {
                    panel.SetActive(false); 
                }
            }
        }
    }

    private void UpdateStatsNow()
    {
        _stats = _state.Stats;
        FillMainInfo();
        FillTexts();
        _needUpd = false;
    }
    
    private void UpdateStatsValues()
    {
        // _stats = _state.Stats;
        // FillMainInfo();
        // FillTexts();
        _needUpd = true;
    }

    private void SetLvl()
    {
        if (_state)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                _state.LvlUp();
                
            }

            if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                _state.LvlDown();
            }
        }
    }

    private void SetPanelPosition()
    {
    }

    private void FillMainInfo()
    {
        mainInfo[0].text = _state.Type;
        mainInfo[1].text = _state.Spec;
        mainInfo[2].text = _state.Lvl.ToString();
        mainInfo[3].text = _state.BaseStats.Strength.ToString(CultureInfo.InvariantCulture);
        mainInfo[4].text = _state.BaseStats.Agility.ToString(CultureInfo.InvariantCulture);
        mainInfo[5].text = _state.BaseStats.Intelligence.ToString(CultureInfo.InvariantCulture);
    }

    private void FillTexts()
    {
        var currentStats = GetExtraStatsArray();

        for (var i = 0; i < extraStatsUI.Length; i++)
        {
            extraStatsUI[i].text = currentStats[i].ToString(CultureInfo.InvariantCulture);
        }
    }

    private float[] GetExtraStatsArray()
    {
        if (_state)
        {
            float[] extraStats =
            {
                _stats.AttackRange, _stats.AttackSpeed, _stats.AvoidChance, _stats.DamagePerHeat, _stats.Defence,
                _stats.DodgeChance, _stats.Health, _stats.HitAccuracy,
                _stats.MagicAccuracy, _stats.MagicDamage, _stats.MagicResistance, _stats.ManaPool, _stats.MoveSpeed,
                _stats.RegenerationHealth, _stats.RegenerationMana,
                _stats.RegenerationStamina, _stats.Stamina
            };
            return extraStats;
        }

        return default;
    }

    private bool TryGetNewState(Collider coll)
    {
        if (coll.GetComponent<GameCharacterState>())
        {
            _state = coll.GetComponent<GameCharacterState>();
            return true;
        }

        if (coll.GetComponentInParent<GameCharacterState>())
        {
            _state = coll.GetComponentInParent<GameCharacterState>();
            return true;
        }

        if (coll.GetComponentInChildren<GameCharacterState>())
        {
            _state = coll.GetComponentInChildren<GameCharacterState>();
            return true;
        }

        return false;
    }
}