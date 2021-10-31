using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    private Animator animTransition;

    private void Start()
    {
        animTransition = GameObject.FindWithTag("TransitionCanvas").GetComponent<Animator>();
    }

    public void LoadLevelScene(int buildIndex) => StartCoroutine(LoadScene(buildIndex));
    public void LoadLevelScene_NoTransition(int buildIndex) => StartCoroutine(LoadSceneNoTrans(buildIndex));
    
    public void LoadLevelScene(string sceneName) => StartCoroutine(LoadScene(sceneName));
   
    IEnumerator LoadScene(int buildIndex)
    {
        if (animTransition != null)
        {
            animTransition.SetTrigger("SlideTrans");
        }

        yield return new WaitForSecondsRealtime(1f);
        
        AsyncOperation operation = SceneManager.LoadSceneAsync(buildIndex);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            yield return null;
        }
    }
    
    IEnumerator LoadSceneNoTrans(int buildIndex)
    {

        AsyncOperation operation = SceneManager.LoadSceneAsync(buildIndex);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            yield return null;
        }
    }
    
    
    IEnumerator LoadScene(string sceneName)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(sceneName);

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            yield return null;
        }
    }
    
    
}
