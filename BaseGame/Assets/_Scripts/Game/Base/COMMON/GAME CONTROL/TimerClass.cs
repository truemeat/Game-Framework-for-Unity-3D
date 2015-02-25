using UnityEngine;
public class TimerClass
{
    public bool isTimerRuning = false;
    private float timeElapsed = 0.0f;
    private float currentTime = 0.0f;
    private float lastTime = 0.0f;
    private float timeScaleFactor = 1.1f;   // <-- If you need to scale time, change this


    private string timeString;
    private string hour;
    private string minutes;
    private string seconds;
    private string mills;


    private int aHour;
    private int aMinute;
    private int aSecond;
    private int aMills;
    private int tmp;
    private int aTime;


    private GameObject callback;

    private void UpdateTimer()
    {
        // calculate the time elaped since the last Update()
        timeElapsed = Mathf.Abs(Time.realtimeSinceStartup - lastTime);
    }
}
