using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
//in charge of any and all changes to mana.
/* the following needs to happen
 * 1. At start of game mana is the basemana which is gotten from character
 * 2. At start of each round mana replenishes to full amount. This amount may vary in game as a result of card effects
 * 3. After playing a card mana goes down by that card's cost
 * 4. At calculation, if something affects mana, next round starts with different amount.
 */
public class ManaManager : MonoBehaviour
{
    public Image playerMana;
    public float baseMana = 50f;//this is a patch for now, need to change it to get info from player
    public float startOfTurnMana;
    private float currentMana;

    void Start()
    {
        currentMana = baseMana;  
        startOfTurnMana = baseMana;
        playerMana.GetComponentInChildren<TMP_Text>().text = currentMana.ToString();
        playerMana.GetComponentsInChildren<Image>()[1].fillAmount =1;
    }

    public void ResetMana()
    {
        currentMana = startOfTurnMana;
    }
    //might not be needed
    public int GetMana()
    {
        return (int)currentMana;
    }

    //if mana is affected by cards in game,
    //need to make sure the image is consistent as well for next round
    //happens ony in calculations so at the end of round need to refill mana
    public void SetMaxMana(int mana)
    {
        playerMana.GetComponentInChildren<TMP_Text>().text = (startOfTurnMana + mana).ToString();
        startOfTurnMana += mana;
        currentMana = startOfTurnMana;
    }

    //play or unplay card - does not affect start value(yet)
    public void ChangeMana(CardObject card, bool added)
    {
        for (int i = 0; i < card.card.abilities.Count; i++)
        {
            if (card.card.abilities[i].name == "Cost")
            {
                if(added)
                {
                    currentMana -= card.card.abilityValues[i];
                }
                else
                {
                    currentMana += card.card.abilityValues[i];
                }
                break;
            }
        }
        playerMana.GetComponentInChildren<TMP_Text>().text = currentMana.ToString();
        playerMana.GetComponentsInChildren<Image>()[1].fillAmount = currentMana / startOfTurnMana;

        //change which cards can be played with new mana amount
    }
}
