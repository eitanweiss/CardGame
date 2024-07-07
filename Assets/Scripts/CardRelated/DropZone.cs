using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class DropZone : MonoBehaviour, IDropHandler
{
    public List<CardObject> handCards;
    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Drop");
        if(eventData.pointerDrag != null)
        {
            eventData.pointerDrag.GetComponent<Draggable>().ChangeParent(transform);
            //eventData.pointerDrag.GetComponent<RectTransform>().parent.SetParent(transform);?
            Debug.Log("set Parent to new zone");
        }
    }

    public void AddCard(CardObject card)
    {
        handCards.Add(card);
        if(transform.name == "PlayerPlayArea")
        {
            transform.parent.GetComponent<ManaManager>().DecreaseMana(card);
        }
    }

    public void RemoveCard(CardObject card)
    {
        handCards.Remove(card);
        if (transform.name == "PlayerPlayArea")
        {
            transform.parent.GetComponent<ManaManager>().IncreaseMana(card);
        }
    }


    public void ActivateDrag()
    {
        Debug.Log(enabled);
        if(enabled)//redundant, called only after disabling anyway
        {
            foreach (CardObject card in handCards)
            {
                Debug.Log("in foreach");
                card.GetComponent<Draggable>().enabled = true;
            }
        }
    }

    public void DisableDrag()
    {
        foreach (CardObject card in handCards)
        {
            card.GetComponent<Draggable>().enabled = false;
        }
    }
}
