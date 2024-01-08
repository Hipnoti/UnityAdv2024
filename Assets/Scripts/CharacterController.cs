using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform[] pathWaypoints;

    private int currentWaypointIndex = 0;
    private void Start()
    {
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
