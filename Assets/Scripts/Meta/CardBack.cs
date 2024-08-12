using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CardBack : MonoBehaviour
{
    public void ShowBack()
    {
        if (gameObject.activeSelf)
        {
            this.gameObject.SetActive(false);
        }
        else
        {
            this.gameObject.SetActive(true);
        }
    }

}
