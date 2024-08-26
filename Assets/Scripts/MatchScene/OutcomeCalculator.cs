using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// calculates all card effects at the end of each round. will reset values
/// this class is currently a mess that needs to be fixed and changed a lot.
/// </summary>
public class OutcomeCalculator : MonoBehaviour
{
    //need to find a way to make this not HardCoded if possible
    int roundNumber = 0;
    const int PLAYER= 0;
    const int OPPONENT= 1;
    
    GameObject playerObject;
    GameObject opponentObject;
    
    HashSet<int> cardIDs = new HashSet<int>();
    
    public CardDB collection;
    public List<CardObject> playerCards;
    public List<CardObject> opponentCards;
    
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

    int[] selfDamage = new int[2];
    //private int decreaseMana;
    //private int decreaseMaxHP;
    //private int decreasehandSize;
    //private int decreasePlayArea;
    //private int decreaseBuffArea;
    //private int decreaseDiscard;
    //private int decreaseDraw;
    
    public GameObject cardPrefab;
    
    [SerializeField] GameObject calcView;

    /// <summary>
    /// basic start method to set all values to zero.
    /// </summary>
    public void ResetValuesToZero()
    {
        for (int i = 0; i < 2; i++)
        {
            damage[i] = 0;
            bonusDamage[i] = 0;
            block[i] = 0;
            heal[i] = 0;
            selfDamage[i] = 0;
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
    /// <summary>
    /// calculate card effects by card and add to total.
    /// each value has 2 entrances: first for player, second for opponent.
    /// </summary>
    /// <param name="list">list of cards to go through</param>
    /// <param name="player">player these cards belong to</param>
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
                        damage[1 - player] -= card.card.abilityValues[i];
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
                        block[1 - player] -= card.card.abilityValues[i]; 
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
                        heal[1-player] -= card.card.abilityValues[i];
                        break;
                    case "Self Damage":
                        selfDamage[player] += card.card.abilityValues[i];
                        //some sort of animation?
                        break;
                    case "Decrease Bonus Damage":
                        //some sort of animation?
                        bonusDamage[1 - player] -= card.card.abilityValues[i];
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
    /// <param name="gameObject"> object the cards belong to</param>
    /// <param name="list"> will contain all the cards to be added upon end of method</param>
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
                newCardObj.origin = card.origin;
                DisplayCard displayCard = cardGO.GetComponent<DisplayCard>();
                displayCard.SetCard(newCardObj.card);
                cardGO.GetComponent<DisplayCard>().enabled = false;
                list.Add(newCardObj);
            }
        }
    }

    /// <summary>
    /// go through all the different zones. add all the cards in each one to a single list, and calculate the outcome of the list
    /// param is temporary as not sure how to insert the delay yet.
    /// should delay the start of the next round until calculation is finished.
    /// </summary>
    /// <param name="phaseText"></param>
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

        ///the order is what i want: calculate all the cards, then reduce the life in the correct amount

        //calculate dmg and related effects
        CalculateByArea(playerCards,PLAYER);
        CalculateByArea(opponentCards,OPPONENT);

        StartCoroutine(Calc());
        ReduceLife();
        phaseText.GetComponent<FadeAway>().ResetFadeAway();
        ClearCards();
        //signify end of round - also does not belong here in the long run, but belongs in the turnManager
        StartNewRound();     
    }

    /// <summary>
    /// adds cards to the collection of played cards.
    /// WARNING: buff cards with no duration
    /// ERROR: ATM adds them each round they are in the play field - this is wrong.
    /// </summary>
    /// <param name="cardObject">card to be added to the collection</param>
    void AddToCollection(CardObject cardObject)
    {
        int _instanceID = cardObject.GetInstanceID();

        if (!cardIDs.Contains(_instanceID) && cardObject.origin == Origin.RandomDeck)
        {
            cardIDs.Add(_instanceID);
            CardScriptableObject newCard = cardObject.card.DeepCopy();
            collection.allCards.Add(newCard); // Consider if needed
            collection.runTimeCards.Add(new SerializableCard(newCard));
            collection.SaveRuntimeChanges(); // Consider when to save since this should be done only once per match
        }




    //if(HasDuration(cardObject))
    //{
    //    Image image = cardObject.transform.GetChild(2).GetComponent<Image>();
    //    string[] parts = image.GetComponentInChildren<TMP_Text>().text.Split('/');
    //    Debug.Log(parts[0] + " and " + parts[1]);
    //    if (parts[0] != parts[1])
    //    {
    //        return;
    //    }
    //}
    //if (!cardObject.card.isBuffCard)
    //{

    //}
    //else
    //{
    //    //buff cards with no duration should be counted only once and not every time they appear
    //}
}

    /// <summary>
    /// check if card has a duration
    /// </summary>
    /// <param name="cardObject"> card to check</param>
    /// <returns></returns>
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

    /// <summary>
    /// effects that take place in the new round.
    /// </summary>
    void StartNewRound()
    {
        playerObject.GetComponent<Mana>().ResetMana();
        opponentObject.GetComponent<Mana>().ResetMana();
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

    /// <summary>
    /// reduce life by calculated amount
    /// ATM full of debugs, just to make sure I can follow what happens.
    /// </summary>
    void ReduceLife()
    {
        //setup
        Character opponent = opponentObject.GetComponent<Character>();
        Character player= playerObject.GetComponent<Character>();
        Image playerLife = GameObject.Find("HealthBar").GetComponentsInChildren<Image>()[1];
        Image oppLife = GameObject.Find("OpponentHealthBar").GetComponentsInChildren<Image>()[1];
        
        //calculation for damage to opp
        bonusDamage[PLAYER] += player.GetAttack();
        int damagedonetoOpponent = CalculateDamage(OPPONENT);

        //calculation for damage to player
        bonusDamage[OPPONENT] += opponent.GetAttack();
        int damagedonetoplayer = CalculateDamage(PLAYER);

        PrintToLog(opponent, player);
        //get curr life
        int playerCurrLife = playerObject.GetComponent<Character>().currentHealthPoints;
        Debug.Log("players life was ------------------- = " + playerCurrLife);
        int opponentCurrLife = opponentObject.GetComponent<Character>().currentHealthPoints;


        playerCurrLife -= damagedonetoplayer;
        if (playerCurrLife > 0)
        {
            playerCurrLife += heal[PLAYER] + player.GetRegeneration();
        }
        playerLife.fillAmount = Mathf.Clamp((float)playerCurrLife/ (float)playerObject.GetComponent<Character>().maxHealthPoints,0f,1f);
        playerObject.GetComponent<Character>().currentHealthPoints = playerCurrLife;
        Debug.Log("players life is ------------------- = " + playerCurrLife);

        opponentCurrLife -= damagedonetoOpponent;
        if (opponentCurrLife > 0)
        {
            opponentCurrLife += heal[OPPONENT] + opponent.GetRegeneration();
        }
        oppLife.fillAmount = Mathf.Clamp((float)opponentCurrLife / (float)opponentObject.GetComponent<Character>().maxHealthPoints, 0f, 1f);
        opponentObject.GetComponent<Character>().currentHealthPoints = opponentCurrLife;



        //update life in character
        //playerObject.GetComponent<Character>().currentHealthPoints = playerCurrLife - damagedonetoplayer + (heal[PLAYER] + player.GetRegeneration());
        //opponentObject.GetComponent<Character>().currentHealthPoints = opponentCurrLife -damagedonetoOpponent + (heal[OPPONENT] +opponent.GetRegeneration());
        




        //float playerFillAmount = ((float)playerCurrLife - (float)damagedonetoplayer + (float)(heal[PLAYER] + player.GetRegeneration())) / (float)playerObject.GetComponent<Character>().maxHealthPoints;
        //playerLife.fillAmount = Mathf.Clamp(playerFillAmount, 0f, 1f);

        //float oppFillAmount = ((float)opponentCurrLife - (float)damagedonetoOpponent + (float)(heal[OPPONENT] + player.GetRegeneration())) / (float)opponentObject.GetComponent<Character>().maxHealthPoints;
        //oppLife.fillAmount = Mathf.Clamp(oppFillAmount, 0f, 1f);


        EndCondition(playerCurrLife, opponentCurrLife);

    }

    /// <summary>
    /// calculate the final damage done after all cards and effects have been added. damage is: self damage of player, damage of other player
    /// </summary>
    /// <param name="player"></param>
    /// <returns></returns>
    int CalculateDamage(int player)
    {
        if (damage[1-player]<0)
        {
            damage[1 - player] = 0;
        }
        if (block[player]<0)
        {
            block[player] = 0;
        }
        if (heal[player]<0)
        {
            heal[player] = 0;
        }
        int result = selfDamage[player] + damage[1 - player] - block[player];
        
        if (damage[1- player]>0)
        {
            result += bonusDamage[1 - player];
        }
        return result;
    }


    /// <summary>
    /// in what way did the game end?
    /// </summary>
    /// <param name="playerFillAmount">player's life</param>
    /// <param name="oppFillAmount">opponent's life</param>
    void EndCondition(float playerFillAmount,float oppFillAmount)
    {
        //if both<=0
        if (playerFillAmount < 0f && oppFillAmount < 0f)
        {
            GameObject.Find("EndMatchScreen").transform.GetChild(0).gameObject.SetActive(true);
            Debug.Log("Both died");
        }
        //if opplife<=0
        if (oppFillAmount < 0f)
        {
            GameObject.Find("EndMatchScreen").transform.GetChild(0).gameObject.SetActive(true);
            Debug.Log("you won!");
        }
        //if playerlife<=0
        if (playerFillAmount < 0f)
        {
            GameObject.Find("EndMatchScreen").transform.GetChild(0).gameObject.SetActive(true);
            Debug.Log("you lost!");
        }
    }

    /// <summary>
    /// prints different values for calculation
    /// </summary>
    /// <param name="opponent">opponent character </param>
    /// <param name="player">player character</param>
    void PrintToLog(Character opponent, Character player)
    {
        int damagedonetoplayer = damage[OPPONENT] + bonusDamage[OPPONENT] - block[PLAYER] + selfDamage[PLAYER];
        int damagedonetoOpponent = damage[PLAYER] + bonusDamage[PLAYER] - block[OPPONENT] + selfDamage[OPPONENT];


        Debug.Log("opp block is " + (block[OPPONENT] + opponent.GetDefense()));
        Debug.Log("player dmg is" + damage[PLAYER]);
        Debug.Log("player bonus dmg is" + bonusDamage[PLAYER]);
        Debug.Log("opp self damage by " + selfDamage[OPPONENT]);
        Debug.Log("opp loses life by " + damagedonetoOpponent);
        Debug.Log("opp heals for " + (heal[OPPONENT] + opponent.GetRegeneration()));


        Debug.Log("that's it for opp");
        Debug.Log("player block is " + (block[PLAYER] + player.GetDefense()));
        Debug.Log("opp dmg is" + damage[OPPONENT]);
        Debug.Log("opp bonus dmg is" + bonusDamage[OPPONENT]);
        Debug.Log("player self damage by " + selfDamage[PLAYER]);
        Debug.Log("player loses life by " + damagedonetoplayer);
        Debug.Log("player heals for " + (heal[PLAYER] + player.GetRegeneration()));

        Debug.Log("that's it for player");
    }

    /// <summary>
    /// show calcview for 1 second(later will be on for as long as the calculation takes and once it is finished it will go off
    /// </summary>
    /// <returns></returns>
    IEnumerator Calc()
    {
        yield return new WaitForSeconds(1);
        calcView.SetActive(false);

    }


    /// <summary>
    /// meant to show all the cards on the screen slowly using the showcard coroutine
    /// </summary>
    /// <param name="list">list of cards</param>
    /// <returns></returns>
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
