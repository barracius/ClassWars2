using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventScene : MonoBehaviour
{

    public GameObject gamehandler;

    private int attackTime = 30;
    private bool attacking;
    private int skilltime = 60;
    private int enemyAttackTime = 60;
    private bool enemyAttack;
    private int waitTime = 30;
    private bool wait;
    private bool skilling;
    public Animator animator;
    private Animator animatorE;
    public FloatValue currentHealth;
    public Signal playerHealthSignal;

    public int hola;
    public GameObject player;
    public GameObject enemy;
    public GameObject exlamation;
    public GameObject search;
    public GameObject move;
    public GameObject skill;
    public GameObject rest;
    public GameObject attack;
    public GameObject skill1;
    public GameObject dialogBox;
    public GameObject up;
    public GameObject down;
    public GameObject right;
    public GameObject left;
    public GameObject menu;

    public Text dialogText;

    public string dialog;

    public bool dialogActive;

    public bool inBox;

    public bool afterFightP;
    public bool afterFightN;
    private bool player2In;

    private bool parch;

    private bool beginFightP;
    private bool beginFightN;


    public int playerDmg;
    public int enemyDmg;

    public GameObject healthbar_fg;
    public GameObject manabar_fg;


    // Start is called before the first frame update
    void Start()
    {
        animator = player.GetComponent<Animator>();
        animatorE = enemy.GetComponent<Animator>();

        HealthbarController hbController = healthbar_fg.GetComponent<HealthbarController>();
        HealthbarController mbController = manabar_fg.GetComponent<HealthbarController>();

        Character character = player.GetComponent<Character>();
        hbController.maxHP = character.maxHP;
        hbController.Health = character.curHP;
        hbController.onTakeDmg(0);
        mbController.maxHP = character.maxMP;
        mbController.Health = character.curMP;
        mbController.onTakeDmg(0);
        print(hbController.Health.ToString());

    }

    // Update is called once per frame
    void Update()
    {

        if (enemyAttack)
        {
            enemyAttackTime -= 1;
            if (enemyAttackTime == 0)
            {
                animatorE.SetBool("attacking", false);
                enemyAttack = false;
                enemyAttackTime = 60;
                TurnOnFight();
            }
        }
        if (wait && !afterFightN)
        {
            waitTime -= 1;
            if (waitTime == 0)
            {
                animatorE.SetBool("attacking", true);
                if (!afterFightN)
                {
                    enemyAttack = true;
                }

                waitTime = 30;
                wait = false;
            }

        }
        if (attacking)
        {
            attackTime -= 1;
            //Debug.Log(attackTime);
            if (attackTime == 0)
            {
                //Debug.Log("whit");

                attacking = false;
                animator.SetBool("attacking", false);
                attackTime = 30;
                wait = true;
            }

        }
        if (skilling)
        {
            skilltime -= 1;
            if (skilltime == 0)
            {
                //Debug.Log("whit");

                skilling = false;
                animator.SetBool("skill", false);
                skilltime = 20;
                wait = true;
            }

        }
        if (player2In && !player.GetComponent<PlayerMovement>().moving && parch)
        {
            dialogActive = true;
            dialogBox.SetActive(true);
            dialog = "You have encountered another player, get ready to rumble!";
            dialogText.text = dialog;
            parch = false;
            beginFightP = true;
            TurnOffMenu();
        }
        if (Input.GetMouseButton(0))
        {
            if (dialogActive)
            {

                TurnOnMenu();
                dialogActive = false;
                dialogBox.SetActive(false);
                if (enemy)
                {
                    exlamation.SetActive(false);
                    enemy.SetActive(true);
                    TurnOffMenu();
                    if (currentHealth.initialValue > 0)
                    {
                        currentHealth.initialValue -= 1;
                        playerHealthSignal.Raise();
                    }
                }

                if (beginFightP)
                {
                    TurnOffMenu();
                    TurnOnFight();
                    Debug.Log("Fight wih user");
                }

                if (beginFightN)
                {
                    TurnOffMenu();
                    TurnOnFight();
                    Debug.Log("Fight wih NPC");
                }

                if (afterFightN)
                {
                    //fight.SetActive(false);
                    enemy.SetActive(false);
                    TurnOnMenu();
                    GameHandler gh = gamehandler.GetComponent<GameHandler>();
                    gh.actions = gh.maxturns;
                }
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.name == "Player")
        {


            inBox = true;
            Debug.Log("player in scene");
            if (player2In)
            {
                parch = true;
            }
        }

        if (other.name == "Player2")
        {
            player2In = true;
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.name == "Player")
        {
            inBox = false;
            Debug.Log("player out scene");
        }
    }

    public void Search()
    {
        Debug.Log(inBox);

        if (inBox)
        {
            if (enemy)
            {
                exlamation.SetActive(true);
                beginFightN = true;
            }
            TurnOffMenu();
            dialogActive = true;
            dialogBox.SetActive(true);
            dialogText.text = dialog;
            dialog = "nothing";
        }
    }

    public void Fight()
    {
        animator.SetFloat("moveX", -1);
        animator.SetFloat("moveY", 0);
        animator.SetBool("attacking", true);
        attacking = true;
        TurnOffFight();
        //fight.SetActive(false);
        Debug.Log("Fighting");

        HealthbarController hbController = healthbar_fg.GetComponent<HealthbarController>();
        Character character = player.GetComponent<Character>();
        Monsters monster = enemy.GetComponent<Monsters>();
        if (character.curHP >= 0)
        {
            // Monsters monster = enemy.GetComponent<Monsters>();
            // Character character = player.GetComponent<Character>();
            CalculateDmgEnemy(monster);
            CalculateDmgPlayer(character);
            if (character.curHP - enemyDmg <= 0 || monster.curHP - playerDmg <= 0)
            {

                afterFightN = true;
                animatorE.SetBool("attacking", false);

                dialogActive = true;
                TurnOffFight();
                dialogBox.SetActive(true);
                dialog = "You have defeated the log!";
                dialogText.text = dialog;



            }
            else
            {
                Debug.Log("player:" + playerDmg.ToString());
                Debug.Log("enemy:" + enemyDmg.ToString());
                character.curHP -= enemyDmg;
                hbController.onTakeDmg(enemyDmg);

                monster.curHP -= playerDmg;

                Debug.Log("player HP:" + character.curHP.ToString());
                Debug.Log("enemy HP:" + monster.curHP.ToString());
            }


        }
        // if (monster.curHP <= 0)
        // {
        //     dialogActive = true;
        //     dialogBox.SetActive(true);
        //     dialog = "You have defeated the log!";
        //     dialogText.text = dialog;

        //     afterFightN = true;

        // }


    }

    void CalculateDmgPlayer(Character p1)
    {
        Debug.Log(p1.CharClass.ToString());

        if (p1.CharClass.ToString() == "Mage")
        {
            playerDmg = p1.intel * 2 + p1.dex;
        }
        else if (p1.CharClass.ToString() == "Warrior")
        {
            playerDmg = p1.str * 2 + p1.dex;
        }
        else if (p1.CharClass.ToString() == "Hunter")
        {
            playerDmg = p1.dex * 3;
        }
    }
    void CalculateDmgEnemy(Monsters e1)
    {
        enemyDmg = e1.str + e1.dex + e1.intel;

    }

    public void TurnOnFight()
    {
        attack.SetActive(true);
        skill1.SetActive(true);
    }

    public void TurnOffFight()
    {
        attack.SetActive(false);
        skill1.SetActive(false);
    }
    public void TurnOffMenu()
    {
        rest.SetActive(false);
        move.SetActive(false);
        skill.SetActive(false);
        search.SetActive(false);
    }

    public void TurnOnMenu()
    {
        rest.SetActive(true);
        move.SetActive(true);
        skill.SetActive(true);
        search.SetActive(true);
    }

    public void TurnOffMovement()
    {
        right.SetActive(false);
        left.SetActive(false);
        up.SetActive(false);
        down.SetActive(false);
        menu.SetActive(false);

    }

    public void Skill()
    {
        animator.SetFloat("moveX", -1);
        animator.SetFloat("moveY", 0);
        animator.SetBool("skill", true);
        skilling = true;
        //fight.SetActive(false);
        Debug.Log("Fighting");
        HealthbarController hbController = healthbar_fg.GetComponent<HealthbarController>();
        HealthbarController mbController = manabar_fg.GetComponent<HealthbarController>();

        Character character = player.GetComponent<Character>();

        Monsters monster = enemy.GetComponent<Monsters>();
        if (character.curHP >= 0)
        {
            // Monsters monster = enemy.GetComponent<Monsters>();
            // Character character = player.GetComponent<Character>();
            CalculateDmgEnemy(monster);
            CalculateDmgPlayer(character);
            if (character.curHP - enemyDmg <= 0 || monster.curHP - playerDmg * 2 <= 0)
            {
                afterFightN = true;
                animatorE.SetBool("attacking", false);

                dialogActive = true;
                TurnOffFight();

                dialogActive = true;
                dialogBox.SetActive(true);
                dialog = "You have defeated the log!";
                dialogText.text = dialog;



            }
            else
            {
                Debug.Log("player:" + playerDmg.ToString());
                Debug.Log("enemy:" + enemyDmg.ToString());
                monster.curHP -= playerDmg * 2;
                character.curMP -= 10;
                mbController.onTakeDmg(10);
                character.curHP -= enemyDmg;
                hbController.onTakeDmg(enemyDmg);

                Debug.Log("player HP:" + character.curHP.ToString());
                Debug.Log("enemy HP:" + monster.curHP.ToString());
            }


        }
    }


}
