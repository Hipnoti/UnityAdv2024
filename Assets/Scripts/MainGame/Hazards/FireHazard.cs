using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Serialization;

public class FireHazard : MonoBehaviour
{
    public uint Damage => fireHazardData.GetRandomFireDamage();

    public event UnityAction<FireEnteredEventArgs> onCharacterEnteredAction;
    
    [SerializeField] private FireHazardScriptableObject fireHazardData;

    [SerializeField]
    private UnityEvent<FireEnteredEventArgs> onCharacterEntered = new UnityEvent<FireEnteredEventArgs>();

    public void SetScriptableData(FireHazardScriptableObject fireHazardScriptableObject)
    {
        fireHazardData = fireHazardScriptableObject;
    }
    private void Start()
    { 
        if(onCharacterEnteredAction != null)
           onCharacterEntered.AddListener(onCharacterEnteredAction);
    }

    private void OnTriggerEnter(Collider other)
    {
        // if (other.gameObject.CompareTag(PlayerCharacterController.CHARACTER_TAG))
        // {
        //     Debug.Log("Player entered this hazard");
        //     onCharacterEntered?.Invoke(new FireEnteredEventArgs
        //     {
        //         damageDealt = Damage, 
        //         targetCharacterController = other.GetComponent<PlayerCharacterController>()
        //     });
        // }
    }
}

public struct FireEnteredEventArgs
{
    public uint damageDealt;
    public PlayerCharacterController targetCharacterController;
}
