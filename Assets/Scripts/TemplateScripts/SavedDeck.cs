using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "CardGame/SavedCardDB")]
public class SavedDeck : ScriptableObject
{
    [SerializeField]
    List<CardScriptableObject> cards = new List<CardScriptableObject>();
    
    public void StartUp()
    {
        cards.Clear();
        string path = Path.Combine(Application.dataPath, "Saved.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SerializableList<SerializableCard> loadedData = JsonUtility.FromJson<SerializableList<SerializableCard>>(json);
            foreach (var cardData in loadedData.list)
            {
                cards.Add(cardData.ToScriptableObject());
            }
        }
    }

    /// <summary>
    /// copy of card list
    /// </summary>
    /// <returns>a copy of the deck</returns>
    public List<CardScriptableObject> GetDeck()
    {
        List<CardScriptableObject> list = new List<CardScriptableObject>(cards);
        return list;
    }

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

    /// <summary>
    /// returns size of list
    /// </summary>
    /// <returns>number of cards in list</returns>
    public int listSize()
    {
        Debug.Log("list size is " + cards.Count);
        return cards.Count;
    }

    /// <summary>
    /// removes a card from the deck
    /// </summary>
    /// <param name="card">card to be removed</param>
    public void RemoveCard(CardScriptableObject card)
    {
       cards.Remove(card);
    }
}
