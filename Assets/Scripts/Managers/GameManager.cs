using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour , IPool
{
    [SerializeField] private GameObject pauseBox;
    
    [SerializeField] private float spawnUnitsCountDown;
    [SerializeField] private Transform spawnPointHierarchy;
    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();
    private void Awake()
    {
        if (spawnPointHierarchy != null)
        {
            foreach (Transform spawnP in spawnPointHierarchy.transform)
            {
                spawnPoints.Add(spawnP);
            }
        }
    }

    void SpawnUnitsCountDown()
    {
        
    }
    
    IEnumerator CreateUnits(float delay)
    {
        for (int i = 0; i < spawnPoints.Count; i++)
        {
            GameObject obj = Pooling.Instance.SpawnFromPool("Enemies", spawnPoints[Random.Range(0,spawnPoints.Count - 1)].position, Quaternion.identity);

            yield return new WaitForSeconds(delay);
        }
       
    }

    public void OnSpawnedObject()
    {
        
    }

    #region Pause Menu Options

    private bool isPaused;
    public void Pause()
    {
        pauseBox.SetActive(true);
        Time.timeScale = 0;
        isPaused = true;
    }
    public void UnPause()
    {
        pauseBox.SetActive(false);
        Time.timeScale = 1;
        isPaused = false;
    }

    public void QuitGame()
    {
        Application.Quit();
    }
    #endregion
    

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            Pause();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused)
        {
            UnPause();    
        }
    }
}
