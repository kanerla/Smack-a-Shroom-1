using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    void Start()
    {
        if (PlayerPrefs.GetInt("score", 0) >= 0)
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
