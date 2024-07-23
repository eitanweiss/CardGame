using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// In charge of changing the duration of cards in game correctly after calculation phase
/// </summary>
public class DurationManager : MonoBehaviour
{
    //this will run after calculation of damage, before throwing out cards from areas
    public GameObject playArea;
    public GameObject activeArea;
    public GameObject buffArea;


    public void ReduceRoundCount()
    {
        //go through PlayArea
        var list = playArea.GetComponent<DropZone>().GetList();
        for (int j = 0; j < list.Count; j++)
        {
            CardObject card = list[j];
            for (int i = 0; i < card.card.abilities.Count; i++)
            {
                if (card.card.abilities[i].name == "Duration")
                {
                    //move it to active area
                    activeArea.GetComponent<DropZone>().AddCard(card);
                    playArea.GetComponent<DropZone>().RemoveCard(card);
                    card.transform.SetParent(activeArea.transform);
                    Debug.Log("moved");
                    card.gameObject.GetComponent<Clickable>().MoveTo(activeArea.transform.position);
                    break;
                }
            }
        }
        Debug.Log("removing all cards from playzone");
        playArea.GetComponent<DropZone>().RemoveAllCards();
        Debug.Log("all cards should be removed now");
        //go through BuffArea
        list = buffArea.GetComponent<DropZone>().GetList();
        for (int j = 0; j < list.Count; j++)
        {
            CardObject card = list[j];
            for (int i = 0; i < card.card.abilities.Count; i++)
            {
                if (card.card.abilities[i].name == "Duration")
                {
                    Image duration = buffArea.transform.GetChild(j).GetChild(2).GetComponent<Image>();
                    WriteBubble(card,duration);
                    break;
                }
            }
        }
        //go through ActiveCardsArea
        list = activeArea.GetComponent<DropZone>().GetList();
        for (int j = 0; j < list.Count; j++)
        {
            CardObject card = list[j];
            for (int i = 0; i < card.card.abilities.Count; i++)
            {
                if (card.card.abilities[i].name == "Duration")
                {
                    Debug.Log("this card was moved to active area" + card.card.name);
                    Image duration = activeArea.transform.GetChild(j).GetChild(2).GetComponent<Image>();
                    WriteBubble(card,duration);
                    break;
                }
            }
        }
    }
    public void WriteBubble(CardObject card,Image image)
    {
        image.gameObject.SetActive(true);
        string[] parts = image.GetComponentInChildren<TMP_Text>().text.Split('/');
        if (int.Parse(parts[0]) > 1)
        {
            image.GetComponentInChildren<TMP_Text>().text = $"{int.Parse(parts[0]) - 1}/{int.Parse(parts[1])}";//decreases duration by one
        }
        else
        {
            card.transform.GetComponentInParent<DropZone>().RemoveCard(card);
            Destroy(card.gameObject);
            //destroy card
        }
        //if(image.GetComponentInChildren<TMP_Text>().text)
    }
}
