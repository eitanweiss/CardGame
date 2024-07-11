using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DurationManager : MonoBehaviour
{
    //this will run after calculation of damage, before throwing out cards from areas
    public GameObject playArea;
    public GameObject activeArea;
    public GameObject buffArea;


    public void ReduceRoundCount()
    {
        //go through PlayArea
        for (int j = 0; j < buffArea.GetComponent<DropZone>().handCards.Count; j++)
        {
            CardObject card = buffArea.GetComponent<DropZone>().handCards[j];
            for (int i = 0; i < card.card.abilities.Count; i++)
            {
                if (card.card.abilities[i].name == "Duration")
                {
                    //move it to active area
                    activeArea.GetComponent<DropZone>().AddCard(card);
                    Image duration = playArea.transform.GetChild(j).GetChild(3).GetComponent<Image>();
                    WriteBubble(duration);
                    break;
                }
            }
        }
        playArea.GetComponent<DropZone>().RemoveAllCards();
        //go through BuffArea
        for (int j = 0; j < buffArea.GetComponent<DropZone>().handCards.Count; j++)
        {
            CardObject card = buffArea.GetComponent<DropZone>().handCards[j];
            for (int i = 0; i < card.card.abilities.Count; i++)
            {
                if (card.card.abilities[i].name == "Duration")
                {
                    Image duration = buffArea.transform.GetChild(j).GetChild(3).GetComponent<Image>();
                    WriteBubble(duration);
                    break;
                }
            }
        }
        //go through ActiveCardsArea
        for (int j = 0; j < buffArea.GetComponent<DropZone>().handCards.Count; j++)
        {
            CardObject card = buffArea.GetComponent<DropZone>().handCards[j];
            for (int i = 0; i < card.card.abilities.Count; i++)
            {
                if (card.card.abilities[i].name == "Duration")
                {
                    Image duration = activeArea.transform.GetChild(j).GetChild(3).GetComponent<Image>();
                    WriteBubble(duration);
                    break;
                }
            }
        }
    }
    public void WriteBubble(Image image)
    {
        image.enabled = true;
        string[] parts = image.GetComponentInChildren<TMP_Text>().text.Split('/');
        image.GetComponentInChildren<TMP_Text>().text = $"{int.Parse(parts[0]) - 1}/{int.Parse(parts[1])}";//decreases duration by one
        //if(image.GetComponentInChildren<TMP_Text>().text)
    }
}
