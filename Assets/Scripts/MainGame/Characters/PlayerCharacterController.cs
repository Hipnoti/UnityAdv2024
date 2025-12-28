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
    private static readonly int SpeedAnimatorHash = Animator.StringToHash("Speed");
    public event UnityAction<int> onTakeDamageEventAction;
    
    [SerializeField] private UnityEvent<int> onTakeDamageEvent;
    
    [Header("Navigation")] 
    [SerializeField] private NavMeshAgent navMeshAgent;

    [SerializeField] private Transform waypoint;
    [SerializeField] private Transform[] pathWaypoints;
    
    
    public int Hp
    {
        get => hp;
        set => hp = value;
    }

    public int CurrentWaypointIndex
    {
        get => currentWaypointIndex;
        set => currentWaypointIndex = value;
    }

    private bool isMoving = true;
    private int currentWaypointIndex = 0;

    private bool hasBloodyBoots = true;

    private int hp = 100;
    private int startingHp;

    public void ToggleMoving(bool shouldMove)
    {
        isMoving = shouldMove;
        if (navMeshAgent) navMeshAgent.enabled = shouldMove;
    }

    public void SetDestination(Transform targetTransformWaypoint)
    {
        if (navMeshAgent)
            navMeshAgent.SetDestination(targetTransformWaypoint.position);
    }

    public void SetDestination(int waypointIndex)
    {
        SetDestination(pathWaypoints[waypointIndex]);
    }

    public void TakeDamage(int damageAmount)
    {
        hp -= damageAmount;
        onTakeDamageEvent.Invoke(hp);
        onTakeDamageEventAction.Invoke(hp);
    }

    private void Start()
    {
        ToggleMoving(true);
        SetDestination(waypoint);
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
    

}