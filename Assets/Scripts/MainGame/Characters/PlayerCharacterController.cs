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
    private const int HurtLayerIndex = 1;
    
    public event UnityAction<int> onTakeDamageEventAction;
    
    [SerializeField] private UnityEvent<int> onTakeDamageEvent;
    
    [Header("Input")]
    [SerializeField] private InputActionAsset inputActionAsset;
    
    [Header("Navigation")] 
    [SerializeField] private NavMeshAgent navMeshAgent;

    [SerializeField] private Transform waypoint;
    [SerializeField] private Transform[] pathWaypoints;
    
    [SerializeField] Animator animator;
    [SerializeField] private ParticleSystem fallEffect;
    
    private InputActionMap inputActionMap;

    private InputSystem_Actions actions;
    
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

    private int hp;
    private int startingHp;

    public void PlaySlipEffect()
    {
        fallEffect.Play();
    }

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
        float hpPercentLeft = (float) hp / startingHp;
   //     animator.SetLayerWeight(1, (1 - hpPercentLeft));
        onTakeDamageEvent.Invoke(hp);
        onTakeDamageEventAction.Invoke(hp);
    }

    private void Start()
    {
        BasePartialClass partialClass = new BasePartialClass();

        hp = startingHp;
        SetMudAreaCost();
        ToggleMoving(true);
        SetDestination(pathWaypoints[0]);
        InitializeInputActions();
        // if (waypoint)
        // {
        //     SetDestination(waypoint);
        // }
    }

    private void SetMudAreaCost()
    {
        if (hasBloodyBoots)
        {
            navMeshAgent.SetAreaCost(3, 1);
        }
    }

    [ContextMenu("Take Damage Test")]
    private void TakeDamageTesting()
    {
        TakeDamage(10);
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

        if (animator)
        { 
            animator.SetFloat(SpeedAnimatorHash, navMeshAgent.velocity.magnitude);
        //    animator.SetLayerWeight(HurtLayerIndex, (float)hp/startingHp);  
        }
    }

    private void PlayFootStepSound()
    {
        Debug.Log("Play footstep sound");
    }

    #region Input

    private void OnEnable()
    {
        actions = new InputSystem_Actions();
        actions.Player.Enable();
    }

    private void InitializeInputActions()
    {
        actions.Player.MoveTo.performed += MoveToAction;
    }
    

    private void OnDisable()
    {
        actions.Player.Disable();
    }

    #endregion

    // public void OnAttack(InputAction.CallbackContext context)
    // {
    //     Debug.Log(context.phase);
    //     if(context.performed)
    //         Debug.Log("Should do attack");
    // }
    //
    public void MoveToAction(InputAction.CallbackContext context)
    {
       Debug.Log("Move to action");
    }
    //
    // public void MoveActionVector(InputAction.CallbackContext context)
    // {
    //     Debug.Log("Move action vector " + context.ReadValue<Vector2>());
    // }

}