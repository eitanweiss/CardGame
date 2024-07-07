using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class DropZone : MonoBehaviour, IDropHandler
{
    public List<CardObject> handCards;
    //get maxslots from character in future
    public int maxslots;
    public int availablePlayerHandCardSlots;

    public void OnDrop(PointerEventData eventData)
    {
        Debug.Log("Drop");
        if(eventData.pointerDrag != null)
        {
            if(!ReachedMaxCards())
            {
                eventData.pointerDrag.GetComponent<Draggable>().ChangeParent(transform);
                //eventData.pointerDrag.GetComponent<RectTransform>().parent.SetParent(transform);?
                Debug.Log("set Parent to new zone");
            }
            //make it so it is always possible to switch out buff
            else if(transform.name == "PlayerBuffArea")
            {
                transform.parent.Find("Hand").GetComponent<DropZone>().AddCard(handCards[0]);
                handCards[0].transform.SetParent(transform.parent.Find("Hand").transform);
                this.RemoveCard(handCards[0]);
                eventData.pointerDrag.GetComponent<Draggable>().ChangeParent(transform);
                Debug.Log("max cards in buff zone");
                //switch buff out
            }            

        }
    }

    public bool ReachedMaxCards()
    {
        if(availablePlayerHandCardSlots<1)
        {
            return true;
        }
        return false;
    }
    public void AddCard(CardObject card)
    {
        handCards.Add(card);
        if(transform.name == "PlayerPlayArea")
        {
            transform.parent.GetComponent<ManaManager>().DecreaseMana(card);
        }
        availablePlayerHandCardSlots = maxslots - handCards.Count;

    }

    public void RemoveCard(CardObject card)
    {

        handCards.Remove(card);
        if (transform.name == "PlayerPlayArea")
        {
            transform.parent.GetComponent<ManaManager>().IncreaseMana(card);
        }
        availablePlayerHandCardSlots = maxslots - handCards.Count;
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
