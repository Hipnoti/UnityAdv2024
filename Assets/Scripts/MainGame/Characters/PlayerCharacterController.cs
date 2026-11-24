using Unity.AI.Navigation;
using UnityEngine;
using UnityEngine.AI;

public class PlayerCharacterController : MonoBehaviour
{
    private const string MUD_LAYER_NAME = "Mud";
    private const float CLOSE_DISTANCE_THRESHOLD = 1.5f;
    
    [Header("Navigation")] 
    [SerializeField] private NavMeshAgent navMeshAgent;
    [SerializeField] private Transform waypoint; 
    [SerializeField] private Transform[] pathWaypoints;
    [SerializeField] private bool moveOnStart;
    [SerializeField] private bool hasBoots;
    
    private bool isMoving;
    private int currentWaypointIndex = 0;
    private int mudAreaName;
  

    public void ToggleMoving(bool shouldMove)
    {
        isMoving = shouldMove;
        if(navMeshAgent) navMeshAgent.enabled = isMoving;
    }
    //
    public void SetDestination(Transform targetTransformWaypoint)
    {
        if(navMeshAgent)
            navMeshAgent.SetDestination(targetTransformWaypoint.position);
    }
    //  
    private void Start()
    {
        mudAreaName = NavMesh.GetAreaFromName(MUD_LAYER_NAME);
        if (moveOnStart && waypoint)
        {
         //   SetDestination(waypoint);
            ToggleMoving(true);
        }

     //   SetAreaCost();
        if (hasBoots)
            SetAreaCost();
    }

    private void Update()
    {
        if (isMoving && !navMeshAgent.isStopped && navMeshAgent.remainingDistance <= CLOSE_DISTANCE_THRESHOLD)
        {
            currentWaypointIndex++;
            if (currentWaypointIndex >= pathWaypoints.Length)
                currentWaypointIndex = 0;
            SetDestination(pathWaypoints[currentWaypointIndex]);
        }
    }
    //
    private void SetAreaCost()
    {
        navMeshAgent.SetAreaCost(mudAreaName, 1);
    }
  
}