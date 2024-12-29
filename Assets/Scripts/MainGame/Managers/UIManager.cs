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

    private void Awake()
    {
        bobby.onTakeDamageEventAction += RefreshHPText;
    }

    private void Start()
    {
        hpText.text = bobby.Hp.ToString();
    }

    // private void RemoveHeart()
    // {
    //     heartIcon.SetActive(false);
    // }

    
}
