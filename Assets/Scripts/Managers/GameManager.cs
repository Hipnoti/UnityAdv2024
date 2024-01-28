using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [SerializeField] private PlayerCharacterController playerCharacterController;
    [SerializeField] private FireHazard fireHazard;

    private void Start()
    {
        fireHazard.onPlayerEntered.AddListener(HandleCharacterEnteredFire);
    }

    public void HandleCharacterEnteredFire()
    {
        playerCharacterController.TakeDamage(fireHazard.Damage);
    }
}
