using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Drop");
        if(eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<Draggable>().transformToReturnTo = transform;
            //eventData.pointerDrag.GetComponent<RectTransform>().parent.SetParent(transform);?
            Debug.Log("set Parent to new zone");
        }
    }
}
