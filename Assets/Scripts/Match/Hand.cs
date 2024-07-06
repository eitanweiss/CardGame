using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Hand : MonoBehaviour
{
    //get maxslots from character in future
    public int maxslots;
    public int availablePlayerHandCardSlots;
    public DropZone dropZone;
    public GameObject cardPrefab;

    public void AddCardFromDeck(CardObject cardObj)
    {
        dropZone.AddCard(cardObj);
        Debug.Log("Added " +cardObj.card.cardName + " to hand");
        //only what is related to hand
        //when i draw a card from a deck
        //display card in leftmost available slot
        //decrease number of available slots

        GameObject cardGO = Instantiate(cardPrefab, transform);
        CardObject newCardObj = cardGO.AddComponent<CardObject>();
        newCardObj.card = cardObj.card;
        DisplayCard displayCard = cardGO.GetComponent<DisplayCard>();
        displayCard.SetCard(cardObj.card);
        availablePlayerHandCardSlots = maxslots - GetComponent<HorizontalLayoutGroup>().transform.childCount;
    }
    public void RemoveCard(CardObject cardObj)
    {



        //
        dropZone.handCards.Remove(cardObj);
    }

    void Start()
    {
        
    }
}
