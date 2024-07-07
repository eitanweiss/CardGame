using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character : MonoBehaviour
{
    public Race race;
    public Type type;


    public int level = 1;
    public int experience = 0;
    public int experienceToLevel = 20;
    public int skillPoints = 0;
    //public int skills;
    public int maxHealthPoints;
    public int currentHealthPoints;
    public int maxMana;
    public int currentMana;
    public int manaMultiplier;
    public int attack = 0;
    public int defense = 0;
    public int regeneration = 0;
    public int handSlots = 5;
    public int buffSlots = 1;
    public int playSlots = 2;
    public int drawCount = 3;
    public int discardSlots= 1;
    public int maxDeckSize = 10;
    
    
    
    
    
    
    ////Meta details
    //public void increaseAttack(int n)
    //{
    //    attack += n;
    //}
    //public void increaseDefense(int n) 
    //{
    //    defense += n;
    //}
    //public void increaseRegeneration(int n)
    //{
    //    regeneration += n;
    //}
    //public void increaseMana(int n)
    //{
    //    mana += n* manaMultiplier;
    //}
    //public void increaseHP(int n)
    //{
    //    maxHealthPoints += n*2;
    //}
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
}
