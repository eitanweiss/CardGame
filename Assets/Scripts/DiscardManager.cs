using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// will be in charge of discard effects: destroy or add to collection, depending on whether it was played
/// </summary>
public class DiscardManager : MonoBehaviour
{
    public void deleteCards()
    {
        var list = transform.GetComponent<DropZone>().GetList();
        for (int i = 0; i < list.Count; i++)
        {
            //if (list[i] wasplayed) => add to collection
            transform.GetComponent<DropZone>().RemoveCard(list[i]);
            Destroy(transform.GetChild(0).gameObject);
        }
    }
}
