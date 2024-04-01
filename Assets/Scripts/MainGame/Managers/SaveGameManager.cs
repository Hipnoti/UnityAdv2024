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
        serializedSaveGame.playerPosition = gameManager.playerCharacterController.transform.position;
      // serializedSaveGame.playerPositionX = gameManager.playerCharacterController.transform.position.x;
      // serializedSaveGame.playerPositionY = gameManager.playerCharacterController.transform.position.y;
      // serializedSaveGame.playerPositionZ = gameManager.playerCharacterController.transform.position.z;
        serializedSaveGame.playerHP = gameManager.playerCharacterController.CurrentHP;
        serializedSaveGame.currentWaypointIndex = gameManager.playerCharacterController.currentWaypointIndex;

         SaveToJson();
             // SaveToBinary();
    }

    [ContextMenu("Load!")]
    public void LoadGame()
    {
        LoadFromJson();

      //  LoadFromBinary();
        
        gameManager.playerCharacterController.transform.position = serializedSaveGame.playerPosition;
       // gameManager.playerCharacterController.transform.position = new Vector3(serializedSaveGame.playerPositionX,
       //     serializedSaveGame.playerPositionY, serializedSaveGame.playerPositionZ);
        gameManager.playerCharacterController.CurrentHP = serializedSaveGame.playerHP;
        gameManager.playerCharacterController.currentWaypointIndex = serializedSaveGame.currentWaypointIndex;

        gameManager.playerCharacterController.SetDestination();

        uiManager.RefreshHPText();
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
    }

    private void LoadFromBinary()
    {
        FileStream fileStream = new FileStream(Application.persistentDataPath + SAVE_FILE_NAME, FileMode.Open);

        BinaryFormatter converter = new BinaryFormatter();
        serializedSaveGame = converter.Deserialize(fileStream) as SerializedSaveGame;

        fileStream.Close();
    }
}
