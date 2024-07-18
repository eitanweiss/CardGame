using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using JetBrains.Annotations;
using Unity.VisualScripting;

public class GameManager : MonoBehaviour
{
    public TMP_Text  DeckSize;
    public CardDB randomCardDB;
    public SavedDeck savedCardDB;
    private List<CardScriptableObject> copySavedDeck;//so changes in play will not affect regular deck and cards drawn will be there again once finished with this match
    public GameObject [] SavedDeckDepth = new GameObject[4];
    public Hand hand;
    public Button drawCardFromSavedDeck;
    public TurnManager turnManager;
    public OpponentBasicAI opponentBasicAI;

    public void DeckImage()
    {
        if (savedCardDB.cards.Count < 30)
        {
            SavedDeckDepth[0].SetActive(false);
        }
        if (savedCardDB.cards.Count < 20)
        {
            SavedDeckDepth[1].SetActive(false);
        }
        if (savedCardDB.cards.Count < 10)
        {
            SavedDeckDepth[2].SetActive(false);
        }
        if (savedCardDB.cards.Count < 1)
        {
            SavedDeckDepth[3].SetActive(false);
        }
        DeckSize.text = savedCardDB.cards.Count.ToString();
    }
    void Start()
    {
        savedCardDB.Randomize();
        copySavedDeck = new List<CardScriptableObject>(savedCardDB.cards);
        DeckImage();
    }

    public void EndGame()
    {
        savedCardDB.cards =  new List<CardScriptableObject>(copySavedDeck);
    }

    public void RandomDeckDraw()
    {
        if(hand.GetComponent<DropZone>().ReachedMaxCards() == false)
        {
            CardScriptableObject card = randomCardDB.randomDraw();

            hand.AddCardFromDeck(card);
        }

    }
    public void SavedDeckDraw()
    {
        if (hand.GetComponent<DropZone>().ReachedMaxCards()==false)
        {
            drawCardFromSavedDeck.gameObject.SetActive(true);
            CardScriptableObject card = savedCardDB.Draw();
            //randomCard.gameObject.SetActive(true);
            //randomCard.transform.position = cardSlots[i].position;
            savedCardDB.cards.Remove(card);
            DeckImage();
            hand.AddCardFromDeck(card);
        }
        else
        {
            drawCardFromSavedDeck.gameObject.SetActive(false);
        }
    }
    /// <summary>
    /// changes phase every time button is pressed
    /// updates which cards are relevant to current phase
    /// activates opponent's AI
    /// </summary>
    public void TurnControl()
    {
        turnManager.ChangePhase();
        gameObject.GetComponent<IsCardPlayable>().UpdateIfCardCanBePlayed();
        opponentBasicAI.Play();
    }
}
