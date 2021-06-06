using System;
using System.Collections;
using System.Collections.Generic;
using __Scripts;
using UnityEngine;
using UnityEngine.UI;

public class UserInputTest : MonoBehaviour
{
    [SerializeField] private GameObject panelGolemType;
    [SerializeField] private GameObject panelGolemSpec;

    private GolemType _golemType;
    private Specialization _specialization;

    private void Start()
    {
        
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.G) && !panelGolemSpec.activeSelf)
        {
            panelGolemType.SetActive(true);
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
        
        ShowResult();
        
        panelGolemSpec.SetActive(false);
    }

    private void ShowResult()
    {
        Debug.Log(_golemType.ToString());
        Debug.Log(_specialization.ToString());
    }

    
    
    private Enum ToEnum(string value, Type enumType)
    {
        return (Enum)Enum.Parse(enumType, value, true);
    }
}