using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class EffectManager : MonoBehaviour
{
    //want to have different stages of calculating:
    // 1. decrease mana immideatly when card is played
    // 3. add player stats to calculations before any cards are resolved
    // 2. resolve effect of cards and combos seperately and consecutively, accumulate effect.
    public Image playerMana;
    public Image oppMana;

    public void decreaseMana(CardScriptableObject card)
    {
        Debug.Log("is this reached?");
        int val = int.Parse(playerMana.GetComponent<TMP_Text>().text);
        for(int i = 0; i < card.abilities.Count; i++)
        {
            Debug.Log("DecreaseMana");
            if (card.abilities[i].name == "Cost")
            {
                Debug.Log("Now");
                val = val - card.abilityValues[i];
            }
        }
    }
}
