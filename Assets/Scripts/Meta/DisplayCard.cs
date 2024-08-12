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
    public Image cardImage;
    public Image border;
    // Start is called before the first frame update


    public void SetCard(CardScriptableObject card)
    {
     
        nameText.text = " " +card.cardName;
        descriptionText.text = "" + card.cardDescription + ";\n";
        FillRaceText(card.race,card.type);
        rarityText.text = " " + card.rarity; 
        for (int i = 0;i<card.abilities.Count;i++)
        {
            if (card.abilities[i].name=="Duration")
            {
                transform.GetChild(2).GetComponent<Image>().GetComponentInChildren<TMP_Text>().text = $"{card.abilityValues[i]}/{card.abilityValues[i]}";
            }
            descriptionText.text = descriptionText.text + card.abilities[i].name + "(" + card.abilityValues[i] +"); ";

        }
        cardImage.sprite = card.image;
        border.sprite = card.image;
    }
    public void FillRaceText(Race race, Type type)
    {
        switch (race)
        {
            case Race.Orc:
                switch (type)
                {
                    case Type.Ranged:
                        raceText.text = "Huntsman";
                        break;
                    case Type.Fighter:
                        raceText.text = "Warrior";
                        break;
                    case Type.Caster:
                        raceText.text = "Shaman";
                        break;
                    case Type.All:
                        raceText.text = "Orc";
                        break;
                }
                break;
            case Race.Elf:
                switch (type)
                {
                    case Type.Ranged:
                        raceText.text = "Ranger";
                        break;
                    case Type.Fighter:
                        raceText.text = "Guard";
                        break;
                    case Type.Caster:
                        raceText.text = "Druid";
                        break;
                    case Type.All:
                        raceText.text = "Elf";
                        break;
                }
                break;
            case Race.Human:
                switch (type)
                {
                    case Type.Ranged:
                        raceText.text = "Rogue";
                        break;
                    case Type.Fighter:
                        raceText.text = "Swordsman";
                        break;
                    case Type.Caster:
                        raceText.text = "Mage";
                        break;
                    case Type.All:
                        raceText.text = "Human";
                        break;
                }
                break;
            case Race.All:
                switch (type)
                {
                    case Type.Ranged:
                        raceText.text = "Ranged";
                        break;
                    case Type.Fighter:
                        raceText.text = "Melee";
                        break;
                    case Type.Caster:
                        raceText.text = "Caster";
                        break;
                    case Type.All:
                        raceText.text = "";
                        break;
                }
                break;
            default:
                Debug.LogError("Invalid race");
                break;
        }
    }



    // Update is called once per frame
    void Update()
    {

    }
}
