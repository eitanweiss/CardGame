using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class CardHover : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler
{
    float hoveramount = 150f;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (transform.parent.name == "Hand")
        {
            //Debug.Log("entering card" + this.GetComponent<DisplayCard>().nameText.text);
            this.transform.position = new Vector2(transform.position.x,transform.position.y+ hoveramount);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (transform.parent.name == "Hand")
        {
            //Debug.Log("leaving card" + this.GetComponent<DisplayCard>().nameText.text);
            this.transform.position = new Vector2(transform.position.x, transform.position.y - hoveramount);
        }
        //might be really heavy and not needed
        //problem it solves: when dragging card A back to hand over card B, mouse enters card B , and after release of card A
        // card A is reentered into hand layout, thereby resetting the position of cardB, so when mouse leaves card B
        // it moves down and out of line with other cards
        //problem it creates: if mouse enters another card immedietly it will not hover
        LayoutRebuilder.MarkLayoutForRebuild(transform.parent.GetComponent<RectTransform>());   
    }
}
