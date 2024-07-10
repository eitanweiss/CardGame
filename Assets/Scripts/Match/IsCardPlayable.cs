using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//class is in charge of checking which cards can be played in each phase.
//IMPORTANT: this does not do mana calculations, but rather the ManaManager does that.
//need to make sure they do not overturn each other
public class IsCardPlayable : MonoBehaviour
{    
    public Hand hand;
    public TurnManager turnManager;

    public void CanCardBePlayed()
    {
        if (turnManager.GetPhase() == TurnManager.Phase.PlayBuff)
        {
            Debug.Log("in playbuff playable phase");
            foreach (CardObject card in hand.GetComponent<DropZone>().handCards)
            {
                if (card.card.isBuffCard == true)
                {
                    card.GetComponent<Draggable>().enabled = true;
                }
                else
                {
                    card.GetComponent<Draggable>().enabled = false;
                }
            }
        }
        else if (turnManager.GetPhase() == TurnManager.Phase.PlayCard)
        {

            int mana = hand.GetComponentInParent<ManaManager>().GetMana();
            Debug.Log(mana);
            foreach (CardObject card in hand.GetComponent<DropZone>().handCards)
            {
                Debug.Log(card.name);
                if (card.card.isBuffCard == false)
                {
                    for(int i = 0; i < card.card.abilities.Count;i++)
                    {
                        if (card.card.abilities[i].name=="Cost")
                        {
                            if (card.card.abilityValues[i]<=mana)
                            {
                                card.GetComponent<Draggable>().enabled = true;
                            }
                            else
                            {
                                card.GetComponent<Draggable>().enabled = false;
                            }
                            break;
                        }
                    }
                }
                else
                {
                    card.GetComponent<Draggable>().enabled = false;
                }

            }
        }
        else if (turnManager.GetPhase() == TurnManager.Phase.Discard)
        {
            foreach (CardObject card in hand.GetComponent<DropZone>().handCards)
            {
                card.GetComponent<Draggable>().enabled = true;
            }
        }  
        else
        {
            foreach (CardObject card in hand.GetComponent<DropZone>().handCards)
            {
                card.GetComponent<Draggable>().enabled = false;
            }
        }
    }
}
