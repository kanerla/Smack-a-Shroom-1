using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TextMeshProUGUI scoreText;
    private int score;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        score = 0;
        SetScore();
    }

    public void AddPoint()
    {
        score += 10;
        SetScore();
    }

    public void ReducePoints()
    {
        score -= 5;
        SetScore();
    }

    private void SetScore()
    {
        scoreText.text = "Points: " + score.ToString();
        PlayerPrefs.SetInt("score", score);
    }

    private void Awake()
    {
        instance = this;
    }
}
