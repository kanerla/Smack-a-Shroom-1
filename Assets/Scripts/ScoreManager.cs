using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TextMeshProUGUI scoreText;
    private int score = 0;
    private int highscore = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreText.text = "Points: " + score.ToString();
        PlayerPrefs.SetInt("score", 0);
        highscore = PlayerPrefs.GetInt("highscore", 0);
    }

    public void AddPoint()
    {
        score += 10;
        scoreText.text = "Points: " + score.ToString();
        PlayerPrefs.SetInt("score", score);
        if (score > highscore)
        {
            highscore = score;
            PlayerPrefs.SetInt("highscore", score);
        }
    }

    public void ReducePoints()
    {
        score -= 5;
        scoreText.text = "Points: " + score.ToString();
        PlayerPrefs.SetInt("score", score);
    }

    private void Awake()
    {
        instance = this;
    }
}
