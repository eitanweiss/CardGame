using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    public List<CardObject> handCards = new List<CardObject>();
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

    public void AddCard(CardObject card)
    {
        handCards.Add(card);
        if(transform.name =="PlayField")
        {
            transform.GetComponent<ManaManager>().DecreaseMana(card);
        }
    }

    public void RemoveCard(CardObject card)
    {
        handCards.Remove(card);
        if (transform.name == "PlayField")
        {
            transform.GetComponent<ManaManager>().IncreaseMana(card);
        }
    }
}
