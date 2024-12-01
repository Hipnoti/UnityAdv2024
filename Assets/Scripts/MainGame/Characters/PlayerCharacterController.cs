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

     private bool isMoving = true;
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
        ToggleMoving(true);
        if (waypoint)
        {
            SetDestination(waypoint);
        }
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
