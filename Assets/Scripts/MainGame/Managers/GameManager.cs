using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public PlayerCharacterController playerCharacterController;
    [SerializeField] private FireHazardScriptableObject[] fireHazardScriptableObjects;
    [SerializeField] private FireHazard[] fireHazards;

    private void Start()
    {
        Instance = this;
        //C# actions
        foreach (FireHazard fireHazard in fireHazards)
        {
            // fireHazard.fireHazardData = 
            //     fireHazardScriptableObjects[Random.Range(0, fireHazardScriptableObjects.Length)];
             fireHazard.onCharacterEnteredAction += HandleCharacterEnteredFire;
         }
        //Unity Actions
        foreach (FireHazard fireHazard in fireHazards)
        {
            // fireHazard.fireHazardData = 
            //     fireHazardScriptableObjects[Random.Range(0, fireHazardScriptableObjects.Length)];
         //   fireHazard.onCharacterEntered.AddListener(HandleCharacterEnteredFire);
        }
      
    }
    

    // public void HandleCharacterEnteredFire(int damageDealt)
    // {
    //     //Formula for changing the damage dealt
    //     playerCharacterController.TakeDamage(damageDealt);
    // }

    public void HandleCharacterEnteredFire(FireEnteredEventArgs args)
    {
        args.targetCharacterController.TakeDamage(args.damageDealt);
    }
    
}
