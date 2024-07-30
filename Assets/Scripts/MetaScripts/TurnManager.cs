using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

/// <summary>
/// in charge of changing the phase FSM
/// 
/// </summary>
public class TurnManager : MonoBehaviour
{
    public bool isMyTurnToStart { get; private set; }
    [SerializeField]private TextMeshProUGUI phaseText;
    public enum Phase { Draw,PlayBuff, PlayCard,Discard, OpponentDraw,OpponentPlayBuff,OpponentPlayCard,OpponentDiscard, Calculation};
    Phase phase;
    [SerializeField] OutcomeCalculator outcomeCalculator;
    [SerializeField] GameObject calcScreen;
    [SerializeField] DurationManager durationManager;

    /// <summary>
    /// at start of game randomizes who is first
    /// </summary>
    void Start()
    {
        if(Random.Range(0,2)==1)
        {
            isMyTurnToStart=false;
            phase = Phase.Calculation;
            phaseText.text = "Draw Phase";
        }
        else
        {
            isMyTurnToStart = true;
            phase = Phase.Calculation;
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
                phase = Phase.Calculation;
                phaseText.text = "Draw Phase";
                break;
            case Phase.Calculation:
                calcScreen.SetActive(true);
                outcomeCalculator.CalculateAllZones(phaseText);
                durationManager.ReduceRoundCount();
                phase = Phase.Draw;
                isMyTurnToStart = true;
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
                phase = Phase.Calculation;
                phaseText.text = "Opponent's Draw Phase";
                //do calculation of damage and stuff
                break;
            case Phase.Calculation:
                /// should go like this: finish discard phase, open calculation panel, place each active card in the view and iterate through them
                /// then sum it up and exit to next phase
                calcScreen.SetActive(true);
                outcomeCalculator.CalculateAllZones(phaseText);
                durationManager.ReduceRoundCount();
                phase = Phase.OpponentDraw;
                isMyTurnToStart = false;
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
        Debug.LogError(phase);//allows me to follow the curernt phase easilyer
        Notify();
    }
    public void Notify()
    {
        phaseText.GetComponent<FadeAway>().ResetFadeAway();
        //let zone activation know somehow
        GameObject.Find("PlayerObject").GetComponent<ZoneActivation>().OnPhaseChange();
    }
}
