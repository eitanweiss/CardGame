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
    public TMP_Text  deckSize;
    public CardDB playerRandomCardDB;
    public CardDB opponentRandomCardDB;
    public SavedDeck savedCardDB;
    private List<CardScriptableObject> copySavedDeck;//so changes in play will not affect regular deck and cards drawn will be there again once finished with this match
    private List<CardScriptableObject> copyRandomDeck;//so changes in play will not affect regular deck and cards drawn will be there again once finished with this match
    public GameObject [] savedDeckDepth = new GameObject[4];
    public Hand hand;
    public Button drawCardFromSavedDeck;
    public TurnManager turnManager;
    public OpponentBasicAI opponentBasicAI;

    public void DeckImage()
    {
        if (savedCardDB.cards.Count < 30)
        {
            savedDeckDepth[0].SetActive(false);
        }
        if (savedCardDB.cards.Count < 20)
        {
            savedDeckDepth[1].SetActive(false);
        }
        if (savedCardDB.cards.Count < 10)
        {
            savedDeckDepth[2].SetActive(false);
        }
        if (savedCardDB.cards.Count < 1)
        {
            savedDeckDepth[3].SetActive(false);
        }
        deckSize.text = savedCardDB.cards.Count.ToString();
    }
    void Start()
    {
        SetupDecks();
        
    }
    /// <summary>
    /// initial setup for decks at the start of each match
    /// </summary>
    private void SetupDecks()
    {
        savedCardDB.Randomize();
        
        copySavedDeck = new List<CardScriptableObject>(savedCardDB.cards);
        copyRandomDeck = new List<CardScriptableObject>(playerRandomCardDB.allCards);

        Character player = GameObject.Find("PlayerObject").GetComponent<Character>();
        playerRandomCardDB.LimitByRace(playerRandomCardDB.allCards, player.race);
        playerRandomCardDB.LimitByType(playerRandomCardDB.allCards, player.type);
        playerRandomCardDB.SetDrawCount(player.handSlots);//first round can pick up max number of cards

        Character opponent = GameObject.Find("OpponentObject").GetComponent<Character>();
        opponentRandomCardDB.LimitByRace(opponentRandomCardDB.allCards, opponent.race);
        opponentRandomCardDB.LimitByType(opponentRandomCardDB.allCards, opponent.type);
        opponentRandomCardDB.SetDrawCount(opponent.handSlots);//first round can pick up max number of cards

        DeckImage();
    }

    /// <summary>
    /// ATM called from yellow button on bottom left, will be called when match is over
    /// </summary>
    public void EndGame()
    {
        ResetDecks();
    }

    void ResetDecks()
    {
        savedCardDB.cards = new List<CardScriptableObject>(copySavedDeck);
        playerRandomCardDB.allCards= new List<CardScriptableObject>(copyRandomDeck);
        opponentRandomCardDB.allCards= new List<CardScriptableObject>(copyRandomDeck);

    }

    /// <summary>
    /// 
    /// </summary>
    public void RandomDeckDraw()
    {
        if (hand.GetComponent<DropZone>().ReachedMaxCards() == false && hand.drawCount > 0)
        {
            CardScriptableObject card = playerRandomCardDB.randomDraw();

            hand.AddCardFromDeck(card);
        }

    }
    public void SavedDeckDraw()
    {
        if (hand.GetComponent<DropZone>().ReachedMaxCards()==false && hand.drawCount > 0)
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
