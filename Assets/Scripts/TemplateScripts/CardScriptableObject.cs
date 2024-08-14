using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

//includes all the different aspects of the card, but not abilities
[CreateAssetMenu(menuName = "CardGame/Card", fileName = "New Card", order = 1)]
public class CardScriptableObject : ScriptableObject
{
    public int id;//Unique
    public string cardName;//name of card
    public string cardDescription;//flavor text + abilities
    public Sprite image;//image
    public List<Ability> abilities;//what the card does
    public List<int> abilityValues;//how much it does for every ability
    public bool isBuffCard;
    public enum Rarity { Common, Uncommon, Rare, Elite, Epic, Legendary, Mythical, Unique, Special, Relic, Heroic }//whatever
    public Rarity rarity; 
    public Type type; //Ranged,Fighter,Caster
    public Race race; //Human,Elf,Orc,Dwarf
    public int value;//how much card can be sold for from collection

    public void CopyValues(CardScriptableObject card)
    {
        id = card.id;
        cardName = card.cardName;
        cardDescription = card.cardDescription;
        abilityValues =  new List<int>(card.abilityValues);
        abilities = new List<Ability>(card.abilities);
        rarity = card.rarity;
        race = card.race;
        value = card.value;
        type = card.type;
        isBuffCard = card.isBuffCard;
        image = card.image;
    }
    public CardScriptableObject DeepCopy()
    {
        CardScriptableObject card = ScriptableObject.CreateInstance<CardScriptableObject>();
        card.CopyValues(this);
        return card;

    }
}