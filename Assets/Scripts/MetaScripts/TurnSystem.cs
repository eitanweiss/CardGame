using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TurnSystem : MonoBehaviour
{
    public bool isMyTurn = true;
    public TMP_Text text; 
    enum Phase { Draw,PlayBuff, PlayCard,Discard, OpponentDraw,OpponentPlayBuff,OpponentPlayCard,OpponentDiscard};
    Phase phase;

    public void EndPhase()
    {
        switch (phase)
        {
            case Phase.Draw:
                phase = Phase.PlayBuff;
                break;
            case Phase.PlayBuff: phase = 
                Phase.PlayCard;
                break;
            case Phase.PlayCard:
                phase = Phase.Discard; 
                break;
            case Phase.Discard: 
                phase = Phase.OpponentDraw;
                isMyTurn = false;
                break;
            case Phase.OpponentDraw:
                phase = Phase.OpponentPlayBuff;
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
