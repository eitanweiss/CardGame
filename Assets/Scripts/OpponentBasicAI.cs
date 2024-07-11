using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR;

public class OpponentBasicAI : MonoBehaviour
{
    public TurnManager turnManager;
    public CardDB randomCardDB;
    public Hand hand;
    // Start is called before the first frame update
    public void Play()
    {
        if (turnManager.GetPhase() == TurnManager.Phase.OpponentDraw)
        {
            while (hand.GetComponent<DropZone>().availablePlayerHandCardSlots > 0)
            {
                CardScriptableObject card = randomCardDB.randomDraw();

                hand.AddCardFromDeck(card);
            }
        }
        if (turnManager.GetPhase() == TurnManager.Phase.OpponentPlayBuff)
        {
            foreach (CardObject card in hand.GetComponent<DropZone>().handCards)
            {
                if (card.GetComponent<Draggable>().enabled)
                {
                    transform.GetChild(3).GetComponent<DropZone>().AddCard(card);
                    hand.GetComponent<DropZone>().RemoveCard(card);
                    card.transform.SetParent(transform.GetChild(3).transform);
                    break;
                }
            }
        }
        if (turnManager.GetPhase() == TurnManager.Phase.OpponentPlayCard)
        {
            List<CardObject> list = new List<CardObject>();
            foreach (CardObject card in hand.GetComponent<DropZone>().handCards)
            {
                if (card.GetComponent<Draggable>().enabled)
                {
                    transform.GetChild(1).GetComponent<DropZone>().AddCard(card);
                    hand.GetComponent<DropZone>().RemoveCard(card);
                    card.transform.SetParent(transform.GetChild(1).transform);
                }
            }
        }
        if (turnManager.GetPhase() == TurnManager.Phase.OpponentDiscard)
        {
            foreach (CardObject card in hand.GetComponent<DropZone>().handCards)
            {
                if (card.GetComponent<Draggable>().enabled)
                {
                    hand.GetComponent<DropZone>().RemoveCard(card);
                    GameObject.Find("Discard").GetComponent<DropZone>().AddCard(card);
                    card.transform.SetParent(GameObject.Find("Discard").transform);
                    break;
                }
            }
        }
    }
}
