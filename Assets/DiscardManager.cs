using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DiscardManager : MonoBehaviour
{
    public void deleteCards()
    {
        int len = transform.GetComponent<DropZone>().handCards.Count;
        for (int i = 0; i<len;i++)
        {
            transform.GetComponent<DropZone>().RemoveCard(transform.GetComponent<DropZone>().handCards[0]);
            Destroy(transform.GetChild(i).gameObject);
        }
    }
}
