using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "CardGame/CardDB")]
public class CardDB : ScriptableObject
{
    [SerializeField]
    public List<Card> cards = new List<Card>();
    
    public List<Card> SearchByRarity(Card.Rarity rarity)
    {
        List<Card> list  = new List<Card>();
        foreach (Card card in cards)
        {
            if(card.rarity == rarity)
            {
                list.Add(card);
            }
        }
        return list;
    }
    public List<Card> SearchByRace(Race race)
    {
        List<Card> list = new List<Card>();
        foreach (Card card in cards)
        {
            if (card.race== race || card.race== Race.All)
            {
                list.Add(card);
            }
        }
        return list;
    }
    public List<Card> SearchByType(Type type)
    {
        List<Card> list = new List<Card>();
        foreach (Card card in cards)
        {
            if (card.type== type || card.type == Type.All)
            {
                list.Add(card);
            }
        }
        return list;
    }

    public List<Card> SearchByAbility(Ability ability)
    {
        List<Card> list = new List<Card>();
        foreach (Card card in cards)
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
    public Card randomDraw()
    {
        List<Card> list = new List<Card>();
        float val = Random.Range(0f, 1f);
        if(val<=chanceCommon)
        {
            list = SearchByRarity(Card.Rarity.Common);
        }
        else if (val <= chanceCommon +chanceRare)
        {
            list = SearchByRarity(Card.Rarity.Rare);
        }
        else if (val <= chanceCommon+ chanceRare +chanceElite)
        {
            list = SearchByRarity(Card.Rarity.Elite);
        }
        else if (val <= chanceCommon + chanceRare + chanceElite + chanceEpic)
        {
            list = SearchByRarity(Card.Rarity.Epic);
        }
        else 
        {
            list = SearchByRarity(Card.Rarity.Mythical);
        }
        return list[Random.Range(0, list.Count)];
    }
    public Card Draw()
    {
        Card card = cards[0];
        //Debug.Log(card.cardName);
        return card;
    }
}
