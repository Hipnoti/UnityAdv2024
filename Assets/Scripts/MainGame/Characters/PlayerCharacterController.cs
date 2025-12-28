using System;
using System.Collections;
using System.Collections.Generic;
using Mono.Cecil.Cil;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;

public class PlayerCharacterController : MonoBehaviour
{
    private const string MUD_LAYER_NAME = "Mud";
    
    [Header("Navigation")] 
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform waypoint; 
    [SerializeField] private Transform[] pathWaypoints;
    [SerializeField] private bool moveOnStart;
    [SerializeField] private bool hasFurBalls;
    
    private bool isMoving;
    private int currentWaypointIndex = 0;
    
    public void ToggleMoving(bool shouldMove)
    {
        isMoving = shouldMove;
        if(navMeshAgent) navMeshAgent.enabled = isMoving;
    }

    public void SetDestination(Transform targetTransformWaypoint)
    {
        if(navMeshAgent)
            navMeshAgent.SetDestination(targetTransformWaypoint.position);
    }
     
    private void Start()
    {
        if (moveOnStart && waypoint)
        {
            SetDestination(waypoint);
            ToggleMoving(true);
        }

        if (hasFurBalls)
            SetAreaCost();
    }

    private void Update()
    {
        if (isMoving && !navMeshAgent.isStopped && navMeshAgent.remainingDistance <= 0.1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= pathWaypoints.Length)
                currentWaypointIndex = 0;
            SetDestination(pathWaypoints[currentWaypointIndex]);
        }
    }

    private void SetAreaCost()
    {
        navMeshAgent.SetAreaCost(NavMesh.GetAreaFromName(MUD_LAYER_NAME), 1);
    }
  
}