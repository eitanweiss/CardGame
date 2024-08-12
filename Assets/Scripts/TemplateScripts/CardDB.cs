using System;
using System.Collections.Generic;
using System.IO;
using Unity.VisualScripting;
using UnityEngine;

[CreateAssetMenu(menuName = "CardGame/randomCardDB")]


/// <summary>
/// contains all the cards known to man(or orc).
/// contains methods for searching and limiting via each criterea: race,type,rarity.
/// may later contain methods to search by ability.
/// </summary>
public class CardDB : ScriptableObject
{
    [SerializeField] public List<CardScriptableObject> allCards = new List<CardScriptableObject>();
    public List<SerializableCard> runTimeCards= new List<SerializableCard>();
    public int drawCount;

    /// <summary>
    /// saves cards to the player collection. called every time a card is used in a match
    /// </summary>
    public void SaveRuntimeChanges()
    {
        string json = JsonUtility.ToJson(new SerializableList<SerializableCard>(runTimeCards), true);
        string path = Path.Combine(Application.dataPath, $"{this.name}.json");
        File.WriteAllText(path, json);
    }

    /// <summary>
    /// needed for the collection part of player data. does not have any use in game or match. not sure when to call yet.
    /// </summary>
    public void LoadRuntimeChanges()
    {
        string path = Path.Combine(Application.dataPath, $"{this.name}.json");
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SerializableList<SerializableCard> loadedData = JsonUtility.FromJson<SerializableList<SerializableCard>>(json);
            foreach (var cardData in loadedData.list)
            {
                if (!allCards.Exists(c => c.id == cardData.id))
                {
                    allCards.Add(cardData.ToScriptableObject());//pointless may need to depercate
                    runTimeCards.Add(cardData);
                }
            }
        }
    }

    /// <summary>
    /// sets how many cards can be drawn next round.
    /// </summary>
    /// <param name="num">number of cards that can be drawn</param>
    public void SetDrawCount(int num)
    {
        drawCount = num;
    }

    /// <summary>
    /// searches in the card collection and creates a list of all cards with Rarity rarity.
    /// </summary>
    /// <param name="rarity">rarity of the cards in the returned list</param>
    /// <returns>a random card with Rarity rarity</returns>
    public CardScriptableObject SearchByRarity(CardScriptableObject.Rarity rarity)
    {
        List<CardScriptableObject> list  = new List<CardScriptableObject>();
        foreach (CardScriptableObject card in allCards)
        {
            if(card.rarity == rarity)
            {
                list.Add(card);
            }
        }
        return list[UnityEngine.Random.Range(0, list.Count)];
    }

    /// <summary>
    /// removes all cards from list that are not of Rarity rarity.
    /// </summary>
    /// <param name="list">list to remove cards from</param>
    /// <param name="rarity">rarity to retain in the list</param>
    public void LimitByRarity(List<CardScriptableObject> list, CardScriptableObject.Rarity rarity)
    {
        for (int i = list.Count - 1; i >= 0; i--)
        {
            if (list[i].rarity != rarity)
            {
                list.Remove(list[i]);
            }
        }
    }

    /// <summary>
    /// searches in the card collection and creates a list of all cards with Race race.
    /// </summary>
    /// <param name="race"> race of the cards in the returned list</param>
    /// <returns> a List<CardScriptableObject> of all cards that are of Race race or Race All </returns>
    public List<CardScriptableObject> SearchByRace(Race race)
    {
        List<CardScriptableObject> list = new List<CardScriptableObject>();
        foreach (CardScriptableObject card in allCards)
        {
            if (card.race== race || card.race== Race.All)
            {
                list.Add(card);
            }
        }
        return list;
    }

    /// <summary>
    /// removes all cards from list that are not of Race race.
    /// </summary>
    /// <param name="list">list to remove cards from</param>
    /// <param name="race">race to retain in list</param>
    public void LimitByRace(List<CardScriptableObject> list, Race race) 
    {
        for (int i = list.Count-1; i >=0 ; i--)
        {
            if (list[i].race != race && list[i].race != Race.All)
            {
                list.Remove(list[i]);
            }
        }
    }

    /// <summary>
    /// searches in the card collection and creates a list of all cards with Type type.
    /// </summary>
    /// <param name="type">type of the cards in the returned list</param>
    /// <returns>a List<CardScriptableObject> of cards that are of Type type or Type All.</returns>
    public List<CardScriptableObject> SearchByType(Type type)
    {
        List<CardScriptableObject> list = new List<CardScriptableObject >();
        foreach (CardScriptableObject card in allCards)
        {
            if (card.type== type || card.type == Type.All)
            {
                list.Add(card);
            }
        }
        return list;
    }

    /// <summary>
    /// removes all cards from list that are not of Type type.
    /// </summary>
    /// <param name="list">list to remove cards from</param>
    /// <param name="type">type to retain in list</param>
    public void LimitByType(List<CardScriptableObject> list, Type type)
    {
        for (int i = list.Count - 1; i >= 0; i--)
        {
            if (list[i].type != type && list[i].type != Type.All)
            {
                list.Remove(list[i]);
            }
        }
    }

    /// <summary>
    /// searches in the card collection and creates a list of all cards with the Ability ability.
    /// </summary>
    /// <param name="ability">abilityto search by</param>
    /// <returns>a list of all cards that have the Ability ability.</returns>
    public List<CardScriptableObject> SearchByAbility(Ability ability)
    {
        List<CardScriptableObject> list = new List<CardScriptableObject>();
        foreach (CardScriptableObject card in allCards)
        {
            foreach (Ability cardAbility in card.abilities)
            {
                if (cardAbility== ability)
                {
                    list.Add(card);
                }
            } 
        }
        return list;
    }

    //currently randomized the drawn card,
    //in future I want to draw the first but randomize it's value
    float chanceCommon = 0.55f;
    float chanceRare = 0.33f;
    float chanceElite = 0.10f;
    float chanceEpic = 0.019f;
    //float chanceMythical = 0.001f;

    /// <summary>
    /// returns a random card out of all possible cards.
    /// </summary>
    /// <returns>a random card drawn.</returns>
    public CardScriptableObject randomDraw()
    {
        CardScriptableObject card;
        float val = UnityEngine.Random.Range(0f, 1f);
        if (val <= chanceCommon)
        {
            card = SearchByRarity(CardScriptableObject.Rarity.Common);
            Debug.Log("draw common");
        }
        else if (val <= chanceCommon + chanceRare)
        {
            card = SearchByRarity(CardScriptableObject.Rarity.Rare);
            Debug.Log("draw rare");
        }
        else if (val <= chanceCommon + chanceRare + chanceElite)
        {
            card = SearchByRarity(CardScriptableObject.Rarity.Elite);
            Debug.Log("draw elite");
        }
        else if (val <= chanceCommon + chanceRare + chanceElite + chanceEpic)
        {
            card = SearchByRarity(CardScriptableObject.Rarity.Epic);
            Debug.Log("draw epic");
        }
        else
        {
            card = SearchByRarity(CardScriptableObject.Rarity.Mythical);
            Debug.Log("draw Mythical");
        }
        //after getting all cards limit them to type and race of user
        return card;
    }
}

/// <summary>
/// Helper class to serialize List
/// </summary>
/// <typeparam name="T">type of element</typeparam>
[Serializable]
public class SerializableList<T>
{
    public List<T> list;
    public SerializableList(List<T> list) => this.list = list;
}
