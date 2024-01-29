using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    [SerializeField] private FireHazard[] fireHazards;

    private void Start()
    {
        foreach (FireHazard fireHazard in fireHazards)
        {
            fireHazard.onCharacterEnteredAction += HandleCharacterEnteredFire;
        }
      
    }

    public void HandleCharacterEnteredFire(FireEnteredEventArgs args)
    {
        args.targetCharacterController.TakeDamage(args.damageDealt);
    }
}
