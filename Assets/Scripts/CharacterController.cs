using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CharacterController : MonoBehaviour
{
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform targetDestination;

    private void Start()
    {
        navMeshAgent.SetDestination(targetDestination.position);


    }

    private void Update()
    {
        if (!navMeshAgent.isStopped && navMeshAgent.remainingDistance <= 0.5f)
        {
            Debug.Log("Reached Destination!");
        }
    }
}
