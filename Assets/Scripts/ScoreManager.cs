using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public TextMeshProUGUI scoreText;
    int score = 0;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        scoreText.text = "Points: " + score.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void AddPoint()
    {
        score += 10;
        scoreText.text = "Points: " + score.ToString();
    }

    public void ReducePoints()
    {
        score -= 5;
        scoreText.text = "Points: " + score.ToString();
    }

    private void Awake()
    {
        instance = this;
    }
}
