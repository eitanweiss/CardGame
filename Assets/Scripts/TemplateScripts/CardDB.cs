using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CardGame/CardDB")]
public class CardDB : ScriptableObject
{
    [SerializeField]
    public List<Card> cards = new List<Card>();

    //currently randomized the drawn card,
    //in future I want to draw the first but randomize it's value
    
    public void Randomize()
    {
        Card temp;
        for (int i = 0; i < cards.Count; i++)
        {
            temp = cards[i];
            int num = Random.Range(0, cards.Count);
            cards[i] = cards[num];
            cards[num] = temp;

        }
    }


    public Card Draw()
    {
        Card card = cards[0];
        Debug.Log(card.cardName);
        return card;
    }
}
