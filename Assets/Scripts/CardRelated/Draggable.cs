using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour,IPointerDownHandler,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    public Transform transformToReturnTo;
    Vector2 offset;
    private CanvasGroup canvasGroup;
    
    void Awake ()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }

    public void ChangeParent(Transform transform)
    {
        transformToReturnTo = transform;
    }


    public void OnBeginDrag(PointerEventData eventData)
    {
        //if (isPlayerTurn())
        //{
            //eventData is Vector2 so need to cast it to 3D
            offset = new Vector3(eventData.position.x,eventData.position.y,transform.position.z) - transform.position;
            transformToReturnTo = this.transform.parent;//set home to where card was
            transform.parent.GetComponent<DropZone>().RemoveCard(eventData.pointerDrag.GetComponent<CardObject>());//take parent(hand/playfield etc.) and remove card from list
            transform.SetParent(transform.parent.parent);//move card to be child of canvas
            canvasGroup.blocksRaycasts = false;
            //Debug.Log("Start drag!");

        //}
        //else
        //{

        //}
    }
    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position - offset;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //if over drop zone don't reset parent?
        transform.SetParent(transformToReturnTo);
        Debug.Log(transformToReturnTo.name);
        canvasGroup.blocksRaycasts = true;
        transform.parent.GetComponent<DropZone>().AddCard(eventData.pointerDrag.GetComponent<CardObject>());//take parent(hand/playfield etc.) and add card to list
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("click!");
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
