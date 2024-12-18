using System;
using System.Collections;
using System.Collections.Generic;
using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;


public enum MovementType { Patrol, Input}
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
    
    [Header("Input")]
    public InputActionAsset inputActionAsset;
    
    [Header("Navigation")] 
    [SerializeField] private MovementType movementType;
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform[] pathWaypoints;
    [SerializeField] private Animator agentAnimator;
    [SerializeField] private bool moveOnGameStart = false;
    [SerializeField] private Camera mainCamera;

    [Header("Health")] 
    [SerializeField] private uint maxHP;
    [SerializeField] private uint currentHP;

    [Header("Graphics")]
    [SerializeField] private ParticleSystem fallEffect;
    
    private bool hasSpecialTevaNaot = true;
    private bool isMoving;
    
    //Saving the mouse instance
  //  private Mouse currentMouse;

    private InputAction moveButtonAction;
    private InputAction positionAction;

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
      //  currentMouse = Mouse.current;
        
        InputActionMap actionMap = inputActionAsset.FindActionMap("Player");
        actionMap.Enable();
        moveButtonAction = actionMap.FindAction("Move");
        if (moveButtonAction != null)
        {
            moveButtonAction.performed += MoveButtonActionOnPerformed;
        }
        
        positionAction = inputActionAsset.FindAction("PointerPosition");
        if(hasSpecialTevaNaot)
           navMeshAgent.SetAreaCost(mudAreaID, 0.2f);
        if (movementType == MovementType.Patrol)
        {
            if (moveOnGameStart)
            {
                ToggleMoving(true);
                navMeshAgent.SetDestination(pathWaypoints[currentWaypointIndex].position);
            }
        }

        currentHP = maxHP;
    }


    private void Update()
    {
        agentAnimator.SetFloat(speedAnimatorParameter, navMeshAgent.velocity.magnitude);
        if (movementType == MovementType.Patrol)
        {
            if (isMoving && !navMeshAgent.isStopped && navMeshAgent.remainingDistance <= 0.1f)
            {
                currentWaypointIndex++;
                if (currentWaypointIndex >= pathWaypoints.Length)
                    currentWaypointIndex = 0;
                SetDestination();
            }
        }
        
        //Option 1
        // if (movementType == MovementType.Input)
        // {
        //     //same thing for keyboard
        //     //Keyboard.current
        //     //Gamepad.current
        //     //Pen.current etc. etc.
        //     if (currentMouse.leftButton.wasPressedThisFrame)
        //     {
        //         TryRayCastMove(currentMouse.position.ReadValue());
        //     }
        // }
        //
        // if (Keyboard.current.escapeKey.wasPressedThisFrame)
        // {
        //     //Activate the pause menu
        // }
        
    }

    
    //Option 3
    public void MoveButtonActionOnPerformed(InputAction.CallbackContext callbackContext)
    {
        TryRayCastMove(positionAction.ReadValue<Vector2>());
    }

    public void PauseGame(InputAction.CallbackContext callbackContext)
    {
        Debug.Log("Game paused! (Not really)");
    }

    private void TryRayCastMove(Vector3 rayCastPosition)
    {
        Debug.Log("Ray casting");
        RaycastHit hit;
        Ray ray = mainCamera.ScreenPointToRay(rayCastPosition);
        if (Physics.Raycast(ray, out hit))
        {
            navMeshAgent.SetDestination(hit.point);
        }
    }

    [ContextMenu("Take Damage")]
    private void TakeDamageTest()
    {
        TakeDamage(20);
    }
  
}
