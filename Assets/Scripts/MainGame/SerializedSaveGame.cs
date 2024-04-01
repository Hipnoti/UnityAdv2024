using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SerializedSaveGame
{
    public float gameVersion;
    // public Vector3 playerPosition;
    // public Vector3 playerRotation;
    public uint playerHP;
    public int currentWaypointIndex;

    public float playerPositionX, playerPositionY, playerPositionZ;
    public float playerRotationX, playerRotationY, playerRotationZ;
    
    
}
