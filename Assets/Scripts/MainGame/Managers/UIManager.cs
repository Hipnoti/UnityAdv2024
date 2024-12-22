using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI hpText;

    [SerializeField] private PlayerCharacterController bobby;
    [SerializeField] private GameObject heartIcon;
    
    public void RefreshHPText(int newHP)
    {
        hpText.text = newHP.ToString();
    }
    
    private void Start()
    {
        hpText.text = bobby.Hp.ToString();
        bobby.onTakeDamageEvent.AddListener(RefreshHPText);
    }

    // private void RemoveHeart()
    // {
    //     heartIcon.SetActive(false);
    // }

    
}
