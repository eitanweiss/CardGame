using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using JetBrains.Annotations;
using Unity.VisualScripting;
using System.IO;

public class GameManager : MonoBehaviour
{
    public TMP_Text  deckSize;
    public CardDB playerRandomCardDB;
    public CardDB opponentRandomCardDB;
    public CardDB collection;
    public SavedDeck savedCardDB;
    private List<CardScriptableObject> copySavedDeck;
    private List<CardScriptableObject> copyRandomDeck;
    public GameObject [] savedDeckDepth = new GameObject[4];
    public Hand hand;
    public Button drawCardFromSavedDeck;
    public TurnManager turnManager;
    public OpponentBasicAI opponentBasicAI;

    /// <summary>
    /// control deck image in game at runtime by number of cards in the saved deck
    /// </summary>
    public void DeckImage()
    {
        if (savedCardDB.listSize() < 30)
        {
            savedDeckDepth[0].SetActive(false);
        }
        if (savedCardDB.listSize() < 20)
        {
            savedDeckDepth[1].SetActive(false);
        }
        if (savedCardDB.listSize() < 10)
        {
            savedDeckDepth[2].SetActive(false);
        }
        if (savedCardDB.listSize() < 1)
        {
            savedDeckDepth[3].SetActive(false);
        }
        deckSize.text = savedCardDB.listSize().ToString();
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
        savedCardDB.StartUp();
        savedCardDB.Randomize();
        
        copySavedDeck = savedCardDB.GetDeck();
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
    /// ATM called when forfeit the match, will be called when match is over as well.
    /// </summary>
    public void EndGame()
    {
        ResetDecks();
        string json = JsonUtility.ToJson(collection);
        Debug.Log(json);
        File.WriteAllText(Application.dataPath + "saveFile.json", json);
    }

    /// <summary>
    /// refills the decks with all possible cards.
    /// </summary>
    void ResetDecks()
    {
        playerRandomCardDB.allCards= new List<CardScriptableObject>(copyRandomDeck);
        opponentRandomCardDB.allCards= new List<CardScriptableObject>(copyRandomDeck);

    }

    /// <summary>
    /// draw a random card from the random deck
    /// </summary>
    public void RandomDeckDraw()
    {
        if (hand.GetComponent<DropZone>().ReachedMaxCards() == false && hand.drawCount > 0)
        {
            CardScriptableObject card = playerRandomCardDB.randomDraw();

            hand.AddCardFromDeck(card,Origin.RandomDeck);
        }

    }

    /// <summary>
    /// draw a random card from the saved deck
    /// </summary>
    public void SavedDeckDraw()
    {
        if (hand.GetComponent<DropZone>().ReachedMaxCards() == false && hand.drawCount > 0)
        {
            drawCardFromSavedDeck.gameObject.SetActive(true);
            CardScriptableObject card = savedCardDB.Draw();
            savedCardDB.RemoveCard(card);
            DeckImage();
            hand.AddCardFromDeck(card,Origin.SavedDeck);
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
