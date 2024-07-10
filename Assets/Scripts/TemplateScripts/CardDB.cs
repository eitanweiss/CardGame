using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CardGame/randomCardDB")]
public class CardDB : ScriptableObject
{
    [SerializeField]
    public List<CardScriptableObject> cards = new List<CardScriptableObject>();
    
    public List<CardScriptableObject> SearchByRarity(CardScriptableObject.Rarity rarity)
    {
        List<CardScriptableObject> list  = new List<CardScriptableObject>();
        foreach (CardScriptableObject card in cards)
        {
            if(card.rarity == rarity)
            {
                list.Add(card);
            }
        }
        return list;
    }
    public List<CardScriptableObject> SearchByRace(Race race)
    {
        List<CardScriptableObject> list = new List<CardScriptableObject>();
        foreach (CardScriptableObject card in cards)
        {
            if (card.race== race || card.race== Race.All)
            {
                list.Add(card);
            }
        }
        return list;
    }
    public List<CardScriptableObject> SearchByType(Type type)
    {
        List<CardScriptableObject> list = new List<CardScriptableObject >();
        foreach (CardScriptableObject card in cards)
        {
            if (card.type== type || card.type == Type.All)
            {
                list.Add(card);
            }
        }
        return list;
    }

    public List<CardScriptableObject> SearchByAbility(Ability ability)
    {
        List<CardScriptableObject> list = new List<CardScriptableObject>();
        foreach (CardScriptableObject card in cards)
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
    float chanceCommon = 0.49f;
    float chanceRare = 0.33f;
    float chanceElite = 0.16f;
    float chanceEpic = 0.019f;
    //float chanceMythical = 0.001f;
    public CardScriptableObject randomDraw()
    {
        List<CardScriptableObject> list = new List<CardScriptableObject>();
        float val = Random.Range(0f, 1f);
        if (val <= chanceCommon)
        {
            list = SearchByRarity(CardScriptableObject.Rarity.Common);
        }
        else if (val <= chanceCommon + chanceRare)
        {
            list = SearchByRarity(CardScriptableObject.Rarity.Rare);
        }
        else if (val <= chanceCommon + chanceRare + chanceElite)
        {
            list = SearchByRarity(CardScriptableObject.Rarity.Elite);
        }
        else if (val <= chanceCommon + chanceRare + chanceElite + chanceEpic)
        {
            list = SearchByRarity(CardScriptableObject.Rarity.Epic);
        }
        else
        {
            list = SearchByRarity(CardScriptableObject.Rarity.Mythical);
        }
        return list[Random.Range(0, list.Count)];
    }
}
