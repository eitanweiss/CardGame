using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CardObject : MonoBehaviour
{
    public Card card;


    public CardObject() { }

    public CardObject (Card card)
    {
        this.card = card;
    }
}
