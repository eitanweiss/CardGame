using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
public class ManaManager : MonoBehaviour
{
    public Image playerMana;
    private float maxmanaval = 50f;//this is a patch for now, ned to change it to get info from player

    public int GetMana()
    {
        return int.Parse(playerMana.GetComponentInChildren<TMP_Text>().text);
    }
    public void SetMaxMana(int mana)
    {
        playerMana.GetComponentInChildren<TMP_Text>().text = mana.ToString();
    }

    public void DecreaseMana(CardObject cardObj)
    {
        int val = int.Parse(playerMana.GetComponentInChildren<TMP_Text>().text);
        for (int i = 0; i < cardObj.card.abilities.Count; i++)
        {
            //Debug.Log("DecreaseMana");
            if (cardObj.card.abilities[i].name == "Cost")
            {
                //Debug.Log("Now");
                val = val - cardObj.card.abilityValues[i];
                break;
            }
        }
        playerMana.GetComponentInChildren<TMP_Text>().text = val.ToString();
        playerMana.GetComponentsInChildren<Image>()[1].fillAmount = val/maxmanaval;
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
        playerMana.GetComponentsInChildren<Image>()[1].fillAmount = val / maxmanaval;
    }
}
