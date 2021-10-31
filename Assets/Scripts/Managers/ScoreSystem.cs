using System;
using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
    private double score;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private float scoreMultiplier = 1.5f;

    [SerializeField] private GameObject scoreUI;
    
    private static ScoreSystem instance;

    public static ScoreSystem Instance => instance;
    private void Awake()
    {
        instance = this;
    }

    private void Start()
    {
        scoreText.text = 0.ToString();
    }

    public void MultiplyScore(int points)
    {
        this.score += (points * scoreMultiplier);
        scoreText.text = score.ToString();
    }

    public void UpdateMultiplier(float multi)
    {
        if (multi > 2.5f)
        {
            multi = 2.5f;
            scoreMultiplier = multi;
        }
    }
    
    public void UpdateScoreMultiply(float increment) => scoreMultiplier += increment;

    public float result = 0;
    public IEnumerator Score(float points)
    {

        
        if (scoreMultiplier >= 2.5f)
        {
            scoreMultiplier = 2.5f;
        }
        result += points * scoreMultiplier;
        
    
        yield return new WaitForSecondsRealtime(.1f);
        
        scoreUI.gameObject.GetComponent<TextMeshProUGUI>().text = result.ToString();
        scoreText.rectTransform.DOLocalMove(new Vector3(0,scoreText.rectTransform.localPosition.y,
            scoreText.rectTransform.localPosition.z), 2);
    }
    private void Update()
    {
        //this.score = Damage.playerScore;

        float updatedScoreMultiplier = scoreMultiplier * Time.time * .1f;
        
        UpdateMultiplier(updatedScoreMultiplier);
        
        //UpdateScoreMultiply(updatedScoreMultiplier);

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            print(updatedScoreMultiplier);
        }
    }
}
