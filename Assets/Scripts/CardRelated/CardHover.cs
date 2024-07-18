using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.UIElements;

public class CardHover : MonoBehaviour,IPointerEnterHandler,IPointerExitHandler//, ISelectHandler,IDeselectHandler
{

    [SerializeField] float verticalMovement = 150f;
    //[SerializeField] float movetime = 0.1f;
    //Coroutine currentCoroutine;
    Vector3 originalPosition;
    Vector3 endPosition;
    public void OnPointerEnter(PointerEventData eventData)
    {
        if (transform.parent.name == "Hand")
        {
            originalPosition=transform.position;
            endPosition=transform.position +new Vector3(0f,verticalMovement,0f);
            eventData.selectedObject = gameObject;
            this.transform.position = new Vector2(transform.position.x,transform.position.y+ verticalMovement);
        }
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        if (transform.parent.name == "Hand")
        {
            eventData.selectedObject = null;
            this.transform.position = originalPosition;
        }
        //might be really heavy and not needed
        //problem it solves: when dragging card A back to hand over card B, mouse enters card B , and after release of card A
        // card A is reentered into hand layout, thereby resetting the position of cardB, so when mouse leaves card B
        // it moves down and out of line with other cards
        //problem it creates: if mouse enters another card immedietly it will not hover
        //LayoutRebuilder.MarkLayoutForRebuild(transform.parent.GetComponent<RectTransform>());   
    }

    //public void OnSelect(BaseEventData eventData)
    //{
    //    if(currentCoroutine != null)
    //    {
    //        StopCoroutine(currentCoroutine);
    //        transform.position = originalPosition;
    //    }
    //    currentCoroutine =StartCoroutine(MoveCard(true));
    //}

    //public void OnDeselect(BaseEventData eventData)
    //{
    //    if (currentCoroutine != null)
    //    {
    //        StopCoroutine(currentCoroutine);
    //        //transform.position = originalPosition;
    //    }
    //    currentCoroutine = StartCoroutine(MoveCard(false));
    //}
    //private IEnumerator MoveCard(bool startingMovement)
    //{        
    //    float elapsedTime = 0f;
    //    while(elapsedTime< movetime)
    //    {
    //        elapsedTime += Time.deltaTime;
    //        if (startingMovement)
    //        {
    //            endPosition = originalPosition +new Vector3(0f,verticalMovement,0f);
    //        }
    //        else
    //        {
    //            endPosition = originalPosition;
    //        }
    //        transform.position = Vector3.Lerp(transform.position, endPosition, elapsedTime / movetime);
        
    //        yield return null;
    //    }
    //}
}
