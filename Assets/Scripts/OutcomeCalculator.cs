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
    int roundNumber = 0;
    const int PLAYER= 0;
    const int OPPONENT= 1;
    GameObject playerObject;
    GameObject opponentObject;
    public CardDB collection;
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
        CalculateByArea(playerCards,PLAYER);
        CalculateByArea(opponentCards,OPPONENT);

        StartCoroutine(Calc());
        ReduceLife();
        phaseText.GetComponent<FadeAway>().ResetFadeAway();
        ClearCards();
        StartNewRound();     
    }
    void AddToCollection(CardObject cardObject)
    {   
        if(HasDuration(cardObject))
        {
            Image image = cardObject.transform.GetChild(2).GetComponent<Image>();
            string[] parts = image.GetComponentInChildren<TMP_Text>().text.Split('/');
            if (parts[0] != parts[1])
            {
                return;
            }
        }
        CardScriptableObject newCard = cardObject.card.DeepCopy();
        collection.allCards.Add(newCard);
        collection.runTimeCards.Add(new SerializableCard(newCard));
        collection.SaveRuntimeChanges();
    }

    bool HasDuration(CardObject cardObject)
    {
        for (int i = 0; i < cardObject.card.abilities.Count; i++)
        {
            if (cardObject.card.abilities[i].name =="Duration")
            {
                return true;
            }

        }
        return false;
    }

    void StartNewRound()
    {
        playerObject.GetComponent<ManaManager>().ResetMana();
        opponentObject.GetComponent<ManaManager>().ResetMana();
        roundNumber++;
        if (roundNumber == 1)
        {
            playerObject.GetComponentInChildren<Hand>().ChangeDrawCount(playerObject.GetComponent<Character>().handSlots);
            opponentObject.GetComponentInChildren<Hand>().ChangeDrawCount(opponentObject.GetComponent<Character>().handSlots);
        }
        else
        {
            playerObject.GetComponentInChildren<Hand>().ResetDrawCount();
            opponentObject.GetComponentInChildren<Hand>().ResetDrawCount();
        }
        //add other effects after base values have been loaded, like buffslots and such.
    }

    /// <summary>
    /// empty the cards copied to the outcomeCalculator so they don't count again next round
    /// </summary>
    void ClearCards()
    {
        while (playerCards.Count > 0)
        {
            AddToCollection(playerCards[0]);
            Destroy(playerCards[0].gameObject);
            playerCards.Remove(playerCards[0]);
        }  
        while (opponentCards.Count > 0)
        {
            Destroy(opponentCards[0].gameObject);
            opponentCards.Remove(opponentCards[0]);
        }
    }

    void ReduceLife()
    {
        Character opponent = opponentObject.GetComponent<Character>();
        Character player= playerObject.GetComponent<Character>();
        Image playerLife = GameObject.Find("HealthBar").GetComponentsInChildren<Image>()[1];
        Image oppLife = GameObject.Find("OpponentHealthBar").GetComponentsInChildren<Image>()[1];

        
        Debug.Log("opp block is " + (block[OPPONENT] + opponent.GetDefense()));
        Debug.Log("player dmg is" + damage[PLAYER]);
        Debug.Log("opp self damage by " + loseHealth[OPPONENT]);
        bonusDamage[PLAYER] += player.GetAttack();
        if (damage[PLAYER] ==0)
        {
            Debug.Log("no bonus dmg");
            bonusDamage[PLAYER] = 0;
        }
        Debug.Log("opp loses life by " + (loseHealth[OPPONENT] + damage[PLAYER] + bonusDamage[PLAYER] - block[OPPONENT]));
        Debug.Log("opp heals for " + (heal[OPPONENT] + opponent.GetRegeneration()));
        
        
        Debug.Log("that's it for opp");

        Debug.Log("player block is " + (block[PLAYER] + player.GetDefense()));
        Debug.Log("opp dmg is" + damage[OPPONENT]);
        bonusDamage[OPPONENT] += opponent.GetAttack();
        if (damage[OPPONENT] == 0)
        {
            Debug.Log("no bonus dmg");
            bonusDamage[OPPONENT] = 0;
        }
        Debug.Log("player self damage by " + loseHealth[PLAYER]);
        int damagedonetoopponent = loseHealth[OPPONENT] + damage[PLAYER] + bonusDamage[PLAYER] - block[1];
        int damagedonetoplayer = loseHealth[PLAYER] + damage[OPPONENT] + bonusDamage[OPPONENT] - block[PLAYER];
        Debug.Log("player loses life by " + damagedonetoplayer);
        Debug.Log("player heals for " + (heal[PLAYER] + player.GetRegeneration()));
        Debug.Log("that's it for player");
        
        int playerCurrLife = playerObject.GetComponent<Character>().currentHealthPoints;
        int opponentCurrLife = opponentObject.GetComponent<Character>().currentHealthPoints;
        playerObject.GetComponent<Character>().currentHealthPoints = playerCurrLife - damagedonetoplayer + (heal[PLAYER] + player.GetRegeneration());
        opponentObject.GetComponent<Character>().currentHealthPoints = opponentCurrLife;
        float playerfillAmount = ((float)playerCurrLife - (float)damagedonetoplayer + (float)(heal[PLAYER] + player.GetRegeneration())) / (float)playerObject.GetComponent<Character>().maxHealthPoints;
        playerLife.fillAmount = Mathf.Clamp(playerfillAmount, 0f, 1f);

        float oppfillAmount = ((float)opponentCurrLife - (float)damagedonetoopponent + (float)(heal[OPPONENT] + player.GetRegeneration())) / (float)opponentObject.GetComponent<Character>().maxHealthPoints;
        oppLife.fillAmount = Mathf.Clamp(oppfillAmount, 0f, 1f);

        //if both<=0
        if(playerfillAmount<0f && oppfillAmount<0f)
        {
            GameObject.Find("EndMatchScreen").transform.GetChild(0).gameObject.SetActive(true);
            Debug.Log("Both died");
        }
        if(oppfillAmount<0f)
        {
            GameObject.Find("EndMatchScreen").transform.GetChild(0).gameObject.SetActive(true);
            Debug.Log("you won!");
        }
        if(playerfillAmount<0f)
        {
            GameObject.Find("EndMatchScreen").transform.GetChild(0).gameObject.SetActive(true);
            Debug.Log("you lost!");
        }
        //if playerlife<=0

        //if opplife<=0

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
            //yield return StartCoroutine(ShowCardCoroutine(list[0]));
            //list.RemoveAt(0);
            Debug.Log("in enumerator");
        }
    }
    IEnumerator ShowCardCoroutine(CardObject card)
    {
        card.GetComponent<DisplayCard>().enabled = true;
        yield return null;
    }
}
