using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class TurnManager : MonoBehaviour
{
    public bool isMyTurnToStart = true;//not sure if this is needed
    public TextMeshProUGUI phaseText;
    public enum Phase { Draw,PlayBuff, PlayCard,Discard, OpponentDraw,OpponentPlayBuff,OpponentPlayCard,OpponentDiscard};
    Phase phase;


    void Start()
    {
        if(Random.Range(0,2)==1)
        {
            isMyTurnToStart=false;
            phase = Phase.Discard;
            phaseText.text = "Draw Phase";
        }
        else
        {
            isMyTurnToStart = true;
            phase = Phase.OpponentDiscard;
            phaseText.text = "Opponent's Draw Phase";
        }
        GameObject.Find("GameManager").GetComponent<GameManager>().TurnControl();
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
            case Phase.OpponentDraw:
                phase = Phase.OpponentPlayBuff;
                phaseText.text = "Opponent's Buff Phase";
                break;
            case Phase.OpponentPlayBuff:
                phase = Phase.OpponentPlayCard;
                phaseText.text = "Opponent's Attack Phase";
                break;
            case Phase.OpponentPlayCard:
                phase = Phase.OpponentDiscard;
                phaseText.text = "Opponent's Discard Phase";
                break;
            case Phase.OpponentDiscard:
                phase = Phase.Draw;
                phaseText.text = "Draw Phase";
                break;
            case Phase.Draw:
                phase = Phase.PlayCard;
                phaseText.text = "Defense Phase";
                break;
            case Phase.PlayCard:
                phase = Phase.Discard;
                phaseText.text = "Discard";
                break;
            case Phase.Discard:
                phase = Phase.Draw;
                //do calculation of damage and stuff
                isMyTurnToStart = true;
                phaseText.text = "Draw Phase";
                break;
        }
    }

    public void PlayerStartsRound()
    {
        switch (phase)
        {
            case Phase.Draw:
                phase = Phase.PlayBuff;
                phaseText.text = "Buff Phase";
                break;
            case Phase.PlayBuff:
                phase = Phase.PlayCard;
                phaseText.text = "Attack Phase";
                break;
            case Phase.PlayCard:
                phase = Phase.Discard;
                phaseText.text = "Discard";
                break;
            case Phase.Discard:
                phase = Phase.OpponentDraw;
                phaseText.text = "Opponent's Draw Phase";
                break;
            case Phase.OpponentDraw:
                phase = Phase.OpponentPlayCard;
                phaseText.text = "Opponent's Defense Phase";
                break;
            case Phase.OpponentPlayCard:
                phase = Phase.OpponentDiscard;
                phaseText.text = "Opponent's Discard Phase";
                break;
            case Phase.OpponentDiscard:
                phase = Phase.OpponentDraw;
                isMyTurnToStart = false;
                phaseText.text = "Opponent's Draw Phase";
                //do calculation of damage and stuff
                break;
        }

    }
    //want to change this for the top version of 2 round types
    public void ChangePhase()
    {
        if(isMyTurnToStart==false)
        {
            OpponentStartsRound();
        }
        else
        {
            PlayerStartsRound();
        }
        {
            //switch (phase)
            //{
            //    case Phase.Draw:
            //        phase = Phase.PlayBuff;
            //        phaseText.text = "Buff Phase";
            //        break;
            //    case Phase.PlayBuff: phase = 
            //        Phase.PlayCard;
            //        phaseText.text = "Card Phase";
            //        break;
            //    case Phase.PlayCard:
            //        phase = Phase.Discard;
            //        phaseText.text = "Discard";
            //        break;
            //    case Phase.Discard: 
            //        phase = Phase.OpponentDraw;
            //        phaseText.text = "End Turn";
            //        isMyTurn = false;
            //        break;
            //    case Phase.OpponentDraw:
            //        phase = Phase.OpponentPlayBuff;
            //        phaseText.text = "Opponent's Turn";
            //        break;
            //    case Phase.OpponentPlayBuff:
            //        phase = Phase.OpponentPlayCard;
            //        break;
            //    case Phase.OpponentPlayCard:
            //        phase = Phase.OpponentDiscard;
            //        break;
            //    case Phase.OpponentDiscard:
            //        phase = Phase.Draw;
            //        isMyTurn = true;
            //        phaseText.text = "Draw Phase";
            //        break;
            //}
        }
        Notify();
    }
    public void Notify()
    {
        phaseText.GetComponent<FadeAway>().ResetFadeAway();
        //let zone activation know somehow
        GameObject.Find("PlayerObject").GetComponent<ZoneActivation>().OnPhaseChange();
    }
}
