using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class GameClock : MonoBehaviour {
    float GameTimer;
    public float GameDuration;
    public Text clockText;

	// Use this for initialization
	void Start ()
    {
        GameTimer = GameDuration;
	}
	
	// Update is called once per frame
	void Update ()
    {
	    if (GameTimer > 0.0f)
        {
            // Subtract DT
            GameTimer -= Time.deltaTime;
            // Ceil the value
            int gameTime = (int)Mathf.Ceil(GameTimer);
            int[] clockTime = SecondsToClockTime(gameTime);
            // update the clock
            clockText.text = clockTime[0].ToString() + ":" + (clockTime[1] >= 10 ? clockTime[1].ToString() : "0" + clockTime[1].ToString());
        }
        else
        {
            //Game End
        }
	}

    int[] SecondsToClockTime(int seconds)
    {
        int[] time = new int[2];
        time[0] = (int)Mathf.Floor(seconds / 60);
        time[1] = seconds % 60;
        return time;
    }
}
