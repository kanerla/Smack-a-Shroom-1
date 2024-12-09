using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{

    public void OnPlayAgainButton()
    {
        SceneManager.LoadScene(1);
    }

    public void OnBackToMenuButton()
    {
        SceneManager.LoadScene(0);
    }
}
