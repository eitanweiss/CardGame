using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Clickable : MonoBehaviour
{
    private bool isSingleClick = false;
    private bool isDoubleClick = false;
    private bool isLongClick = false;
    
    float lastClickTime = 0f;
    float doubleClickThreshold = 0.2f;
    float longClickDuration = 1f;

    // Update is called once per frame
    void Update()
    {
        // Single Click Detection
        //for hover effect
        if(Input.GetMouseButtonDown(0) &&!isLongClick)
        {
            if(!isSingleClick && Time.time -lastClickTime > doubleClickThreshold)
            {
                isSingleClick = true;
                //do stuff with delay
                //make it false again
            }
        }

        // Double Click Detection
        //for moving between dropzones
        if (Input.GetMouseButtonDown(0) && !isLongClick)
        {
            if (isSingleClick && Time.time - lastClickTime <= doubleClickThreshold)
            {
                isSingleClick = false;
                isDoubleClick = true;
                //check if card is playable. if yes, double click moves it to relevant dropzone
                Debug.Log("Double click on card: " + gameObject.name);
            }
            lastClickTime = Time.time;
        }

        // Long Click Detection
        //for information
        if (Input.GetMouseButtonDown(0))
        {
            isLongClick = true;
            //StartCoroutine(LongClickRoutine());
        }
        if (Input.GetMouseButtonUp(0))
        {
            //StopCoroutine(LongClickRoutine());
            isLongClick = false;
        }
    }
}
