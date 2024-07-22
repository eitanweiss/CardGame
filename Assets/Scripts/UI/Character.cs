using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField] private Race race;
    [SerializeField] private Type type;
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
    public int buffSlots;
    public int playSlots;
    public int drawCount;
    public int discardSlots;
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
        drawCount = 3;
        discardSlots = 1;
        maxDeckSize = 10;
    }
    
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
    
    private void LevelUp()
    {
        if (experience>experienceToLevel)
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
        if(level<5)
        {
            experienceToLevel += 20;
        }
        else
        {
            experienceToLevel += 150;
        }
    }
    ////Meta details
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
