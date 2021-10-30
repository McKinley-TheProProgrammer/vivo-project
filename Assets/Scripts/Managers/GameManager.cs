using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameManager : MonoBehaviour , IPool
{
    [SerializeField] private GameObject pauseBox, gameOverBox;

    private static GameObject gameOverBoxRef;
    [SerializeField] private float spawnUnitsCountDown;
    [SerializeField] private Transform spawnPointHierarchy;
    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();

    //Find it with Tag "Score"
    public static GameObject scoreUI;
    
    private static float scoreMultiply;
    private void Awake()
    {
        if (spawnPointHierarchy != null)
        {
            foreach (Transform spawnP in spawnPointHierarchy.transform)
            {
                spawnPoints.Add(spawnP);
            }
        }

        gameOverBoxRef = gameOverBox;
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

    public static IEnumerator EndGame(float delay)
    {
        yield return new WaitForSeconds(delay);
        gameOverBoxRef.SetActive(true);
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

    #region Score Placement

    public void UpdateScoreMultiply(float increment) => scoreMultiply += increment;

    public static float result = 0;
    public static IEnumerator Score(float points)
    {

        
        if (scoreMultiply >= 2.5f)
        {
            scoreMultiply = 2.5f;
        }
        result += points * scoreMultiply;
        
    
        yield return new WaitForSecondsRealtime(.1f);
        
        scoreUI.gameObject.GetComponent<TextMeshProUGUI>().text = result.ToString();
    }

    #endregion
    
    //public static IEnumerator GameOver
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
