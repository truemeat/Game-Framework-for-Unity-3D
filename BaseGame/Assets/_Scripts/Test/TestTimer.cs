using UnityEngine;
using System.Collections;

public class TestTimer : MonoBehaviour {

    private TimerClass theTimer;


    void Start()
    {
        theTimer = new TimerClass();
        theTimer.StartTimer();
    }

    void Update()
    {
        Debug.Log(theTimer.GetFormattedTime() + "   " + theTimer.GetTime());
    }
}
