using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GhostArrivalTimer : MonoBehaviour
{
    [SerializeField] GameObject Enemy;
    [SerializeField] TextMeshProUGUI timerDisplay;
    [SerializeField] float ghostArrivalTime = 20;
    [SerializeField] float ghostDepartureTime = 10;

    float totalTime;
    float currenttime;

    bool isGhostSpwaned;

    private void Start()
    {
        Enemy.SetActive(false);
        totalTime = ghostArrivalTime + ghostDepartureTime;
        currenttime = totalTime;
    }

    private void Update()
    {
        currenttime -= 1 * Time.deltaTime;

        if (currenttime < totalTime && currenttime > ghostDepartureTime)
        {
            timerDisplay.text = "Ghost Arrival : " + (currenttime - ghostDepartureTime).ToString("0");
        }
        else if (currenttime < ghostDepartureTime)
        {
            timerDisplay.text = "Ghost Departure : " + (currenttime).ToString("0");
        }


        if(currenttime <= (totalTime - ghostArrivalTime) && !isGhostSpwaned)
        {
            isGhostSpwaned = true;
            Enemy.SetActive(true);
        }
        if(currenttime <= 0)
        {
            isGhostSpwaned = false;
            Enemy.SetActive(false);
            currenttime = totalTime;
        }
    }
}
