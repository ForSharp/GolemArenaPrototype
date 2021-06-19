using System;
using System.Collections;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;

public class UIHealthBar : MonoBehaviour
{
    [HideInInspector] public GameCharacterState characterState;
    [SerializeField] private GameObject fill;
    [SerializeField] private Text maxHealthText;
    [SerializeField] private Text currentHealthText;
    private Slider _slider;
    private const int TimeToDestroy = 5;
    private Camera _mainCamera;

    private void Start()
    {
        _slider = GetComponent<Slider>();
        transform.SetParent(GameObject.Find("Canvas").transform);
        if (characterState)
        {
            _slider.maxValue = characterState.MaxHealth;
            maxHealthText.text = _slider.maxValue.ToString(CultureInfo.InvariantCulture);
        }

        _mainCamera = Camera.main;
    }

    private void Update()
    {
        if (!characterState)
            return;

        UpdateMaxValue();
        UpdateSliderValue();
        SetRequiredPosition();
        DestroyOnDeath();
    }

    private void SetRequiredPosition(float multiplier = 1)
    {
        var requirePos = new Vector3(characterState.transform.position.x,
            characterState.transform.position.y + characterState.GetComponent<Collider>().bounds.size.y * multiplier,
            characterState.transform.position.z);

        var position = _mainCamera.WorldToScreenPoint(requirePos);

        transform.position = position;
    }

    private void UpdateSliderValue()
    {
        _slider.value = characterState.CurrentHealth;
        currentHealthText.text = _slider.value.ToString(CultureInfo.InvariantCulture);
    }

    private void DestroyOnDeath()
    {
        if (characterState.IsDead)
        {
            fill.SetActive(false);
            //Destroy(gameObject, TimeToDestroy);
            
            StartCoroutine(WaitForSeconds(TimeToDestroy));
            gameObject.SetActive(false);
        }
    }

    private IEnumerator WaitForSeconds(int sec)
    {
        yield return new WaitForSeconds(sec);
    }

    private void ChangeMaxValue()
    {
        //if an event occurs, in which the maximum health value changes in runtime, this method should be executed

        _slider.maxValue = characterState.MaxHealth;
    }

    private void UpdateMaxValue()
    {
        if (Math.Abs(characterState.MaxHealth - _slider.maxValue) > 1)
        {
            _slider.maxValue = characterState.MaxHealth;
            maxHealthText.text = _slider.maxValue.ToString(CultureInfo.InvariantCulture);
        }
    }
}