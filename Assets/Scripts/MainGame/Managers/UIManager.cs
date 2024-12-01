using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI hpText;

    [SerializeField] private PlayerCharacterController bobby;
    
    // public void RefreshHPText()
    // {
    //     hpText.text = bobby.CurrentHP.ToString();
    // }
    
    // private void Start()
    // {
    //     bobby.onTakeDamageEvent.AddListener(RefreshHPText);
    // }

    
}
