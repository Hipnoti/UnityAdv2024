using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;

public class PlayerCharacterController : MonoBehaviour
{
    public const string CHARACTER_TAG = "PlayerCharacter";
    private readonly int speedAnimatorParameter = Animator.StringToHash("Speed");
    private readonly int fallAnimatorTrigger = Animator.StringToHash("Fall");
    private const int hurtLayerIndex = 1;
    private const int mudAreaID = 3;

    public UnityEvent onTakeDamageEvent;

    public uint CurrentHP
    {
        get { return currentHP; }
        set { currentHP = value; }
    }
    
    public int currentWaypointIndex = 0;
    
    [Header("Navigation")]
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform[] pathWaypoints;
    [SerializeField] private Animator agentAnimator;
    [SerializeField] private bool moveOnGameStart = false;

    [Header("Health")] 
    [SerializeField] private uint maxHP;
    [SerializeField] private uint currentHP;

    [Header("Graphics")]
    [SerializeField] private ParticleSystem fallEffect;
    
    private bool hasSpecialTevaNaot = true;
    private bool isMoving;

    public void ShowFallEffect()
    {
        if(fallEffect)
            fallEffect.Play();
    }

    [ContextMenu("Make Character Fall")]
    public void MakeCharacterFall()
    {
        agentAnimator.SetTrigger(fallAnimatorTrigger);
        ToggleMoving(false);
    }
    
    public void ToggleMoving(bool shouldMove)
    {
        isMoving = shouldMove;
        navMeshAgent.enabled = shouldMove;
    }

    public void TakeDamage(uint damageAmount)
    {
        currentHP -= damageAmount;
        float healthPercent = 1 - (float)currentHP / maxHP;
        agentAnimator.SetLayerWeight(hurtLayerIndex,healthPercent);
        onTakeDamageEvent.Invoke();
    }

    public void SetDestination()
    {
        navMeshAgent.SetDestination(pathWaypoints[currentWaypointIndex].position);
    }
    
    private void Start()
    { 
        if(hasSpecialTevaNaot)
           navMeshAgent.SetAreaCost(mudAreaID, 0.2f);
        if (moveOnGameStart)
        {
            ToggleMoving(true);
            navMeshAgent.SetDestination(pathWaypoints[currentWaypointIndex].position);
        }

        currentHP = maxHP;
    }

    private void Update()
    {
        agentAnimator.SetFloat(speedAnimatorParameter, navMeshAgent.velocity.magnitude);
        if (isMoving && !navMeshAgent.isStopped && navMeshAgent.remainingDistance <= 0.1f)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= pathWaypoints.Length)
                currentWaypointIndex = 0;
         SetDestination();
        }
    }

    
    [ContextMenu("Take Damage")]
    private void TakeDamageTest()
    {
        TakeDamage(20);
    }
  
}
