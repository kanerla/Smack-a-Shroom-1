using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameOver : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI scoreText;
    [SerializeField] private TextMeshProUGUI highscoreText;
    private int score;
    private int highscore;
    void Start()
    {
        score = PlayerPrefs.GetInt("score", 0);
        highscore = PlayerPrefs.GetInt("highscore", 0);

        if (score > highscore)
        {
            highscore = score;
            PlayerPrefs.SetInt("highscore", score);
        }

        scoreText.text = "Points: " + score.ToString();
        highscoreText.text = "Highscore: " + highscore.ToString();
        if (score >= 0)
        {
            AudioScript.instance.PlayPositive();
        } else {
            AudioScript.instance.PlayNegative();
        }
    }

    public void OnPlayAgainButton()
    {
        SceneManager.LoadScene(1);
    }

    public void OnBackToMenuButton()
    {
        SceneManager.LoadScene(0);
    }
}
