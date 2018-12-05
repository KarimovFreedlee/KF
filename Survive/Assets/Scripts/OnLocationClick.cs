using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Assets.Scripts;

public class OnLocationClick : MonoBehaviour
{
    public Color coolor;
    public Text eventText;
    public Text eventStatus;
    public Text dayText;
    public Text playerHpText;
    public Text enemyHpText;
    public Text toMenuText;
    private int days = 1;
    int turn = 0;
    public Button restButton;
    public Button attackButton;
    public Button stationButton;
    public Button schoolButton;
    public Button policeButton;
    public Button hospitalnButton;
    public Button goDeeper;
    public Button BackToSHelter;
    Characters playerCharacter = new Characters("Player", 20, 3, 5);
    Undead undead = new Undead();

    private void Start()
    {
        playerHpText.text = "Your hp: " + playerCharacter.hp;
    }
    private void LocationDis()
    {
        schoolButton.interactable = false;
        hospitalnButton.interactable = false;
        stationButton.interactable = false;
        policeButton.interactable = false;
        
    }
    private void LocationOn()
    {
        schoolButton.interactable = true;
        hospitalnButton.interactable = true;
        stationButton.interactable = true;
        policeButton.interactable = true;
        
    }
    public void Onclick()
    {
        LocationDis();
        restButton.interactable = false;
        if (turn <3)
        {

            float result = Random.Range(1, 100);
            if (result < 60)
            {
                undead.RandNum();
                enemyHpText.text = (undead.Name + " hp: " + undead.hp);
                eventStatus.text = ("Your enemy is " + undead.Name + " hp: " + undead.hp + " Attack " + undead.minDamage + "-" + undead.maxDamage+" Your attack: "+playerCharacter.minDamage+" - "+playerCharacter.maxDamage);
                attackButton.interactable = true;
                eventText.text = "You have found enemy!";

                BackToSHelter.interactable = false;
                goDeeper.interactable = false;
            }
            else if (result < 75 && result > 59)
            {
                eventText.text = "You have found survivor!";
                turn++;
                BackToSHelter.interactable = true;
                goDeeper.interactable = true;
            }
            else if (result > 74)
            {
                turn++;
                eventText.text = "You have found item!";
                BackToSHelter.interactable = true;
                goDeeper.interactable = true;
            }
        }
        else
        {
            eventStatus.text = "You can't search today anymore! Going back to shelter.";
            OnBackPress();
            LocationDis();
            turn = 0;
        }
       

    }
    public void OnAttackPress()
    {

        enemyHpText.text = (undead.Name + " hp: " + undead.hp);
        undead.hp -= Random.Range(playerCharacter.minDamage, playerCharacter.maxDamage);

        if (undead.hp <= 0)
        {
            eventText.text = ("You have killed " + undead.Name);
            attackButton.interactable = false;
            goDeeper.interactable = true;
            BackToSHelter.interactable = true;
            turn++;

        }
        else
        {
            playerCharacter.hp -= Random.Range(undead.minDamage, undead.maxDamage);
            if (playerCharacter.hp <= 0)
            {
                attackButton.interactable = false;
                eventStatus.text = undead.Name + "'d killed you! Game over!";
                BackToSHelter.interactable = true;
                toMenuText.text = "Back to menu";
            }

        }
        if(undead.hp<=0)
        {
            enemyHpText.text = (undead.Name + " hp: 0");
        }
        else
        {
            enemyHpText.text = (undead.Name + " hp: " + undead.hp);
        }
        if (playerCharacter.hp<=0)
        {
            playerHpText.text = "Your hp: 0";
        }
        else
        {
            playerHpText.text = "Your hp: " + playerCharacter.hp;
        }
    }

    public void OnRestPress()
    {
        days++;
        dayText.text = "Day " + days;
        playerCharacter.hp = 20;
        playerHpText.text = "Your hp: " + playerCharacter.hp;
        restButton.interactable = false;
        LocationOn();
        eventStatus.text = "";
        if (days==5)
        {
            eventStatus.text = "You died from sturving!";
            LocationDis();
            BackToSHelter.interactable = true;
            toMenuText.text = "Back to menu";            
        }
    }

    public void OnBackPress()
    {
        attackButton.interactable = false;
        goDeeper.interactable = false;
        BackToSHelter.interactable = false;
        LocationOn();
        restButton.interactable = true;
        eventText.text = "You're in the shelter";
        enemyHpText.text = "";
    }
    public void BakcToMenu()
    {
        if (playerCharacter.hp <= 0)
        {
            Application.LoadLevel("Menu");
        }
        else
        {
            if (days == 5) {
                Application.LoadLevel("Menu");
            }
        }

    }
}
