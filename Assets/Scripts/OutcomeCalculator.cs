using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
/// <summary>
/// calculates all card effects at the end of each round. will reset values
/// </summary>
public class OutcomeCalculator : MonoBehaviour
{
    //need to find a way to make this not HardCoded if possible

    GameObject playerObject;
    GameObject opponentObject;
    private void Start()
    {
        playerObject = GameObject.Find("PlayerObject").gameObject;
        opponentObject = GameObject.Find("OpponentObject").gameObject;
    }
    //this will be effects on player
    int [] damage = new int[2];
    int [] bonusDamage = new int[2];
    int [] block = new int[2];
    int [] heal = new int[2];
    int [] loseHealth = new int[2];
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
        for (int i = 0; i < 2; i++)
        {
            damage[i] = 0;
            bonusDamage[i] = 0;
            block[i] = 0;
            heal[i] = 0;
            loseHealth[i] = 0;
            //decreaseMana = 0;
            //decreaseMaxHP = 0;
            //decreasehandSize = 0;
            //decreasePlayArea = 0;
            //decreaseBuffArea = 0;
            //decreaseDiscard = 0;
            //decreaseDraw = 0;
        }
    }

    //need to decide if i want to calculate by card or by effect
    //ATM it is by card
    public void CalculateByArea(List<CardObject> list, int player)
    {
         foreach(CardObject card in list)
        {
            for(int i = 0;i<card.card.abilities.Count;i++)
            {
                switch (card.card.abilities[i].name)
                {
                    case "Attack":
                        damage[player] += card.card.abilityValues[i];
                        //some sort of animation?
                        break;
                    case "Bonus Attack":
                        bonusDamage[player] += card.card.abilityValues[i];
                        //some sort of animation?
                        break;
                    case "Self Attack Break":
                        damage[player] -= card.card.abilityValues[i];
                        //some sort of animation?
                        break;
                    case "Attack Break":
                        //some sort of animation?
                        break;
                    case "Block":
                        block[player] += card.card.abilityValues[i];
                        //some sort of animation?
                        break;
                    case "Self Defense Break":
                        block[player] -= card.card.abilityValues[i];
                        //some sort of animation?
                        break;
                    case "Defense Break":
                        //some sort of animation?
                        break;
                    case "Heal":
                        heal[player] += card.card.abilityValues[i];
                        //some sort of animation?
                        break;
                    case "Self Heal Block":
                        heal[player] -= card.card.abilityValues[i];
                        //some sort of animation?
                        break;
                    case "Opponent Heal Block":
                        //some sort of animation?
                        break;
                    case "Self Damage":
                        loseHealth[player] += card.card.abilityValues[i];
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
        Debug.Log("calcView should be active");
        ResetValuesToZero();

        //add cards to lists.

        AddCards(opponentObject,opponentCards);
        AddCards(playerObject,playerCards);

        //pan out cards in 1st player
        StartCoroutine(Timer(playerCards) );
        //pan out cards in 2nd player

        //calculate the rest of the effects(those not directly related to hp and attack)


        //calculate dmg and related effects
        CalculateByArea(playerCards,0);
        CalculateByArea(opponentCards,1);

        StartCoroutine(Calc());
        ReduceLife();
        phaseText.GetComponent<FadeAway>().ResetFadeAway();
        playerObject.GetComponent<ManaManager>().ResetMana();
        opponentObject.GetComponent<ManaManager>().ResetMana();
        playerObject.GetComponentInChildren<Hand>().ResetDrawCount();
        opponentObject.GetComponentInChildren<Hand>().ResetDrawCount();
    }


    void ReduceLife()
    {
        Character opponent = opponentObject.GetComponent<Character>();
        Character player= playerObject.GetComponent<Character>();

        Debug.Log("opp block is " + (block[1] + opponent.GetDefense()));
        Debug.Log("player dmg is" + damage[0]);
        bonusDamage[0] += player.GetAttack();
        if (damage[0]==0)
        {
            Debug.Log("no bonus dmg");
            bonusDamage[0] = 0;
        }

        Debug.Log("opp self damage by " + loseHealth[1]);
        Debug.Log("opp loses life by " + (loseHealth[1] + damage[0] + bonusDamage[0] - block[1]));
        Debug.Log("opp heals for " + (heal[1] + opponent.GetRegeneration()));
        Debug.Log("that's it for opp");

        Debug.Log("player block is " + (block[0] + player.GetDefense()));
        Debug.Log("opp dmg is" + damage[1]);
        bonusDamage[1] += opponent.GetAttack();
        if (damage[1] == 0)
        {
            Debug.Log("no bonus dmg");
            bonusDamage[1] = 0;
        }

        Debug.Log("player self damage by " + loseHealth[0]);
        int damagedone = loseHealth[0] + damage[1] + bonusDamage[1] - block[0];
        Debug.Log("player loses life by " + damagedone);
        Debug.Log("player heals for " + (heal[0] + player.GetRegeneration()));
        Debug.Log("that's it for player");
        Image playerLife = GameObject.Find("HealthBar").GetComponentsInChildren<Image>()[1];
        Debug.Log(playerLife.transform.name) ;
        Image oppLife = GameObject.Find("OpponentHealthBar").GetComponentsInChildren<Image>()[1];
        int maxlife = playerObject.GetComponent<Character>().maxHealthPoints;
        Debug.Log(maxlife);
        playerLife.fillAmount = ((float)maxlife- (float)damagedone)/ (float)maxlife;
        maxlife = opponentObject.GetComponent<Character>().maxHealthPoints;
        oppLife.fillAmount = ((float)maxlife- (float)damagedone)/ (float)maxlife;
    }
    IEnumerator Calc()
    {
        yield return new WaitForSeconds(1);
        calcView.SetActive(false);

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
