using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class PayMana : MonoBehaviour
{
    public Image playerMana;
    public Image oppMana;
    private int maxmanaval = 50;//this is a patch for now, ned to change it to get info from player

    public void DecreaseMana(CardObject cardObj)
    {
        Debug.Log("is this reached?");
        int val = int.Parse(playerMana.GetComponentInChildren<TMP_Text>().text);
        for (int i = 0; i < cardObj.card.abilities.Count; i++)
        {
            Debug.Log("DecreaseMana");
            if (cardObj.card.abilities[i].name == "Cost")
            {
                Debug.Log("Now");
                val = val - cardObj.card.abilityValues[i];
                break;
            }
        }
        playerMana.GetComponentInChildren<TMP_Text>().text = val.ToString();
    }

    public void IncreaseMana(CardObject cardObj)
    {
        Debug.Log("is this reached?");
        int val = int.Parse(playerMana.GetComponentInChildren<TMP_Text>().text);
        for (int i = 0; i < cardObj.card.abilities.Count; i++)
        {
            Debug.Log("DecreaseMana");
            if (cardObj.card.abilities[i].name == "Cost")
            {
                Debug.Log("Now");
                val = val + cardObj.card.abilityValues[i];
                break;
            }
        }
        playerMana.GetComponentInChildren<TMP_Text>().text = val.ToString();
    }
}
