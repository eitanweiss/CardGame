using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TurnSystem : MonoBehaviour
{
    public bool isMyTurn = true;
    public TMP_Text text;
    public Button phaseButton;
    enum Phase { Draw,PlayBuff, PlayCard,Discard, OpponentDraw,OpponentPlayBuff,OpponentPlayCard,OpponentDiscard};
    Phase phase;

    public void EndPhase()
    {
        switch (phase)
        {
            case Phase.Draw:
                phase = Phase.PlayBuff;
                phaseButton.GetComponent<Text>().text = "Buff Phase";
                break;
            case Phase.PlayBuff: phase = 
                Phase.PlayCard;
                phaseButton.GetComponent<Text>().text = "Card Phase";
                break;
            case Phase.PlayCard:
                phase = Phase.Discard;
                phaseButton.GetComponent<Text>().text = "Discard";
                break;
            case Phase.Discard: 
                phase = Phase.OpponentDraw;
                phaseButton.GetComponent<Text>().text = "End Turn";
                isMyTurn = false;
                break;
            case Phase.OpponentDraw:
                phase = Phase.OpponentPlayBuff;
                phaseButton.GetComponent<Text>().text = "Opponent's Turn";
                break;
            case Phase.OpponentPlayBuff:
                phase = Phase.OpponentPlayCard;
                break;
            case Phase.OpponentPlayCard:
                phase = Phase.OpponentDiscard;
                break;
            case Phase.OpponentDiscard:
                phase = Phase.Draw;
                isMyTurn = true;
                text.text = "get max mana from character";//might not want to do this this way, because during turn mana value needs to update which means either other
                                                          //scripts will have access to same text or need more functions here which is more likely

                break;

        }
    }
    //will probably have phases: draw, playbuff, playcard, discard.
}
