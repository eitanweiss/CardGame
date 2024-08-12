using System;
using System.Collections.Generic;
using UnityEngine;
using static CardScriptableObject;

[Serializable]
public class SerializableCard 
{
    public int id;
    public string cardName;
    public string cardDescription;
    public List<int> abilityValues;
    public List<Ability> abilities;
    public Rarity rarity;
    public Race race;
    public int value;
    public Type type;
    public bool isBuffCard;
    public string spritePath;
    public Origin origin;
    public SerializableCard(CardScriptableObject card)
    {
        id = card.id;
        cardName = card.cardName;
        cardDescription = card.cardDescription;
        abilityValues = new List<int>(card.abilityValues);
        abilities = new List<Ability>(card.abilities);
        rarity = card.rarity;
        race = card.race;
        value = card.value;
        type = card.type;
        isBuffCard = card.isBuffCard;

        spritePath = card.image != null ? $"Sprites/Card/{card.image.name}" : null; // Store the relative path
        Debug.Log($"Serialized Sprite Path: {spritePath}"); // Debug log
    }

    public CardScriptableObject ToScriptableObject()
    {
        CardScriptableObject card = ScriptableObject.CreateInstance<CardScriptableObject>();
        card.id = id;
        card.cardName = cardName;
        card.cardDescription = cardDescription;
        card.abilityValues = new List<int>(abilityValues);
        card.abilities = new List<Ability>(abilities);
        card.rarity = rarity;
        card.race = race;
        card.value = value;
        card.type = type;
        card.isBuffCard = isBuffCard;
        Debug.Log($"Loading Sprite from Path: {spritePath}");

        card.image = !string.IsNullOrEmpty(spritePath) ? Resources.Load<Sprite>(spritePath) : null;

        return card;
    }
}

