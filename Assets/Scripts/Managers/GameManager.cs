using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;
using DG.Tweening;

public enum GameStates
{
    START_GAME,
    SPAWNING,
    WAITING,
    END_GAME
}
public class GameManager : MonoBehaviour
{
    [SerializeField] private GameStates gameStates;
    
    [SerializeField] private GameObject pauseBox, gameOverBox;

    private static GameObject gameOverBoxRef;
    [SerializeField] private float spawnUnitsCountDown = 5f;
    private float spawnUnitsCountAux;
    [SerializeField] private Transform spawnPointHierarchy;
    [SerializeField] private Transform spawnPowerUpPointHierarchy;
    
    [SerializeField] private List<Transform> spawnPoints = new List<Transform>();
    [SerializeField] private List<Transform> spawnPowerUpPoints = new List<Transform>();

    [SerializeField] private GameObject player;
    //Find it with Tag "Score"
    public GameObject scoreUI;
    
    private static float scoreMultiply;

    private static GameManager instance;
    public static GameManager Instance => instance;
    private void Awake()
    {
        instance = this;
        
        spawnUnitsCountAux = spawnUnitsCountDown;
        if (spawnPointHierarchy != null)
        {
            foreach (Transform spawnP in spawnPointHierarchy.transform)
            {
                spawnPoints.Add(spawnP);
            }
        }

        if (spawnPowerUpPointHierarchy != null)
        {
            foreach (Transform p in spawnPowerUpPointHierarchy.transform)
            {
                spawnPowerUpPoints.Add(p);
            }
        }
        
        player = GameObject.FindWithTag("Player");
        
        gameOverBoxRef = gameOverBox;

        gameStates = GameStates.START_GAME;
    }

    public TextMeshProUGUI initializeCountDownText;
    
    private float initializeCountDown = 3f; 
    
    void CountdownToInitializeGame()
    {
        if (gameStates == GameStates.START_GAME)
        {
            initializeCountDown -= Time.deltaTime;
            int d = (int)initializeCountDown;
            initializeCountDownText.text = d.ToString();
            
            if (initializeCountDown <= 0)
            {
                //StartCoroutine(CreateUnits(Random.Range(0.5f, 2.1f)));
                initializeCountDown = 0;
                initializeCountDownText.gameObject.SetActive(false);
                
                gameStates = GameStates.WAITING;
            }
        }
        
        
    }
    
    void SpawnUnitsCountDown()
    {
        if (gameStates == GameStates.WAITING)
        {
            spawnUnitsCountDown -= Time.deltaTime;
            if (spawnUnitsCountDown <= 0)
            {
                gameStates = GameStates.SPAWNING;
                StartCoroutine(CreateUnits(Random.Range(0.5f, 2.1f)));
                StartCoroutine(CreateLifeBatUnits(Random.Range(2f, 3f)));
                spawnUnitsCountDown = spawnUnitsCountAux;
            }
        }
    }
    
    //Create the Enemies
    IEnumerator CreateUnits(float delay)
    {
        if (gameStates == GameStates.SPAWNING)
        {
            for (int i = 0; i < spawnPoints.Count; i++)
            {
                GameObject obj = Pooling.Instance.SpawnFromPool("Enemies",
                    spawnPoints[Random.Range(0, spawnPoints.Count - 1)].position, Quaternion.identity);
                obj.GetComponent<EnemyMovement>().steerAmp = Random.Range(.31f, 1f);
                
                yield return new WaitForSeconds(delay);
            }

            gameStates = GameStates.WAITING;
        }

    }
    //Creates the LifeBats
    IEnumerator CreateLifeBatUnits(float delay)
    {
        if (gameStates == GameStates.SPAWNING)
        {
            for (int i = 0; i < spawnPowerUpPoints.Count; i++)
            {
                GameObject obj = Pooling.Instance.SpawnFromPool("LifeBat",
                    spawnPowerUpPoints[Random.Range(0, spawnPowerUpPoints.Count - 1)].position, Quaternion.identity);

                yield return new WaitForSeconds(delay);
            }
        }
        else
        {
            yield break;
        }

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

    public float result = 0;
    public IEnumerator Score(float points)
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
        CountdownToInitializeGame();
        SpawnUnitsCountDown();
        
        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused && player != null)
        {
            Pause();
        }
        else if (Input.GetKeyDown(KeyCode.Escape) && isPaused && player != null)
        {
            UnPause();    
        }
    }
}
