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
    private Transform SavedDeckViewContent;

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

    /// <summary>
    /// 
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
        foreach(CardScriptableObject card in savedDeck)
        {
            GameObject cardGO = Instantiate(prefab, SavedDeckViewContent);
            CardObject newCardObj = cardGO.AddComponent<CardObject>();
            newCardObj.card = card;
            DisplayCard displayCard = cardGO.GetComponent<DisplayCard>();
            displayCard.SetCard(card);
            Debug.Log("Card up");
        }
        Debug.Log("All Cards are up");
    }

    /// <summary>
    /// 
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
            Debug.Log("Card up");
        }
        Debug.Log("All Cards are up");
    }
    //when clicked - move from one to the other
    //limit the number of cards in the saved deck according to 
}
