using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    public bool isMyTurn = true;
    public Button phaseButton;
    public enum Phase { Draw,PlayBuff, PlayCard,Discard, OpponentDraw,OpponentPlayBuff,OpponentPlayCard,OpponentDiscard};
    Phase phase;


    void Start()
    {
            phase = Phase.OpponentDiscard;
            EndPhase();
    }
    public Phase GetPhase()
    {
        Phase newPhase = phase;
        return newPhase;
    }

    public void EndPhase()
    {
        switch (phase)
        {
            case Phase.Draw:
                phase = Phase.PlayBuff;
                phaseButton.GetComponentInChildren<TMP_Text>().text = "Buff Phase";
                break;
            case Phase.PlayBuff: phase = 
                Phase.PlayCard;
                phaseButton.GetComponentInChildren<TMP_Text>().text = "Card Phase";
                break;
            case Phase.PlayCard:
                phase = Phase.Discard;
                phaseButton.GetComponentInChildren<TMP_Text>().text = "Discard";
                break;
            case Phase.Discard: 
                phase = Phase.OpponentDraw;
                phaseButton.GetComponentInChildren<TMP_Text>().text = "End Turn";
                isMyTurn = false;
                break;
            case Phase.OpponentDraw:
                phase = Phase.OpponentPlayBuff;
                phaseButton.GetComponentInChildren<TMP_Text>().text = "Opponent's Turn";
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
                phaseButton.GetComponentInChildren<TMP_Text>().text = "Draw Phase";
                break;
        }
        //let zone activation know somehow
        GameObject.Find("PlayerObject").GetComponent<ZoneActivation>().OnPhaseChange();
    }
}
