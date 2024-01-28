using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FireHazard : MonoBehaviour
{
    public int Damage => damage;
    
    public UnityEvent onPlayerEntered;
    [SerializeField] private int damage = 10;
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(PlayerCharacterController.CHARACTER_TAG))
        {
            Debug.Log("Player entered this hazard");
            onPlayerEntered.Invoke();
        }
    }
}
