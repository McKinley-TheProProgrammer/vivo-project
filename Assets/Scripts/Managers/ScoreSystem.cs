using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreSystem : MonoBehaviour
{
    private double score;
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private float scoreMultiplier = 1.5f;

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
        this.score = (points * scoreMultiplier);
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
    private void Update()
    {
        this.score = Damage.playerScore;

        float updatedScoreMultiplier = scoreMultiplier * Time.time * .1f;
        
        UpdateMultiplier(updatedScoreMultiplier);

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            print(updatedScoreMultiplier);
        }
    }
}
