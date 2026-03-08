using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private AssetReference sectorAsset;
    [SerializeField] private Transform targetTransform;

    private GameObject loadedSector;
    private GameObject spawnedSector;
    private AsyncOperationHandle<GameObject> loadedSectorHandle;
    
    [ContextMenu("Load and Generate Sector")]
    public void LoadAndGenerateSector()
    {
        AsyncOperationHandle<GameObject> asyncOperation = Addressables.InstantiateAsync(sectorAsset, targetTransform.position, Quaternion.identity);
        asyncOperation.Completed +=  AsyncOperationOnCompleted;
    }
    
    private void AsyncOperationOnCompleted(AsyncOperationHandle<GameObject> obj)
    {
       Debug.Log("Instantiate");
       spawnedSector = obj.Result;
    }
    
    
    [ContextMenu("Release Sector")]
    private void ReleaseSector()
    {
        Addressables.ReleaseInstance(spawnedSector);
    }
    
    [ContextMenu("Load Sector")]
    public void LoadSector()
    {
        AsyncOperationHandle<GameObject> asyncOperationHandle = Addressables.LoadAssetAsync<GameObject>(sectorAsset);
        asyncOperationHandle.Completed +=  LoadAsyncComplete;
    }
    
    private void LoadAsyncComplete(AsyncOperationHandle<GameObject> asyncOperationHandle)
    {
        if(asyncOperationHandle.Status == AsyncOperationStatus.Succeeded)
        {
            Debug.Log("Loading complete!");
            loadedSector = asyncOperationHandle.Result;
            loadedSectorHandle = asyncOperationHandle;
        }
    }
    //
    [ContextMenu("Instantiate Sector")]
    private void InstantiateSector()
    {
        GameObject instadSector = Instantiate(loadedSector, targetTransform.position, Quaternion.identity);
    }
  
    [ContextMenu("Unload Sector")]
    private void UnloadSector()
    {
        Addressables.Release(loadedSectorHandle);
        loadedSector = null;
    
        Debug.Log("Sector unloaded.");
    }
}
