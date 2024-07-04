using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Hand : MonoBehaviour
{
    public int availablePlayerHandCardSlots;
    public List<Card> handCards = new List<Card>();
    public GameObject cardPrefab;

    public void AddCard(Card card)
    {
        availablePlayerHandCardSlots--;
        handCards.Add(card);
        Debug.Log("Added " +card.cardName + " to hand");
        //only what is related to hand
        //when i draw a card from a deck
        //display card in leftmost available slot
        //decrease number of available slots

        GameObject cardGO = Instantiate(cardPrefab, transform);
        DisplayCard displayCard = cardGO.GetComponent<DisplayCard>();
        displayCard.SetCard(card);
    }


    void Start()
    {
        
    }
}
