using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class FireHazard : MonoBehaviour
{
    public UnityEvent onPlayerEntered;
    [SerializeField] private float damage = 10f;
    
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag(CharacterController.CHARACTER_TAG))
        {
            Debug.Log("Player entered this hazard");
            onPlayerEntered.Invoke();
        }
    }
}
