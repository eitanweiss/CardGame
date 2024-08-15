using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// basic opp AI for game feel when testing things out,
/// set up for card playing,mana changes and calculations of damage at end of round
/// </summary>
public class OpponentBasicAI : MonoBehaviour
{
    public CardDB randomCardDB;
    public Hand hand;

    /// <summary>
    /// Basic actions taken by computer player for in game testing. Does not have any AI or decision-making ability
    /// </summary>
    public void Play()
    {
        //check why draggable becomes active again when moving between zones, patched it with the disable every phase
        if (TurnManager.Instance.GetPhase() == TurnManager.Phase.OpponentDraw)
        {
            while (hand.GetComponent<DropZone>().ReachedMaxCards()==false &&hand.drawCount>0)
            {
                CardScriptableObject card = randomCardDB.randomDraw();

                hand.AddCardFromDeck(card, Origin.RandomDeck);
            }
            hand.GetComponent<DropZone>().DisableDrag();
            hand.GetComponent<DropZone>().TurnFaceDown();
        }
        if (TurnManager.Instance.GetPhase() == TurnManager.Phase.OpponentPlayBuff)
        {
            foreach (CardObject card in hand.GetComponent<DropZone>().GetList())
            {
                //this does not allow to switch out cards from buff zone - but AI doesn't switch out cards ATM anyway
                if (card.isPlayable && !transform.GetChild(3).GetComponent<DropZone>().ReachedMaxCards())
                {
                    transform.GetChild(3).GetComponent<DropZone>().AddCard(card);
                    transform.GetChild(3).GetComponent<DropZone>().DisableDrag();
                    transform.GetChild(3).GetComponent<DropZone>().TurnFaceUp();
                    hand.GetComponent<DropZone>().RemoveCard(card);
                    card.transform.SetParent(transform.GetChild(3).transform);
                    break;
                }
            }
        }
        if (TurnManager.Instance.GetPhase() == TurnManager.Phase.OpponentPlayCard)
        {
            List<CardObject> list = new List<CardObject>();
            List<CardObject> cardsToRemove = new List<CardObject>();
            foreach (CardObject card in hand.GetComponent<DropZone>().GetList())
            {
                if (card.isPlayable&& !transform.GetChild(1).GetComponent<DropZone>().ReachedMaxCards())
                {
                    Debug.Log("play card" + card.card.name);
                    cardsToRemove.Add(card);
                    transform.GetChild(1).GetComponent<DropZone>().AddCard(card);
                    transform.GetChild(1).GetComponent<DropZone>().DisableDrag();
                    transform.GetChild(1).GetComponent<DropZone>().TurnFaceUp();
                    card.transform.SetParent(transform.GetChild(1).transform);
                    MatchManager.Instance.GetComponent<IsCardPlayable>().UpdateIfCardCanBePlayed();
                }
            }
            foreach (CardObject card in cardsToRemove) 
            {
                hand.GetComponent<DropZone>().RemoveCard(card);
            }
        }
        if (TurnManager.Instance.GetPhase() == TurnManager.Phase.OpponentDiscard)
        {
            foreach (CardObject card in hand.GetComponent<DropZone>().GetList())
            {
                if (card.isPlayable)
                {
                    hand.GetComponent<DropZone>().RemoveCard(card);
                    GameObject.Find("Discard").GetComponent<DropZone>().AddCard(card);
                    GameObject.Find("Discard").GetComponent<DropZone>().DisableDrag();
                    GameObject.Find("Discard").GetComponent<DropZone>().TurnFaceUp();
                    card.transform.SetParent(GameObject.Find("Discard").transform);
                    break;
                }
            }
        }
    }
}
