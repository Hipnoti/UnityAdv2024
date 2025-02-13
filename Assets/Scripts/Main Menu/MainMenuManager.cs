using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class MainMenuManager : MonoBehaviour
{
    private AsyncOperation loadSceneAsyncOperation;
    [SerializeField] private Image loadingBar;
    [SerializeField] private TextMeshProUGUI anyButtonHintText;

    private bool isLoadingWaitingForInput = false;
        
    public void LoadMainSceneAsync()
    { 
      loadSceneAsyncOperation = SceneManager.LoadSceneAsync("Audio Scene");
      loadSceneAsyncOperation.allowSceneActivation = false;
       StartCoroutine(CheckLoadingProgressRoutine());
    }

    public void LoadMainSceneSync()
    {
        SceneManager.LoadScene(1);
    }
    
    private void Update()
    {
        if (loadSceneAsyncOperation != null)
        {
            loadingBar.fillAmount = loadSceneAsyncOperation.progress;
        }

        if (isLoadingWaitingForInput && Input.anyKeyDown)
        {
            loadSceneAsyncOperation.allowSceneActivation = true;
        }
    }

    IEnumerator CheckLoadingProgressRoutine()
    {
        yield return new WaitUntil(IsLoadingComplete);
        //play sound
        yield return new WaitForSeconds(1);
        anyButtonHintText.gameObject.SetActive(true);
        isLoadingWaitingForInput = true;
    }

    private bool IsLoadingComplete()
    {
        return loadSceneAsyncOperation.progress >= 0.9f;
    }
}
