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
    [Header("Navigation")] 
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform waypoint; 
    [SerializeField] private Transform[] pathWaypoints;
    
     private bool isMoving;
     private int currentWaypointIndex = 0;
     
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
     
    private void Start()
    {
        if (waypoint)
        {
            SetDestination(waypoint);
            ToggleMoving(true);
        }
    }

    // private void Update()
    // {
    //     if (isMoving && !navMeshAgent.isStopped && navMeshAgent.remainingDistance <= 0.1f)
    //     {
    //         currentWaypointIndex++;
    //         if (currentWaypointIndex >= pathWaypoints.Length)
    //             currentWaypointIndex = 0;
    //         SetDestination(pathWaypoints[currentWaypointIndex]);
    //     }
    // }
  
}
