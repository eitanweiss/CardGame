using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardObject : MonoBehaviour
{
    public CardScriptableObject card;
    public bool isPlayable;

    public CardObject() { }

    public CardObject (CardScriptableObject card)
    {
        this.card = card;
        isPlayable = true;
    }
}
