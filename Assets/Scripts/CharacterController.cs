using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class CharacterController : MonoBehaviour
{
    private const int mudAreaID = 3;
    
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform[] pathWaypoints;

    private int currentWaypointIndex = 0;
    private void Start()
    {
        navMeshAgent.SetAreaCost(mudAreaID, 0.2f);
        navMeshAgent.SetDestination(pathWaypoints[currentWaypointIndex].position);
    }

    private void Update()
    {
        if (!navMeshAgent.isStopped && navMeshAgent.remainingDistance <= 0.1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= pathWaypoints.Length)
                currentWaypointIndex = 0;
            navMeshAgent.SetDestination(pathWaypoints[currentWaypointIndex].position);
        }
    }
}
