using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimeSystem : MonoBehaviour
{
    public Text timeCounter;
    public Text gameStatus; //Temporary Game State Indicator
    private float timeSec = 0; //60secs IRL = 1hr in the GAME
    private float timeMin = 0;
    private float timeHr = 0;
    private float endTime = 213f;
    // Start is called before the first frame update
    void Start()
    {
        gameStatus.text = "Game in Progress";
    }

    // Update is called once per frame
    void Update()
    {
        TimeCounter();
    }

    public void TimeCounter()
    {
        if(timeSec < endTime)
        {
            timeSec += Time.deltaTime;
            timeMin = Mathf.Floor(timeSec % 60);
            timeHr = Mathf.Floor(timeSec / 60);
            timeCounter.text = "Time: " + timeHr.ToString() + " Hr " + timeMin.ToString() + " Mins";
        }
        else if(timeSec >= endTime)
        {
            timeSec = endTime;
            gameStatus.text = "Game Over";
        }
        
        
    }
}
