using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using Unity.VisualScripting;

public class Timer : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField]float timeLeft;
    // Update is called once per frame
    void Update()
    {
        //update color when time is low
        if(timeLeft >1 && timeLeft<20)
        {
            timerText.color = Color.yellow;
        }


        if(timeLeft > 1)
        {
            timeLeft -= Time.deltaTime;
        }
        else
        {
            timeLeft = 0;
            timerText.color = Color.red;
        }
        int seconds = Mathf.FloorToInt(timeLeft);
        timerText.text = string.Format("{00}", seconds);
    }
    
}
