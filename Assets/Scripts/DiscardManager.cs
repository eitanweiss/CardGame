using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DiscardManager : MonoBehaviour
{
    public void deleteCards()
    {
        transform.GetComponent<DropZone>().handCards.Clear();
    }
}
