using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;


/// <summary>
/// every area where cards can be in will havea dropzone. 
/// <param name="handCards"> a list of cards which are connected to the prefab children of gameObject this is attached to </param>
/// <param name="maxslots">maximum number of cards allowed here at any given time </param>
/// <param name="availablePlayerHandCardSlots">current available slots </param>
/// </summary>
public class DropZone : MonoBehaviour
{

    private List<CardObject> handCards = new List<CardObject>();
    //get maxslots from character in future
    [SerializeField] private int maxslots;//this will be determined from player/opp abilities later on, not directly from editor
    [SerializeField] private int availablePlayerHandCardSlots = 3;
    //TODO: add slots to dropzone instead of layoutgroup.

    public void setMaxSlots(int num)
    {
        maxslots = num;
    }
    public void ResetAvailablePlayerHandCardSlots()
    {
        availablePlayerHandCardSlots = maxslots;
    }
    public ReadOnlyCollection<CardObject> GetList()
    {
        return handCards.AsReadOnly();
    }

    /// <summary>
    /// will change when i implement clickable instead of draggable 
    /// </summary>
    /// <param name="eventData"> mouse </param>
    //public void OnDrop(PointerEventData eventData)
    //{
    //    if (eventData.pointerDrag != null)
    //    {
    //        if (!ReachedMaxCards())
    //        {
    //            eventData.pointerDrag.GetComponent<Draggable>().ChangeParent(transform);
    //        }
    //        //make it so it is always possible to switch out buff
    //        else if (transform.name == "PlayerBuffArea")
    //        {
    //            //switch buff out one for one
    //            transform.parent.Find("Hand").GetComponent<DropZone>().AddCard(handCards[0]);
    //            handCards[0].transform.SetParent(transform.parent.Find("Hand").transform);
    //            this.RemoveCard(handCards[0]);
    //            eventData.pointerDrag.GetComponent<Draggable>().ChangeParent(transform);
    //            //Debug.Log("max cards in buff zone");
    //        }

    //    }
    //}

    /// <summary>
    /// check if another card can be added
    /// </summary>
    /// <returns>true if the maximum amount of cards is in the zone, false otherwise</returns>
    public bool ReachedMaxCards()
    {
        if (availablePlayerHandCardSlots < 1)
        {
            return true;
        }
        return false;
    }
    /// <summary>
    /// adds card to dropzone and updates availableslots
    /// </summary>
    /// <param name="card"> card to be added to dropzone </param>
    public void AddCard(CardObject card)
    {
        handCards.Add(card);
        if (transform.name == "PlayerPlayArea" || transform.name == "OpponentPlayArea")
        {
           transform.parent.GetComponent<ManaManager>().ChangeMana(card, true);
           GameObject.Find("GameManager").GetComponent<IsCardPlayable>().UpdateIfCardCanBePlayed();
        }
        availablePlayerHandCardSlots = maxslots - handCards.Count;//make it independant of previous values

    }
    /// <summary>
    /// removes card from dropzone and updates availableslots
    /// </summary>
    /// <param name="card">card to be removed from dropzone</param>
    public void RemoveCard(CardObject card)
    {

        handCards.Remove(card);
        
        ////
        ///needed to fix the problem of removing card but not back to hand.
        ///might want to find a cleaner solution than adding the second condition here
        ///it is a little ugly
        if ((transform.name == "PlayerPlayArea" || transform.name == "OpponentPlayArea")&&
            GameObject.Find("TurnManager").GetComponent<TurnManager>().GetPhase() != TurnManager.Phase.Calculation)
        {
            transform.parent.GetComponent<ManaManager>().ChangeMana(card,false);
        }
        availablePlayerHandCardSlots = maxslots - handCards.Count;
    }
    /// <summary>
    /// removes all of the cards
    /// </summary>
    public void RemoveAllCards()
    {
        while(handCards.Count > 0)
        {
            Destroy(handCards[0].gameObject);
            handCards.Remove(handCards[0]);
        }

    }
    /// <summary>
    /// enable user interface with all cards in zone
    /// </summary>
    public void ActivateDrag()
    {
        //Debug.Log(enabled);
        if(enabled)//redundant, called only after disabling anyway
        {
            foreach (CardObject card in handCards)
            {
                //Debug.Log("in foreach");
                card.GetComponent<Draggable>().enabled = true;
                card.isPlayable = true;
            }
        }
    }
    /// <summary>
    /// disable user interface with all cards in zone
    /// </summary>
    public void DisableDrag()
    {
        foreach (CardObject card in handCards)
        {
            card.GetComponent<Draggable>().enabled = false;
            card.isPlayable = false;
        }
    }

    public void MoveCardToOtherZone(CardObject card)
    {
        if(transform.name == "Hand")
        {
            //from hand
        }
        else
        {
            GameObject.Find("Hand").GetComponent<DropZone>().AddCard(card);
            //give to hand
        }
        //move card to other dropzone
    }
}
