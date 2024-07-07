using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Hand : MonoBehaviour
{
    //get maxslots from character in future
    public int maxslots;
    public int availablePlayerHandCardSlots;
    public Image playerMana; //in future will have access to the Character or something?
    public DropZone dropZone;
    public GameObject cardPrefab;
    public void getMana()
    {

    }

    public void AddCardFromDeck(Card card)
    {
        GameObject cardGO = Instantiate(cardPrefab, transform);
        CardObject newCardObj = cardGO.AddComponent<CardObject>();
        newCardObj.card = card;

        dropZone.AddCard(newCardObj);
        Debug.Log("Added " +card.cardName + " to hand");
        //only what is related to hand
        //when i draw a card from a deck
        //display card in leftmost available slot
        //decrease number of available slots



        DisplayCard displayCard = cardGO.GetComponent<DisplayCard>();
        displayCard.SetCard(card);
        availablePlayerHandCardSlots = maxslots - GetComponent<HorizontalLayoutGroup>().transform.childCount;
    }
    public void RemoveCard(CardObject cardObj)
    {


        //
        dropZone.RemoveCard(cardObj);
    }

    public void CheckValid()
    {
        foreach (CardObject cardObj in dropZone.handCards)
        {
            //check if correct phase
            //if yes, check if can play card (room in field, cost)
            for (int i = 0; i < cardObj.card.abilities.Count; i++)
            {
                if (cardObj.card.abilities[i].name == "Cost")
                {
                    Image playerMana = transform.parent.GetComponentInChildren<Image>();
                    if(cardObj.card.abilityValues[i] > int.Parse(playerMana.GetComponentInChildren<TMP_Text>().text))
                    {
                        cardObj.GetComponent<Draggable>().enabled = false;
                    }
                    break;
                }
            }
        }
    }


    void Start()
    {
        
    }
}
