using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//class is in charge of checking which cards can be played in each phase.
//IMPORTANT: this does not do mana calculations, but rather the Mana does that.
//need to make sure they do not overturn each other
public class IsCardPlayable : MonoBehaviour
{    
    public Hand hand;
    public Hand oppHand;
    public TurnManager turnManager;

    /// <summary>
    /// sets which cards can be played at every phase
    /// </summary>
    public void UpdateIfCardCanBePlayed()
    {
        if (turnManager.GetPhase() == TurnManager.Phase.PlayBuff)
        {
            //Debug.Log("in playbuff playable phase");

            foreach (CardObject card in hand.GetComponent<DropZone>().GetList())
            {
                if (card.card.isBuffCard == true)
                {
                    Debug.Log(card.name + "is active");
                    card.isPlayable = true;
                }
                else
                {
                    card.isPlayable = false;
                }
            }
        }
        else if (turnManager.GetPhase() == TurnManager.Phase.PlayCard)
        {

            int mana = hand.GetComponentInParent<Mana>().GetMana();
            //Debug.Log(mana);
            foreach (CardObject card in hand.GetComponent<DropZone>().GetList())
            {
                if (card.card.isBuffCard == false)
                {
                    for(int i = 0; i < card.card.abilities.Count;i++)
                    {
                        if (card.card.abilities[i].name=="Cost")
                        {
                            if (card.card.abilityValues[i]<=mana)
                            {
                                Debug.Log(card.name + "is active");
                                card.isPlayable = true;
                            }
                            else
                            {
                                card.isPlayable = false;
                            }
                            break;
                        }
                    }
                }
                else
                {
                    card.isPlayable = false;
                    Debug.Log(card.name + "has been deactivated");
                }

            }
        }
        else if (turnManager.GetPhase() == TurnManager.Phase.Discard)
        {
            foreach (CardObject card in hand.GetComponent<DropZone>().GetList())
            {
                card.isPlayable = true;
            }
        }  
        else
        {
            foreach (CardObject card in hand.GetComponent<DropZone>().GetList())
            {
                card.isPlayable = false;
            }
        }


        if (turnManager.GetPhase() == TurnManager.Phase.OpponentPlayBuff)
        {
            //Debug.Log("in playbuff playable phase");
            foreach (CardObject card in oppHand.GetComponent<DropZone>().GetList())
            {
                if (card.card.isBuffCard == true)
                {
                    card.isPlayable = true;
                }
                else
                {
                    card.isPlayable = false;
                }
            }
        }
        else if (turnManager.GetPhase() == TurnManager.Phase.OpponentPlayCard)
        {

            int mana = oppHand.GetComponentInParent<Mana>().GetMana();
            //Debug.Log(mana);
            foreach (CardObject card in oppHand.GetComponent<DropZone>().GetList())
            {
                //Debug.Log(card.name);
                if (card.card.isBuffCard == false)
                {
                    for (int i = 0; i < card.card.abilities.Count; i++)
                    {
                        if (card.card.abilities[i].name == "Cost")
                        {
                            if (card.card.abilityValues[i] <= mana)
                            {
                                card.isPlayable = true;
                            }
                            else
                            {
                                card.isPlayable = false;
                            }
                            break;
                        }
                    }
                }
                else
                {
                    card.isPlayable = false;
                }

            }
        }
        else if (turnManager.GetPhase() == TurnManager.Phase.OpponentDiscard)
        {
            foreach (CardObject card in oppHand.GetComponent<DropZone>().GetList())
            {
                card.isPlayable = true;
            }
        }
        else
        {
            foreach (CardObject card in oppHand.GetComponent<DropZone>().GetList())
            {
                card.isPlayable = false;
            }
        }
    }
}
