using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CardGame/SavedCardDB")]
public class SavedDeck : ScriptableObject
{
    [SerializeField]
    public List<Card> cards = new List<Card>();
    // Start is called before the first frame update
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
    private void MixList(List<Card> list)
    {
        Card temp;
        for (int i = 0; i < list.Count; i++)
        {
            temp = list[i];
            int num = Random.Range(0, list.Count);
            list[i] = list[num];
            list[num] = temp;
        }
    }
    public Card Draw()
    {
        Card card = cards[0];
        //Debug.Log(card.cardName);
        return card;
    }
}
