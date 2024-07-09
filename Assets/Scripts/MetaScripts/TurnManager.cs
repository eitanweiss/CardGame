using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    public bool isMyTurn = true;
    public TextMeshProUGUI phaseText;
    public enum Phase { Draw,PlayBuff, PlayCard,Discard, OpponentDraw,OpponentPlayBuff,OpponentPlayCard,OpponentDiscard};
    Phase phase;


    void Start()
    {
        if(Random.Range(0,1)==1)
        {
            //player starts
        }
        else
        {
            //AI starts
        }
        phase = Phase.OpponentDiscard;
        EndPhase();
    }
    public Phase GetPhase()
    {
        Phase newPhase = phase;
        return newPhase;
    }

    public void OpponentStartsRound()
    {
        switch (phase)
        {
            case Phase.Draw:
                phase = Phase.PlayCard;
                phaseText.text = "Defense Phase";
                break;

            case Phase.PlayCard:
                phase = Phase.Discard;
                phaseText.text = "Discard";
                break;
            case Phase.Discard:
                phase = Phase.OpponentDraw;
                //do calculation of damage and stuff
                isMyTurn = false;
                break;
        }
    }

    public void PlayerStartsRound()
    {
        switch (phase)
        {
            case Phase.Draw:
                phase = Phase.PlayCard;
                phaseText.text = "Buff Phase";
                break;
            case Phase.PlayBuff:
                phase =
                Phase.PlayCard;
                phaseText.text = "Attack Phase";
                break;
            case Phase.PlayCard:
                phase = Phase.Discard;
                phaseText.text = "Discard";
                break;
            case Phase.Discard:
                phase = Phase.OpponentDraw;
                phaseText.text = "Opponent's Turn";
                isMyTurn = false;
                break;
        }
    }
    //want to change this for the top version of 2 round types
    public void EndPhase()
    {
        switch (phase)
        {
            case Phase.Draw:
                phase = Phase.PlayBuff;
                phaseText.text = "Buff Phase";
                break;
            case Phase.PlayBuff: phase = 
                Phase.PlayCard;
                phaseText.text = "Card Phase";
                break;
            case Phase.PlayCard:
                phase = Phase.Discard;
                phaseText.text = "Discard";
                break;
            case Phase.Discard: 
                phase = Phase.OpponentDraw;
                phaseText.text = "End Turn";
                isMyTurn = false;
                break;
            case Phase.OpponentDraw:
                phase = Phase.OpponentPlayBuff;
                phaseText.text = "Opponent's Turn";
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
                phaseText.text = "Draw Phase";
                break;
        }
        phaseText.GetComponent<FadeAway>().resetFadeAway();
        //let zone activation know somehow
        GameObject.Find("PlayerObject").GetComponent<ZoneActivation>().OnPhaseChange();
    }
}
