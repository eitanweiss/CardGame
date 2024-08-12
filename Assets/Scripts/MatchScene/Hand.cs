using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Hand : MonoBehaviour
{
    [SerializeField] private GameObject cardPrefab;
    public int drawCount;

    /// <summary>
    /// Reset base draw count to 3
    /// </summary>
    public void ResetDrawCount()
    {
        drawCount = 3;
    }

    /// <summary>
    /// change draw count to change 
    /// </summary>
    /// <param name="change">number of cards that can be drawn next round</param>
    public void ChangeDrawCount(int change)
    {
        drawCount +=change;
    }


    //only hand can get a card from deck so it makes sense to have a special function for it. SRP
    /// <summary>
    /// Builds a gameobject from the card, inserting the display and origin, and adding it to the dropzone
    /// </summary>
    /// <param name="card">card to build around</param>
    /// <param name="origin">origin of card: random deck or saved deck</param>
    public void AddCardFromDeck(CardScriptableObject card,Origin origin)
    {
        GameObject cardGO = Instantiate(cardPrefab, transform);
        CardObject newCardObj = cardGO.AddComponent<CardObject>();
        newCardObj.card = card;
        newCardObj.origin = origin;
        transform.GetComponent<DropZone>().AddCard(newCardObj);
        Debug.Log("Added " +card.cardName + " to hand");
        //only what is related to hand
        //when i draw a card from a deck
        //display card in leftmost available slot
        //decrease number of available slots


        drawCount--;
        DisplayCard displayCard = cardGO.GetComponent<DisplayCard>();
        displayCard.SetCard(card);
    }

    /// <summary>
    /// checks if card is playable due to mana constraints
    /// called in relevant phase only
    /// </summary>
    public void CheckValid()
    {
        foreach (CardObject cardObj in transform.GetComponent<DropZone>().GetList())//for each card in hand
        {
            for (int i = 0; i < cardObj.card.abilities.Count; i++)
            {
                if (cardObj.card.abilities[i].name == "Cost")
                {
                    ManaManager playerMana = transform.parent.GetComponent<ManaManager>();
                    if(cardObj.card.abilityValues[i] > playerMana.GetMana())
                    {
                        cardObj.GetComponent<Draggable>().enabled = false;
                        cardObj.isPlayable = false;
                    }
                    else
                    {

                        cardObj.GetComponent<Draggable>().enabled = true;
                        cardObj.isPlayable = true;
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
