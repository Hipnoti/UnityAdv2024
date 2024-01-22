using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class CharacterController : MonoBehaviour
{
    private const string speedAnimatorParameter = "Speed";
    private const int hurtLayerIndex = 1;
    private const int mudAreaID = 3;
    
    
    [Header("Navigation")]
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform[] pathWaypoints;
    [SerializeField] private Animator agentAnimator;
    [SerializeField] private bool moveOnGameStart = false;

    [Header("Health")] 
    [SerializeField] private int maxHP;
    [SerializeField] private int currentHP;
    
    private bool hasSpecialTevaNaot = true;
    private int currentWaypointIndex = 0;
    private void Start()
    {
        if(hasSpecialTevaNaot)
           navMeshAgent.SetAreaCost(mudAreaID, 0.2f);
        if(moveOnGameStart)
            navMeshAgent.SetDestination(pathWaypoints[currentWaypointIndex].position);
        
        currentHP = maxHP;
    }

    private void Update()
    {
        agentAnimator.SetFloat(speedAnimatorParameter, navMeshAgent.velocity.magnitude);
        if (moveOnGameStart && !navMeshAgent.isStopped && navMeshAgent.remainingDistance <= 0.1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= pathWaypoints.Length)
                currentWaypointIndex = 0;
            navMeshAgent.SetDestination(pathWaypoints[currentWaypointIndex].position);
        }

        float healthPercent = 1 - (float)currentHP / maxHP;
        agentAnimator.SetLayerWeight(hurtLayerIndex,healthPercent);
    }
}
