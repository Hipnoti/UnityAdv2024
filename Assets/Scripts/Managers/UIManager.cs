using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI hpText;

    [SerializeField] private CharacterController bobby;
    
    private void Start()
    {
        bobby.onTakeDamageEvent.AddListener(RefreshHPText);
    }

    private void RefreshHPText()
    {
        hpText.text = bobby.CurrentHP.ToString();
    }
}
