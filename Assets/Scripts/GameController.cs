using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    private bool playing = false;
    public List<Mushroom> mushrooms;
    public HashSet<Mushroom> currentMushrooms = new HashSet<Mushroom>();
    [SerializeField] private GameObject gameOverPanel;
    [SerializeField] private GameObject backToMenuButton;
    [SerializeField] private GameObject playAgainButton;
    [SerializeField] private TextMeshProUGUI gameOverText;
    // [SerializeField] private TextMeshProUGUI playOrQuitText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        gameOverPanel.SetActive(false);
        backToMenuButton.SetActive(false);
        playAgainButton.SetActive(false);
        gameOverText.enabled = false;
        ScoreManager.instance.scoreText.enabled = true;
        TimerScript.instance.timerText.enabled = true;
        // playOrQuitText.enabled = false;

        for (int i = 0; i < mushrooms.Count; i++)
        {
            mushrooms[i].Hide();
            mushrooms[i].SetIndex(i);
        }
        currentMushrooms.Clear();

        playing = true;

        TimerScript.instance.StartTimer();
    }

    // Update is called once per frame
    void Update()
    {
        if (playing)
        {
            if (TimerScript.instance.TimerOn) {
                if (currentMushrooms.Count < 1)
                {
                    int index = Random.Range(0, mushrooms.Count);
                    if (!currentMushrooms.Contains(mushrooms[index]))
                    {
                        currentMushrooms.Add(mushrooms[index]);
                        mushrooms[index].Activate();
                    }
                
                }
            } else {
                playing = false;
                GameOver();
            }
        }
/*         else {
            // Play again
            if (Input.GetKeyDown(KeyCode.P))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            }

            // Quit
            if (Input.GetKeyDown(KeyCode.Q))
            {
                print("Application Quit");
                Application.Quit();
            }
        } */
    }

    public void GameOver()
    {
        foreach (Mushroom mushroom in mushrooms)
        {
            mushroom.StopGame();
        }

        gameOverPanel.SetActive(true);
        backToMenuButton.SetActive(true);
        playAgainButton.SetActive(true);
        gameOverText.enabled = true;
        ScoreManager.instance.scoreText.enabled = false;
        TimerScript.instance.timerText.enabled = false;
        // playOrQuitText.enabled = true;
    }

    public void OnPlayAgainButton()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void OnBackToMenuButton()
    {
        SceneManager.LoadScene(0);
    }
}
