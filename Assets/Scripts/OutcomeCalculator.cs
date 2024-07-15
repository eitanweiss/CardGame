using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
/// <summary>
/// calculates all card effects at the end of each round. will reset values
/// </summary>
public class OutcomeCalculator : MonoBehaviour
{
    //this will be effects on player
    private int damage;
    private int bonusDamage;
    private int block;
    private int heal;
    private int loseHealth;
    private int decreaseMana;
    private int decreaseMaxHP;
    private int decreasehandSize;
    private int decreasePlayArea;
    private int decreaseBuffArea;
    private int decreaseDiscard;
    private int decreaseDraw;
    public DropZone activeArea;
    public DropZone buffArea;
    public DropZone playArea;

    //void Start()
    //{
    //    damage = 0;
    //    bonusDamage = 0;
    //    block = 0;
    //    heal = 0;
    //    loseHealth = 0;
    //    decreaseMana = 0;
    //    decreaseMaxHP = 0;
    //    decreasehandSize = 0;
    //    decreasePlayArea = 0;
    //    decreaseBuffArea = 0;
    //    decreaseDiscard = 0;
    //    decreaseDraw = 0;
    //}

    void ResetValuesToZero()
    {
        damage = 0;
        bonusDamage = 0;
        block = 0;
        heal = 0;
        loseHealth = 0;
        decreaseMana = 0;
        decreaseMaxHP = 0;
        decreasehandSize = 0;
        decreasePlayArea = 0;
        decreaseBuffArea = 0;
        decreaseDiscard = 0;
        decreaseDraw = 0;
    }
    //need to decide if i want to calculate by card or by effect
    //ATM it is by card
    public void CalculateByArea(DropZone zone)
    {
         foreach(CardObject card in zone.handCards)
        {
            for(int i = 0;i<card.card.abilities.Count;i++)
            {
                switch (card.card.abilities[i].name)
                {
                    case "Attack":
                        damage += card.card.abilityValues[i];
                        //some sort of animation?
                        break;
                    case "Bonus Attack":
                        bonusDamage += card.card.abilityValues[i];
                        //some sort of animation?
                        break;
                    case "Self Attack Break":
                        damage -= card.card.abilityValues[i];
                        //some sort of animation?
                        break;
                    case "Attack Break":
                        //some sort of animation?
                        break;
                    case "Block":
                        block += card.card.abilityValues[i];
                        //some sort of animation?
                        break;
                    case "Self Defense Break":
                        block -= card.card.abilityValues[i];
                        //some sort of animation?
                        break;
                    case "Defense Break":
                        //some sort of animation?
                        break;
                    case "Heal":
                        heal += card.card.abilityValues[i];
                        //some sort of animation?
                        break;
                    case "Self Heal Block":
                        heal -= card.card.abilityValues[i];
                        //some sort of animation?
                        break;
                    case "Opponent Heal Block":
                        //some sort of animation?
                        break;
                    case "Self Damage":
                        loseHealth += card.card.abilityValues[i];
                        //some sort of animation?
                        break;
                    case "Decrease Bonus Damage":
                        break;
                    
                    //non-damage related effects
                    case "Increase Discard Area":
                        break;
                    case "Decrease Discard Area":
                        break;
                    case "Increase Player Draw":
                        break;
                    case "Decrease Player Draw":
                        break;
                    case "Increase Duration":
                        break;
                    case "Decrease Duration":
                        break;
                    case "Increase Opponent Mana":
                        break;
                    case "Decrease Opponent Mana":
                        break;
                    case "Increase Opponent Buff Area":
                        break;
                    case "Decrease Opponent Buff Area":
                        break;
                    case "Increase Play Area":
                        break;
                    case "Decrease Opponent Play Area":
                        break;
                    case "Decrease Saved Draw":
                        break;
                    case "Increase Mana":
                        break;
                    case "Increase Max HP":
                        break;
                    case "Increase Max HP Permanently":
                        break;


                    default: break;
                }
            }
        }
    }



    public void CalculateAllZones()
    {
        ResetValuesToZero();
        CalculateByArea(activeArea);
        CalculateByArea(buffArea);
        CalculateByArea(playArea);


        //CalculateBlockByArea(activeArea);
        //CalculateBlockByArea(buffArea);
        //CalculateBlockByArea(playArea);

        //CalculateHealByArea(activeArea);
        //CalculateHealByArea(buffArea);
        //CalculateHealByArea(playArea);

        //CalculateSelfBlockBreakByArea(activeArea);
        //CalculateSelfBlockBreakByArea(buffArea);
        //CalculateSelfBlockBreakByArea(playArea);

        //CalculateSelfDamageBreakByArea(activeArea);
        //CalculateSelfDamageBreakByArea(buffArea);
        //CalculateSelfDamageBreakByArea(playArea);

        //CalculateSelfDamageByArea(activeArea);
        //CalculateSelfDamageByArea(buffArea);
        //CalculateSelfDamageByArea(playArea);

        //CalculateBonusDamageByArea(activeArea);
        //CalculateBonusDamageByArea(buffArea);
        //CalculateBonusDamageByArea(playArea);

        //calculate the rest of the effects(those not directly related to hp and attack)
    }
}
