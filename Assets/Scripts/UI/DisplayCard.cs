using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
public class DisplayCard : MonoBehaviour
{
    //public int id;
    public TMP_Text nameText;
    public TMP_Text descriptionText;
    public TMP_Text rarityText;
    public TMP_Text raceText;
    // Start is called before the first frame update


    public void SetCard(CardScriptableObject card)
    {
     
        nameText.text = " " +card.cardName;
        descriptionText.text = "" + card.cardDescription + ";\n";
        raceText.text = " " + card.race;
        rarityText.text = " " + card.rarity; 
        for (int i = 0;i<card.abilities.Count;i++)
        {
            if (card.abilities[i].name=="Duration")
            {
                transform.GetChild(2).GetComponent<Image>().GetComponentInChildren<TMP_Text>().text = $"{card.abilityValues[i]}/{card.abilityValues[i]}";
            }
            descriptionText.text = descriptionText.text + card.abilities[i].name + "(" + card.abilityValues[i] +"); ";

        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
