using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.TextCore.Text;

public class Character : MonoBehaviour
{
    public Race race;
    public Type type;
    public int level;
    public int experience;
    public int experienceToLevel;
    public int skillPoints;
    public int attack;
    public int costToIncreaseAttack;
    public int defense;
    public int costToIncreaseDefense;
    public int regeneration;
    public int costToIncreaseRegeneration;
    public int handSlots;
    public int currentHandslots;
    public int buffSlots;
    public int currentBuffslots;
    public int playSlots;
    public int currentPlayslots;
    public int drawCount;
    public int currentDrawCount;
    public int discardSlots;
    public int currentDiscardSlots;
    public int maxDeckSize;
    public int maxHealthPoints;
    public int costToIncreaseMaxHP;
    public int currentHealthPoints;
    public int maxMana;
    public int costToIncreaseMaxMana;
    public int currentMana;
     
    public Character() { }

    public Character(Race race, Type type)
    {
        this.race = race;
        this.type = type;
        NewCharacter();
        baseStats(race);
        baseStats(type);
    }

    void Start()
    {
        if(transform.name!= "PlayerPortrait")
        {
            StartDropZones();
            currentHealthPoints = maxHealthPoints;
            currentMana = maxMana;
        }
    }

    private void NewCharacter()
    {
        level = 1;
        experience = 0;
        experienceToLevel = 20;
        skillPoints = 0;
        attack = 0; 
        defense = 0;
        regeneration = 0;
        handSlots = 5;
        buffSlots = 1;
        playSlots = 2;
        drawCount = 5;
        discardSlots = 1;
        maxDeckSize = 10;
    }
    
    /// <summary>
    /// set base stats for Race race
    /// </summary>
    /// <param name="race">race to set stats for</param>
    private void baseStats(Race race)
    {
        switch (race)
        {
            case Race.Human:

                break;
            case Race.Orc:
                break;
            case Race.Elf: 
                break;
            default: break;
        }
    }

    /// <summary>
    /// set base stats for Type type
    /// </summary>
    /// <param name="type"></param>
    private void baseStats(Type type)
    {
        switch(type)
        {
            case Type.Ranged:
                maxHealthPoints = 50;
                maxMana = 40;
                break;
            case Type.Caster:
                maxHealthPoints = 30;
                maxMana = 60;
                break;
            case Type.Fighter:
                maxHealthPoints = 60;
                maxMana = 30;
                break;
            default: break;
        }
    }
    
    /// <summary>
    /// set card limit for each dropzone
    /// </summary>
    void StartDropZones()
    {
        //hand
        gameObject.GetComponentsInChildren<DropZone>()[3].setMaxSlots(handSlots);
        gameObject.GetComponentsInChildren<DropZone>()[3].ResetAvailablePlayerHandCardSlots();
        //buff
        gameObject.GetComponentsInChildren<DropZone>()[2].setMaxSlots(buffSlots);
        gameObject.GetComponentsInChildren<DropZone>()[2].ResetAvailablePlayerHandCardSlots();
        //active - some randon big number - there should be no technical limit
        gameObject.GetComponentsInChildren<DropZone>()[1].setMaxSlots(15);
        gameObject.GetComponentsInChildren<DropZone>()[1].ResetAvailablePlayerHandCardSlots();
        //play
        gameObject.GetComponentsInChildren<DropZone>()[0].setMaxSlots(playSlots);
        gameObject.GetComponentsInChildren<DropZone>()[0].ResetAvailablePlayerHandCardSlots();
        //discard?
        GameObject.Find("Discard").GetComponent<DropZone>().setMaxSlots(discardSlots);
        GameObject.Find("Discard").GetComponent<DropZone>().ResetAvailablePlayerHandCardSlots();
    }

    public int GetRegeneration()
    {
        return regeneration;
    }
    public int GetAttack()
    {
        return attack;
    }

    public int GetDefense()
    {
        return defense;
    }


    private void LevelUp()
    {
        if (experience > experienceToLevel)
        {
            UpdateXP();
            level++;
            skillPoints += 5;
            maxDeckSize++;
            LevelUp();
        }
    }
    void UpdateXP()
    {
        if (level < 5)
        {
            experienceToLevel += 20;
        }
        else
        {
            experienceToLevel += 150;
        }
    }











    ////Meta details - skills leveled up with level
    public void increaseAttack(int n)
    {
        attack += n;
    }
    public void increaseDefense(int n)
    {
        defense += n;
    }
    public void increaseRegeneration(int n)
    {
        regeneration += n;
    }

    public void increaseMana(int n)
    {
        maxMana += n;
    }
    public void increaseHP(int n)
    {
        maxHealthPoints += n;
    }
}
