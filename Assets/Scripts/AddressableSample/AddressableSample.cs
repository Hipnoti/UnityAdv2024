using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.ResourceManagement.AsyncOperations;

public class AddressableSample : MonoBehaviour
{
    private AssetReference enemyAssetReference;

    private GameObject loadedEnemyObject;
    private Transform targetTransform;
    
    
    private List<GameObject> spawnedEnemies = new List<GameObject>();
    
    private void LoadEnemyAsset()
    {
        AsyncOperationHandle<GameObject> handle =
            Addressables.LoadAssetAsync<GameObject>(enemyAssetReference);
        handle.Completed += OnEnemyAssetLoaded;
    }

    private void OnEnemyAssetLoaded(AsyncOperationHandle<GameObject> operation)
    {
        if (operation.Status == AsyncOperationStatus.Succeeded)
        {
            loadedEnemyObject = operation.Result;
        }
        else
        {
            Debug.LogError("Failed to load enemy asset.");
        }
    }

    private void InstantiateEnemy()
    {
        if (loadedEnemyObject)
        {
            AsyncOperationHandle<GameObject> handle = Addressables.InstantiateAsync(loadedEnemyObject);
            handle.Completed += EnemyInstantiateComplete;
        }
    }

    private void EnemyInstantiateComplete(AsyncOperationHandle<GameObject> obj)
    {
        if (obj.Status == AsyncOperationStatus.Succeeded)
        {
            spawnedEnemies.Add(obj.Result);
        }
        else
        {
           Debug.LogError("Failed to instantiate enemy");
        }
    }

    private void ReleaseEnemyInstance(GameObject enemy)
    {
        Addressables.ReleaseInstance(enemy);
    }

    private void ReleaseEnemyAsset()
    {
        Addressables.Release(loadedEnemyObject);
    }
    
    
    
}
