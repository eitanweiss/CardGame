using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CardGame/randomCardDB")]


/// <summary>
/// contains all the cards known to man(or orc).
/// contains methods for searching and limiting via each criterea,
/// </summary>
public class CardDB : ScriptableObject
{

    //[SerializeField] Race race;
    //[SerializeField] Type type;
    [SerializeField] public List<CardScriptableObject> allCards = new List<CardScriptableObject>();
    public int drawCount;

    public void SetDrawCount(int num)
    {
        drawCount = num;
    }

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
        return list[Random.Range(0, list.Count)];
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
        foreach (CardScriptableObject card in allCards)
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
        foreach (CardScriptableObject card in allCards)
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
    float chanceCommon = 0.49f;
    float chanceRare = 0.33f;
    float chanceElite = 0.16f;
    float chanceEpic = 0.019f;
    //float chanceMythical = 0.001f;
    public CardScriptableObject randomDraw()
    {
        CardScriptableObject card;
        float val = Random.Range(0f, 1f);
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
