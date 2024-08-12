using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ViewClick : MonoBehaviour, IPointerClickHandler
{
    /// <summary>
    /// click event handler
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        GameObject editView = transform.parent.parent.parent.parent.gameObject;
        if(transform.parent.name == "CollectionContent")
        {
            //move to Saved/sell cards
            //remove from collection
            Debug.Log("parent is collection");
            editView.GetComponent<EditView>().RemoveFromCollection(eventData.pointerClick.GetComponent<CardObject>());
            //move to saved
            transform.SetParent(editView.GetComponent<EditView>().GetSavedTransform());
            //add to saved
            editView.GetComponent<EditView>().AddToSaved(eventData.pointerClick.GetComponent<CardObject>()); 
        }
        else
        {
            //move to collection
            editView.GetComponent<EditView>().RemoveFromSaved(eventData.pointerClick.GetComponent<CardObject>());
            //move to saved
            transform.SetParent(editView.GetComponent<EditView>().GetCollectionTransform());
            //add to saved
            editView.GetComponent<EditView>().AddToCollection(eventData.pointerClick.GetComponent<CardObject>());
        }
    }
}
