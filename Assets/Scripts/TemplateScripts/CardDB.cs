using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CardGame/randomCardDB")]

/*  
 * this contains all the cards known to man(or orc)
 * it contains methods for searching and limiting via each criterea,
 */



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
        //after getting all cards limit them to type and race of user
        return list[Random.Range(0, list.Count)];
    }
}
