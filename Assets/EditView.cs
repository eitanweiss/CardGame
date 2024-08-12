using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class EditView : MonoBehaviour
{
    [SerializeField]
    private Transform collectionViewContent;
    [SerializeField]
    private Transform savedDeckViewContent;

    [SerializeField]
    private GameObject prefab;

    List<CardScriptableObject> collection;
    List<CardScriptableObject> savedDeck;

    // Start is called before the first frame update
    void Start()
    {
        LoadCollection();
        LoadSavedDeck();
        //load collection from json file and fill in the colelction view
        //load saved deck from json
    }

    public void Restart()
    {
        //empty children from content view
        for(int i=collectionViewContent.childCount-1;i>=0 ;i--)
        {
            Destroy(collectionViewContent.GetChild(i).gameObject);
        }
        for(int i=savedDeckViewContent.childCount-1;i>=0 ;i--)
        {
            Destroy(savedDeckViewContent.GetChild (i).gameObject);
        }
        LoadCollection();
        LoadSavedDeck();
    }

    public void SaveCollection()
    {
        List<SerializableCard> serializedCards= new List<SerializableCard>();
        foreach(var card in collection)
        {
            serializedCards.Add(new SerializableCard(card));
        }
        string json = JsonUtility.ToJson(new SerializableList<SerializableCard>(serializedCards), true);
        string path = Path.Combine(Application.dataPath, "Collection.json");
        File.WriteAllText(path, json);
    }

    /// <summary>
    /// load the current collection
    /// </summary>
    private void LoadCollection()
    {
        collection = new List<CardScriptableObject>();
        string path = Path.Combine(Application.dataPath, "Collection.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SerializableList<SerializableCard> loadedData = JsonUtility.FromJson<SerializableList<SerializableCard>>(json);
            foreach (var cardData in loadedData.list)
            {
                if (!collection.Exists(c => c.id == cardData.id))
                {
                    collection.Add(cardData.ToScriptableObject());
                }
            }
        }
        foreach (CardScriptableObject card in collection)
        {
            GameObject cardGO = Instantiate(prefab, collectionViewContent);
            CardObject newCardObj = cardGO.AddComponent<CardObject>();
            newCardObj.card = card;
            DisplayCard displayCard = cardGO.GetComponent<DisplayCard>();
            displayCard.SetCard(card);
            cardGO.AddComponent<ViewClick>();
            Destroy(cardGO.GetComponent<Clickable>());
            Debug.Log("Card up");
        }
        Debug.Log("All Cards are up");
    }
    //when clicked - move from one to the other

    /// <summary>
    /// add CardObject card to collection
    /// </summary>
    /// <param name="card">card to add</param>
    public void AddToCollection(CardObject card)
    {
        collection.Add(card.card);
    }

    /// <summary>
    /// remove CardScriptableObject card from colleciton
    /// </summary>
    /// <param name="card">card to be removed from collection</param>
    public void RemoveFromCollection(CardObject card)
    {
        collection.Remove(card.card);
    }

    /// <summary>
    /// get the transform of collection content
    /// </summary>
    /// <returns> transform of collection content</returns>
    public Transform GetCollectionTransform()
    {
        return collectionViewContent;
    }

    /// <summary>
    /// add CardObject card to savedDeck
    /// </summary>
    /// <param name="card">card to add</param>
    public void AddToSaved(CardObject card)
    {
        savedDeck.Add(card.card);
    }

    /// <summary>
    /// remove card from saved deck
    /// </summary>
    /// <param name="card">card to be removed from collection</param>
    public void RemoveFromSaved(CardObject card)
    {
        savedDeck.Remove(card.card);
    }

    public void SaveSavedDeck()
    {
        List<SerializableCard> serializedCards = new List<SerializableCard>();
        foreach (var card in savedDeck)
        {
            serializedCards.Add(new SerializableCard(card));
        }
        string json = JsonUtility.ToJson(new SerializableList<SerializableCard>(serializedCards), true);
        string path = Path.Combine(Application.dataPath, "Saved.json");
        File.WriteAllText(path, json);
    }

    /// <summary>
    /// load the current saved deck
    /// </summary>
    private void LoadSavedDeck()
    {
        savedDeck = new List<CardScriptableObject>();
        string path = Path.Combine(Application.dataPath, "Saved.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SerializableList<SerializableCard> loadedData = JsonUtility.FromJson<SerializableList<SerializableCard>>(json);
            foreach (var cardData in loadedData.list)
            {
                savedDeck.Add(cardData.ToScriptableObject());
            }
        }
        foreach (CardScriptableObject card in savedDeck)
        {
            GameObject cardGO = Instantiate(prefab, savedDeckViewContent);
            CardObject newCardObj = cardGO.AddComponent<CardObject>();
            newCardObj.card = card;
            DisplayCard displayCard = cardGO.GetComponent<DisplayCard>();
            displayCard.SetCard(card);
            cardGO.AddComponent<ViewClick>();
            Destroy(cardGO.GetComponent<Clickable>());
            Debug.Log("Card up");
        }
        Debug.Log("All Cards are up");
    }

    /// <summary>
    /// get the transform of savedDeck content
    /// </summary>
    /// <returns>transform of savedDeck content</returns>
    public Transform GetSavedTransform()
    {
        return savedDeckViewContent;
    }
    //limit the number of cards in the saved deck according to - higher order not important ATM
}
