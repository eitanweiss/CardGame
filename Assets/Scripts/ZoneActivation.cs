using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ZoneActivation : MonoBehaviour
{
    public TurnManager turnManager;
    public Button buttonA;
    public Button buttonB;
    public void OnPhaseChange()
    {
        DropZone buffArea = GameObject.Find("PlayerBuffArea").GetComponent<DropZone>();
        DropZone playArea = GameObject.Find("PlayerPlayArea").GetComponent<DropZone>();
        DropZone discard = GameObject.Find("Discard").GetComponent<DropZone>();
        DropZone hand = GameObject.Find("Hand").GetComponent<DropZone>();

        Debug.Log("on phase change works");
        switch (turnManager.GetPhase())
        {
            case TurnManager.Phase.Draw:
                discard.enabled = false;//this is done twice, may need to rwwrite how and when these get updated
                discard.GetComponent<DiscardManager>().deleteCards();//this should be done as first step of calculations. just here for now
                buttonA.enabled = true;
                buttonB.enabled = true;
                
                hand.enabled = true;
                hand.ActivateDrag();
                break;
            
            case TurnManager.Phase.PlayBuff:
                buttonA.enabled = false;
                buttonB.enabled = false;
                
                buffArea.enabled = true;
                buffArea.ActivateDrag();
                break;

            case TurnManager.Phase.PlayCard:
                buttonA.enabled = false;
                buttonB.enabled = false;
                buffArea.enabled = false;
                buffArea.DisableDrag();
                
                playArea.enabled = true;
                playArea.ActivateDrag();
                break;

            case TurnManager.Phase.Discard:
                playArea.enabled = false;
                playArea.DisableDrag();
                
                discard.enabled = true;
                discard.ActivateDrag();
                break;
            
            case TurnManager.Phase.OpponentDraw:
                discard.GetComponent<DiscardManager>().deleteCards();//this should be done as first step of calculations. just here for now
                discard.enabled = false;
                discard.DisableDrag();
                
                hand.enabled = false;
                hand.DisableDrag();
                break;

            default:
                break;
        }
    }
}
