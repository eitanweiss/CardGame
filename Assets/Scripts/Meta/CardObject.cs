using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Wrapper class for CardScriptableObject
/// contains the card and 2 other fields:
/// @param isPlayable - if card is playable in game or not
/// @param origin - if card originated from saved deck or from random deck
/// </summary>
public class CardObject : MonoBehaviour
{
    public CardScriptableObject card;
    public bool isPlayable;
    public Origin origin;
}
