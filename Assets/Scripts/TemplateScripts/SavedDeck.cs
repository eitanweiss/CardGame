using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CardGame/SavedCardDB")]
public class SavedDeck : ScriptableObject
{
    [SerializeField]
    public List<CardScriptableObject> cards = new List<CardScriptableObject>();
    // Start is called before the first frame update
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
    public CardScriptableObject Draw()
    {
        CardScriptableObject card = cards[0];
        //Debug.Log(card.cardName);
        return card;
    }
}
