using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class FireHazard : MonoBehaviour
{
    public uint Damage => damage;

    public event UnityAction<FireEnteredEventArgs> onCharacterEnteredAction;
    [SerializeField] private UnityEvent<FireEnteredEventArgs> onCharacterEntered;
    [SerializeField] private uint damage = 10;

    private void Start()
    {
        onCharacterEntered.AddListener(onCharacterEnteredAction);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(PlayerCharacterController.CHARACTER_TAG))
        {
            Debug.Log("Player entered this hazard");
            onCharacterEntered.Invoke(new FireEnteredEventArgs
            {
                damageDealt = damage, 
                targetCharacterController = other.GetComponent<PlayerCharacterController>()
            });
        }
    }
}

public struct FireEnteredEventArgs
{
    public uint damageDealt;
    public PlayerCharacterController targetCharacterController;
}
