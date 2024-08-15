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
    public GameObject playerPlayArea;
    public GameObject playerActiveArea;
    public GameObject playerBuffArea;
    public GameObject oppPlayArea;
    public GameObject oppActiveArea;
    public GameObject oppBuffArea;


    /// <summary>
    /// checks which cards have duration and moves them to active zone, removes those that do not
    /// </summary>
    /// <param name="playArea">relevant play area</param>
    /// <param name="activeArea">relevant active area</param>
    void ReducePlayCount(GameObject playArea,GameObject activeArea)
    {
        var list = playArea.GetComponent<DropZone>().GetList();
        for (int j = list.Count - 1; j >= 0; j--)
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

    }

    /// <summary>
    /// reduces the duration of all cards in the buff area
    /// </summary>
    /// <param name="buffArea">relevant buff area</param>
    void ReduceBuffCount(GameObject buffArea)
    {
        var list = buffArea.GetComponent<DropZone>().GetList();
        for (int j = list.Count - 1; j >= 0; j--)
        {
            CardObject card = list[j];
            for (int i = 0; i < card.card.abilities.Count; i++)
            {
                if (card.card.abilities[i].name == "Duration")
                {
                    Image duration = buffArea.transform.GetChild(j).GetChild(2).GetComponent<Image>();
                    WriteBubble(card, duration);
                    break;
                }
            }
        }
    }

    /// <summary>
    /// reduces the duration of all cards in the active area
    /// </summary>
    /// <param name="activeArea">relevant active area</param>
    void ReduceActiveCount(GameObject activeArea)
    {
        var list = activeArea.GetComponent<DropZone>().GetList();
        for (int j = list.Count-1; j >=0; j--)
        {
            CardObject card = list[j];
            for (int i = 0; i < card.card.abilities.Count; i++)
            {
                if (card.card.abilities[i].name == "Duration")
                {
                    Debug.Log("this card was moved to active area" + card.card.name);
                    Image duration = activeArea.transform.GetChild(j).GetChild(2).GetComponent<Image>();
                    WriteBubble(card, duration);
                    break;
                }
            }
        }
    }

    /// <summary>
    /// reorganizes all cards in the correct areas for the next round, reducing all durations accordingly.
    /// </summary>
    public void ReduceRoundCount()
    {
        ///player zones
        //go through PlayArea
        ReducePlayCount(playerPlayArea, playerActiveArea);
        //go through BuffArea
        ReduceBuffCount(playerBuffArea);
        //go through ActiveCardsArea
        ReduceActiveCount(playerActiveArea);

        ///opponent zones
        //go through PlayArea
        ReducePlayCount(oppPlayArea, oppActiveArea);
        //go through BuffArea
        ReduceBuffCount(oppBuffArea);
        //go through ActiveCardsArea
        ReduceActiveCount(oppActiveArea);
    }

    /// <summary>
    /// Updates the duration bubble of all cards in the game when called by the ReduceRoundCount method.
    /// </summary>
    /// <param name="card">card who's duration is decreased </param>
    /// <param name="image">image containing the text that needs to change</param>
    public void WriteBubble(CardObject card,Image image)
    {
        image.gameObject.SetActive(true);
        string[] parts = image.GetComponentInChildren<TMP_Text>().text.Split('/');
        if (int.Parse(parts[0]) > 1)
        {   //decreases duration by one
            image.GetComponentInChildren<TMP_Text>().text = $"{int.Parse(parts[0]) - 1}/{int.Parse(parts[1])}";
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
