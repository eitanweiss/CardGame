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
                buttonA.enabled = true;
                buttonB.enabled = true;
                hand.enabled = true;
                break;
            
            case TurnManager.Phase.PlayBuff:
                buttonA.enabled = false;
                buttonB.enabled = false;
                buffArea.enabled = true;
                buffArea.ActivateDrag();
                break;

            case TurnManager.Phase.PlayCard:
                buffArea.enabled = false;
                buffArea.DisableDrag();
                playArea.enabled = true;
                break;

            case TurnManager.Phase.Discard:
                playArea.enabled = false;
                discard.enabled = true;
                break;
            
            case TurnManager.Phase.OpponentDraw:
                discard.enabled = false;
                hand.enabled = false;
                break;

            default:
                break;
        }
    }
}
