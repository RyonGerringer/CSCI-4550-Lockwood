using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerController : MonoBehaviour
{

    // Reference to the timer textfield.
    public Text TimerText;

    // References to several game object(s).
    public GameObject GameOverText, RestartButton, PlayerController;

    // The alotted time or starting time.
    // This should be provided in MS; 1000 MS = 1 second, 60000 MS = 60 seconds (1 min.)
    // Should be set in inspector panel.
    public float timeLimit;

    // Tracks current minutes as floating point value.
    private float minutes;

    // Tracks current seconds as a floating point value.
    private float seconds;

    // Total number of seconds; computed from 'timeLimit'.
    private float realSeconds;

    // Is the timer running?
    private bool Paused;
    // Start is called before the first frame update
    void Start()
    {


        this.Paused = false;

        // If the time limit is not specified in the inspector panel,
        // default it to one minute.
        if (this.timeLimit == 0.0f)
        {

            this.realSeconds = 60.0f;
            this.minutes = 1.0f;
            this.seconds = 60.0f;

        }
        else
        {

            this.realSeconds = timeLimit / 1000.0f;
            this.minutes = (realSeconds / 60.0f) < 1 ? 0 : (realSeconds / 60.0f);

        }

        float a = realSeconds % 60 == 0 ? 0.0f : realSeconds / (minutes * 2.0f);
        int b = (int)a;
        string s = seconds < 10.0f ? "0" + b.ToString() : b.ToString();
        this.seconds = a;
        this.TimerText.text = ((int)this.minutes) + " : " + s;

    }

    // Update is called once per frame
    void Update()
    {

        // Update seconds
        seconds = seconds - Time.deltaTime;

        if (Paused)
        {


            KillVisuals();
            Text WonText = GameOverText.GetComponent<Text>();
            WonText.text = "You WON!";

        }

        // If there is less than or equal to five seconds remaining make 
        // text color red.
        if (minutes < 1.0f && seconds <= 5.0f)
        {

            TimerText.color = Color.red;

        }

        // Minutes and seconds are zero so time is up.
        if (minutes < 1.0f && seconds <= 0.0f)
        {

            minutes = 0.0f;
            seconds = 0.0f;
            KillVisuals();

        }

        // Is the minute over? If so, start a new minute by decrementing minute
        // and resetting seconds to 60.
        if (minutes > 0.0f && (seconds <= 0.0f || timeLimit == 60000))
        {

            minutes--;
            seconds = 60.0f;

        }

        this.TimerText.text = ((int)this.minutes) + " : " + GetFixedSeconds();

    }

    // Pause() sets the pause flag to true and effectively, ends the game on the next frame update.
    public void Pause()
    {

        this.Paused = true;

    }

    // KillVisuals() disables active visual components if the timer is paused
    // or the timer runs out.
    private void KillVisuals()
    {

        enabled = false;
        GameOverText.SetActive(true);
        RestartButton.SetActive(true);
        PlayerController.SetActive(false);

    }

    private string GetFixedSeconds()
    {

        // Multiple's of 60 mean the minute just started and we should show "00"
        if ((seconds % 60.0f) == 0.0f)
        {

            return "00";

        }
        else
        {

            // Format seconds
            string s = ((int)seconds).ToString();
            return ((seconds < 10.0f) ? "0" + s : s);

        }
    }
}
