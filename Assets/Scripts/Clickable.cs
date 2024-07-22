using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Clickable : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    float lastClickTime;
    float doubleClickThreshold = 0.2f;
    bool isCardPressed;
    Coroutine coroutine;

    private void Update()
    {
        // Single Click Detection
        if(isCardPressed)
        {
            if(Input.GetMouseButtonDown(0))
            {
                float _timeBetween = Time.time-lastClickTime;
                if(_timeBetween <= doubleClickThreshold ) 
                {
                    StopCoroutine(coroutine);
                    //move to other active zone
                    StartCoroutine(HandleDoubleClick());
                    Debug.Log("double Click!");
                }
                else
                {
                    //open in enhanced window so it can be read
                    coroutine = StartCoroutine(HandleSingleClick());
                }
                lastClickTime = Time.time;
            }
        }
    }
    IEnumerator HandleSingleClick()
    {
        yield return new WaitForSeconds(doubleClickThreshold);
        //dostuff
        Debug.Log("Click!");
    }

    IEnumerator HandleDoubleClick()
    {
        TurnManager.Phase phase = GameObject.FindObjectOfType<TurnManager>().GetPhase();
        switch (phase)
        {
            case TurnManager.Phase.PlayBuff:
                if (transform.parent.name == "Hand")
                {
                    DropZone newZone = GameObject.Find("PlayerObject").GetComponentsInChildren<DropZone>()[2];

                    transform.parent.GetComponent<DropZone>().RemoveCard(gameObject.GetComponent<CardObject>());
                    transform.SetParent(newZone.transform);
                    MoveTo(newZone.transform.position);
                    //move to buff
                }
                //move to/from buff
                break;
            case TurnManager.Phase.Discard:
                if (transform.parent.name == "Hand")
                {
                    DropZone newZone = GameObject.Find("Discard").GetComponent<DropZone>();
                    MoveTo(newZone.transform.position);
                    //move to discard
                }
                //move to/from discard
                break;
            case TurnManager.Phase.PlayCard:

                if (transform.parent.name == "Hand")
                {
                    DropZone newZone = GameObject.Find("PlayerObject").GetComponentsInChildren<DropZone>()[0];
                    MoveTo(newZone.transform.position);
                    //move to play
                }
                break;
            default:
                if(gameObject.GetComponent<CardObject>().isPlayable)
                {
                    DropZone newZone = GameObject.Find("PlayerObject").GetComponentsInChildren<DropZone>()[3];
                    MoveTo(newZone.transform.position);
                }
                break;
        }
        //lerp
        //at end of lerp add to newzone
        yield return null;
    }


    public void OnPointerDown(PointerEventData eventData)
    {
        isCardPressed = true;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isCardPressed=false;
    }
    public void MoveTo(Vector3 position)
    {
        this.transform.position = position;
    }
}
