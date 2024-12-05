using UnityEngine;
using System.Collections.Generic;

public class GameController : MonoBehaviour
{
    private TimerScript timer;
    public List<Mushroom> mushrooms;
    public HashSet<Mushroom> currentMushrooms = new HashSet<Mushroom>();

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        for (int i = 0; i < mushrooms.Count; i++)
        {
            mushrooms[i].Hide();
            mushrooms[i].SetIndex(i);
        }
        currentMushrooms.Clear();
    }

    // Update is called once per frame
    void Update()
    {
        if (TimerScript.instance.TimerOn)
        {
            if (currentMushrooms.Count < 1)
            {
                int index = Random.Range(0, mushrooms.Count);
                if (!currentMushrooms.Contains(mushrooms[index]))
                {
                    currentMushrooms.Add(mushrooms[index]);
                    mushrooms[index].Activate();
                }
            
            }
        }
    }

    public void GameOver()
    {
        foreach (Mushroom mushroom in mushrooms)
        {
            mushroom.StopGame();
        }
    }
}
