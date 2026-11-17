using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.Serialization;

[System.Serializable]
public class SerializedSaveGame
{
    public float gameVersion;
    // public Vector3 playerPosition;
    // public Vector3 playerRotation;
    //public string aString = "sdada";
    public int playerHPNew;
    public int currentWaypointIndex;

    public float playerPositionX, playerPositionY, playerPositionZ;
    public float playerRotationX, playerRotationY, playerRotationZ;
    
    public void Serialize(BinaryWriter writer)
    {
        writer.Write(gameVersion);
        writer.Write(playerHPNew);
        writer.Write(currentWaypointIndex);
        
        writer.Write(playerPositionX);
        writer.Write(playerPositionY);
        writer.Write(playerPositionZ);
        
        writer.Write(playerRotationX);
        writer.Write(playerRotationY);
        writer.Write(playerRotationZ);
    }

    public void Deserialize(BinaryReader reader)
    {
        gameVersion = reader.ReadSingle();
        playerHPNew = reader.ReadInt32();
        currentWaypointIndex = reader.ReadInt32();
        
        playerPositionX = reader.ReadSingle();
        playerPositionY = reader.ReadSingle();
        playerPositionZ = reader.ReadSingle();
        
        playerRotationX = reader.ReadSingle();
        playerRotationY = reader.ReadSingle();
        playerRotationZ = reader.ReadSingle();
    }
}
