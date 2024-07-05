using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class CardHover : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (transform.parent.name == "Hand")
        {
            Debug.Log("entering card");
            this.transform.position = new Vector2(transform.position.x,transform.position.y+150f);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (transform.parent.name == "Hand")
        {
            Debug.Log("leaving card");
            this.transform.position = new Vector2(transform.position.x, transform.position.y - 150f);
        }
    }
}
