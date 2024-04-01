using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public PlayerCharacterController playerCharacterController;
    [SerializeField] private FireHazardScriptableObject[] fireHazardScriptableObjects;
    [SerializeField] private FireHazard[] fireHazards;

    private void Start()
    {
        
        foreach (FireHazard fireHazard in fireHazards)
        {
            fireHazard.onCharacterEnteredAction += HandleCharacterEnteredFire;
            fireHazard.SetScriptableData(
                fireHazardScriptableObjects[Random.Range(0, fireHazardScriptableObjects.Length)]);
        }
      
    }

    public void HandleCharacterEnteredFire(FireEnteredEventArgs args)
    {
        args.targetCharacterController.TakeDamage(args.damageDealt);
    }
}
