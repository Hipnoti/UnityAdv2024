using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using UnityEngine;

public class SaveGameManager : MonoBehaviour
{
    private const string SAVE_FILE_NAME = "/Save.dat";
    [SerializeField] private GameManager gameManager;
    [SerializeField] private UIManager uiManager;

    private SerializedSaveGame serializedSaveGame;

    [ContextMenu("Save!")]
    public void SaveGame()
    {
        serializedSaveGame = new SerializedSaveGame();
        // serializedSaveGame.playerPosition = gameManager.playerCharacterController.transform.position;
        // serializedSaveGame.playerRotation = gameManager.playerCharacterController.transform.eulerAngles;
      serializedSaveGame.playerPositionX = gameManager.playerCharacterController.transform.position.x;
      serializedSaveGame.playerPositionY = gameManager.playerCharacterController.transform.position.y;
      serializedSaveGame.playerPositionZ = gameManager.playerCharacterController.transform.position.z;
      
      serializedSaveGame.playerRotationX = gameManager.playerCharacterController.transform.eulerAngles.x;
      serializedSaveGame.playerRotationY = gameManager.playerCharacterController.transform.eulerAngles.y;
      serializedSaveGame.playerRotationZ = gameManager.playerCharacterController.transform.eulerAngles.z;
      
        serializedSaveGame.playerHP = gameManager.playerCharacterController.CurrentHP;
        serializedSaveGame.currentWaypointIndex = gameManager.playerCharacterController.currentWaypointIndex;

         //SaveToJson();
         SaveToBinary();
    }

    [ContextMenu("Load!")]
    public void LoadGame()
    {
        //LoadFromJson();

        LoadFromBinary();
        
        // gameManager.playerCharacterController.transform.position = serializedSaveGame.playerPosition;
        // gameManager.playerCharacterController.transform.eulerAngles = serializedSaveGame.playerRotation;
       gameManager.playerCharacterController.transform.position = new Vector3(serializedSaveGame.playerPositionX,
           serializedSaveGame.playerPositionY, serializedSaveGame.playerPositionZ);
       gameManager.playerCharacterController.transform.eulerAngles = new Vector3(serializedSaveGame.playerRotationX,
           serializedSaveGame.playerRotationY, serializedSaveGame.playerRotationZ);
        gameManager.playerCharacterController.CurrentHP = serializedSaveGame.playerHP;
        uiManager.RefreshHPText();
        gameManager.playerCharacterController.currentWaypointIndex = serializedSaveGame.currentWaypointIndex;

        gameManager.playerCharacterController.SetDestination();

       
    }

    private void SaveToJson()
    {
        string jsonString = JsonUtility.ToJson(serializedSaveGame, true);

        File.WriteAllText(Application.persistentDataPath + SAVE_FILE_NAME, jsonString);
    }

    private void LoadFromJson()
    {
        string jsonString = File.ReadAllText(Application.persistentDataPath + SAVE_FILE_NAME);
        serializedSaveGame = JsonUtility.FromJson<SerializedSaveGame>(jsonString);
    }

    private void SaveToBinary()
    {
        FileStream fileStream = new FileStream(Application.persistentDataPath + SAVE_FILE_NAME, FileMode.Create);
        BinaryFormatter converter = new BinaryFormatter();
        converter.Serialize(fileStream, serializedSaveGame);
        fileStream.Close();
    }

    private void LoadFromBinary()
    {
        if (File.Exists(Application.persistentDataPath + SAVE_FILE_NAME))
        {
            FileStream fileStream = new FileStream(Application.persistentDataPath + SAVE_FILE_NAME, FileMode.Open);

            BinaryFormatter converter = new BinaryFormatter();
            serializedSaveGame = converter.Deserialize(fileStream) as SerializedSaveGame;

            fileStream.Close();
        }
    }
}
