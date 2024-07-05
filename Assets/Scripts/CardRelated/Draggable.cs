using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Draggable : MonoBehaviour,IPointerDownHandler,IBeginDragHandler,IDragHandler,IEndDragHandler
{
    public Transform transformToReturnTo;
    Vector2 offset;
    private CanvasGroup canvasGroup;
    public bool isBeingDragged;

    void Awake ()
    {
        canvasGroup = GetComponent<CanvasGroup>();
    }
    public void OnBeginDrag(PointerEventData eventData)
    {
        //evemtData is Vector2 so need to cast it to 3D
        isBeingDragged = true;
        offset = new Vector3(eventData.position.x,eventData.position.y,transform.position.z) - transform.position;
        transformToReturnTo = this.transform.parent;
        transform.SetParent(transform.parent.parent);
        canvasGroup.blocksRaycasts = false;
        Debug.Log("Begin drag!");
    }

    public void OnDrag(PointerEventData eventData)
    {
        this.transform.position = eventData.position - offset;
        //Debug.Log("Begin drag");
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        //if over drop zone don't reset parent?
        transform.SetParent(transformToReturnTo);
        canvasGroup.blocksRaycasts = true;
        isBeingDragged=false;
        Debug.Log("Drag end!");
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
