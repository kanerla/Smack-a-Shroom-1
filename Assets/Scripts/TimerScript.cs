using UnityEngine;
using UnityEngine.UI;
using TMPro; 

public class TimerScript : MonoBehaviour
{
    public static TimerScript instance;
    public float TimeLeft = 30;
    public bool TimerOn = false;
    public TextMeshProUGUI timerText;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public void StartTimer()
    {
        TimerOn = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (TimerOn)
        {
            if (TimeLeft > 0)
            {
                TimeLeft -= Time.deltaTime;
                updateTimer(TimeLeft);
            } else {
                TimeLeft = 0;
                TimerOn = false;
            }
        }
    }

    void updateTimer(float currentTime)
    {
        currentTime += 1;
        float seconds = Mathf.FloorToInt(currentTime % 60);

        timerText.text = string.Format("{00}", seconds);
    }

    private void Awake()
    {
        instance = this;
    }
}
