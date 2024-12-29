using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerCharacterController : MonoBehaviour
{ 
    public event UnityAction<int> onTakeDamageEventAction;
    [SerializeField] private UnityEvent<int> onTakeDamageEvent;
    
    [Header("Navigation")] 
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform waypoint; 
    [SerializeField] private Transform[] pathWaypoints;

     private bool isMoving = true;
     private int currentWaypointIndex = 0;

     private bool hasBloodyBoots = true;
     public int Hp => hp;
     
     private int hp = 100;
    
     public void ToggleMoving(bool shouldMove)
     {
         isMoving = shouldMove;
         if(navMeshAgent) navMeshAgent.enabled = shouldMove;
     }

     public void SetDestination(Transform targetTransformWaypoint)
     {
         if(navMeshAgent)
             navMeshAgent.SetDestination(targetTransformWaypoint.position);
     }
     
     public void TakeDamage(int damageAmount)
     {
         hp -= damageAmount;
         onTakeDamageEvent.Invoke(hp);
         onTakeDamageEventAction.Invoke(hp);
     }
     
    private void Start()
    {
        SetMudAreaCost();
        ToggleMoving(true);
        if (waypoint)
        {
            SetDestination(waypoint);
        }
    }

    private void SetMudAreaCost()
    {
        if (hasBloodyBoots)
        {
            navMeshAgent.SetAreaCost(3, 1);
        }
    }

    [ContextMenu("Take Damage Test")]
    private void TakeDamageTesting()
    {
        TakeDamage(10);
    }
    

    private void Update()
    {
        // if (isMoving && !navMeshAgent.isStopped && navMeshAgent.remainingDistance <= 0.1f)
        // {
        //       Debug.Log("Reached Waypoint!");
        //       ToggleMoving(false);
        // }
        // if (isMoving && !navMeshAgent.isStopped && navMeshAgent.remainingDistance <= 0.1f)
        // {
        //     currentWaypointIndex++;
        //     if (currentWaypointIndex >= pathWaypoints.Length)
        //         currentWaypointIndex = 0;
        //     SetDestination(pathWaypoints[currentWaypointIndex]);
        // }
    }
  
}
