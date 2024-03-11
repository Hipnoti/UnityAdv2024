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
      loadSceneAsyncOperation = SceneManager.LoadSceneAsync(1);
      loadSceneAsyncOperation.allowSceneActivation = false;
      StartCoroutine(CheckLoadingProgressRoutine());
    }

    public void LoadMainSceneSync()
    {
        SceneManager.LoadScene(1);
    }

    // private void Start()
    // {
    //     SceneManager.LoadScene(1, LoadSceneMode.Additive);
    // }

    private void Update()
    {
        if (loadSceneAsyncOperation != null)
            loadingBar.fillAmount = loadSceneAsyncOperation.progress;
        if (isLoadingWaitingForInput && Input.anyKeyDown)
        {
            loadSceneAsyncOperation.allowSceneActivation = true;
        }
    }

    IEnumerator CheckLoadingProgressRoutine()
    {
        yield return new WaitUntil(() => loadSceneAsyncOperation.progress >= 0.9f);
        anyButtonHintText.gameObject.SetActive(true);
        isLoadingWaitingForInput = true;
    }
}
