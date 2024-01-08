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
}
