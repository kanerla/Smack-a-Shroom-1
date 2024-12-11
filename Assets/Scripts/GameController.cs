using UnityEngine;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using TMPro;

public class GameController : MonoBehaviour
{
    private bool playing = false;
    public List<Mushroom> mushrooms;
    public HashSet<Mushroom> currentMushrooms = new HashSet<Mushroom>();

    void Start()
    {
        for (int i = 0; i < mushrooms.Count; i++)
        {
            mushrooms[i].Hide();
            mushrooms[i].SetIndex(i);
        }
        currentMushrooms.Clear();

        playing = true;

        TimerScript.instance.StartTimer();
    }

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
    }

    public void GameOver()
    {
        foreach (Mushroom mushroom in mushrooms)
        {
            mushroom.StopGame();
        }

        SceneManager.LoadScene(2);
    }

}
