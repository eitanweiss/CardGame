using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    //private int decreaseMana;
    //private int decreaseMaxHP;
    //private int decreasehandSize;
    //private int decreasePlayArea;
    //private int decreaseBuffArea;
    //private int decreaseDiscard;
    //private int decreaseDraw;
    public List<CardObject> playerCards;
    public List<CardObject> opponentCards;

    public GameObject cardPrefab;
    [SerializeField] GameObject calcView;

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

    public void ResetValuesToZero()
    {
        damage = 0;
        bonusDamage = 0;
        block = 0;
        heal = 0;
        loseHealth = 0;
        //decreaseMana = 0;
        //decreaseMaxHP = 0;
        //decreasehandSize = 0;
        //decreasePlayArea = 0;
        //decreaseBuffArea = 0;
        //decreaseDiscard = 0;
        //decreaseDraw = 0;
    }

    //need to decide if i want to calculate by card or by effect
    //ATM it is by card
    public void CalculateByArea(List<CardObject> list)
    {
         foreach(CardObject card in list)
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

    /// <summary>
    /// copy all cards from the different zones into one list in order to make it easier to show and go over all of them together
    /// </summary>
    /// <param name="gameObject"> object to add cards to</param>
    /// <param name="list"> cards to add to object</param>
    private void AddCards(GameObject gameObject, List<CardObject> list)
    {
        Component[] dropzones = gameObject.GetComponentsInChildren<DropZone>();
        foreach (DropZone dropzone in dropzones)
        {
            //ignore hands
            //need to find a way to make this not HardCoded
            if(dropzone.transform.name == "OpponentHand" || dropzone.transform.name =="Hand")
            {
                Debug.Log("in hand");
                continue;
            }
            foreach(CardObject card in dropzone.GetList())
            {
                GameObject cardGO = Instantiate(cardPrefab, transform);
                CardObject newCardObj = cardGO.AddComponent<CardObject>();
                newCardObj.card = card.card.DeepCopy();
                DisplayCard displayCard = cardGO.GetComponent<DisplayCard>();
                displayCard.SetCard(newCardObj.card);
                cardGO.GetComponent<DisplayCard>().enabled = false;
                list.Add(newCardObj);
            }
        }
    }

    public void CalculateAllZones(TextMeshProUGUI phaseText)
    {
        calcView.SetActive(true);
        ResetValuesToZero();

        //add cards to lists.
        //need to find a way to make this not HardCoded
        GameObject opponentObject = GameObject.Find("OpponentObject").gameObject;
        GameObject playerObject = GameObject.Find("PlayerObject").gameObject;
        AddCards(opponentObject,opponentCards);
        AddCards(playerObject,playerCards);

        //pan out cards in 1st player
        StartCoroutine(Timer(playerCards) );
        //pan out cards in 2nd player

        //calculate the rest of the effects(those not directly related to hp and attack)


        //calculate dmg and related effects
        CalculateByArea(playerCards);
        CalculateByArea(opponentCards);


        calcView.SetActive(false);
        phaseText.GetComponent<FadeAway>().ResetFadeAway();
        playerObject.GetComponent<ManaManager>().ResetMana();
        opponentObject.GetComponent<ManaManager>().ResetMana();
    }


    IEnumerator Timer(List<CardObject> list)
    {
        while(list.Count > 0)
        {
            float duration = 1f;
            float elapsedTime = 0f;
            while(elapsedTime < duration)
            {
                elapsedTime += Time.deltaTime;
                yield return null;
            } 
            yield return StartCoroutine(ShowCardCoroutine(list[0]));
            list.RemoveAt(0);
            Debug.Log("in enumerator");
        }
    }
    IEnumerator ShowCardCoroutine(CardObject card)
    {
        card.GetComponent<DisplayCard>().enabled = true;
        yield return null;
    }
}
