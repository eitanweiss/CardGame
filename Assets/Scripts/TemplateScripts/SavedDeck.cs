using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CardGame/SavedCardDB")]
public class SavedDeck : ScriptableObject
{
    [SerializeField]
    public List<CardScriptableObject> cards = new List<CardScriptableObject>();
    
    /// <summary>
    /// randomize the order of the cards in this deck
    /// </summary>
    public void Randomize()
    {

        CardScriptableObject temp;
        for (int i = 0; i < cards.Count; i++)
        {
            temp = cards[i];
            int num = Random.Range(0, cards.Count);
            cards[i] = cards[num];
            cards[num] = temp;

        }
    }
    /// <summary>
    /// randomize the order of the cards in the given deck
    /// </summary>
    /// <param name="list">deck of cards to randomize</param>
    private void MixList(List<CardScriptableObject> list)
    {
        CardScriptableObject temp;
        for (int i = 0; i < list.Count; i++)
        {
            temp = list[i];
            int num = Random.Range(0, list.Count);
            list[i] = list[num];
            list[num] = temp;
        }
    }

    /// <summary>
    /// draw a card
    /// </summary>
    /// <returns>the card drawn</returns>
    public CardScriptableObject Draw()
    {
        CardScriptableObject card = cards[0];
        //Debug.Log(card.cardName);
        return card;
    }
}
