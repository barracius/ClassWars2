using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleHandler : MonoBehaviour
{
    public GameObject gamehandler;
    public GameObject player;
    public GameObject healthbar_fg;

    private bool playerAttacking;
    private bool enemyAttacking;
    private int attackTime = 15;
    public GameObject enemy;

    private Animator animator;
    private Animator animatorE;
    public int playerDmg;
    public int enemyDmg;

    // Start is called before the first frame update
    void Start()
    {
        // Animator animator = player.GetComponent<Animator>();
        // Animator animatorE = enemy.GetComponent<Animator>();

    }

    // Update is called once per frame
    void Update()
    {

        // if (enemyAttacking)
        // {
        //     Debug.Log(attackTime);
        //     if (attackTime == 0)
        //     {
        //         Debug.Log("whit");

        //         attacking = false;
        //         animatorE.SetBool("attacking", false);
        //         attackTime = 15;
        //     }
        //     attackTime -= 1;
        // }
    }

    // public void AgarrarseAVergazos()
    // {
    //     Monsters monster = enemy.GetComponent<Monsters>();
    //     Character character = player.GetComponent<Character>();
    //     CalculateDmgEnemy(monster);
    //     CalculateDmgPlayer(character);
    //     if (character.curHP - enemyDmg <= 0 || monster.curHP - playerDmg <= 0)
    //     {
    //         GameHandler gh = gamehandler.GetComponent<GameHandler>();
    //         gh.actions = gh.maxturns;

    //     }
    //     else
    //     {
    //         Debug.Log("player:" + playerDmg.ToString());
    //         Debug.Log("enemy:" + enemyDmg.ToString());
    //         character.curHP -= enemyDmg;
    //         monster.curHP -= playerDmg;
    //         Debug.Log("player HP:" + character.curHP.ToString());
    //         Debug.Log("enemy HP:" + monster.curHP.ToString());
    //     }


    // }
    // void CalculateDmgPlayer(Character p1)
    // {
    //     Debug.Log(p1.CharClass.ToString());

    //     if (p1.CharClass.ToString() == "Mage")
    //     {
    //         playerDmg = p1.intel * 2 + p1.dex;
    //     }
    //     else if (p1.CharClass.ToString() == "Warrior")
    //     {
    //         playerDmg = p1.str * 2 + p1.dex;
    //     }
    //     else if (p1.CharClass.ToString() == "Hunter")
    //     {
    //         playerDmg = p1.dex * 3;
    //     }
    // }
    // void CalculateDmgEnemy(Monsters e1)
    // {
    //     enemyDmg = e1.str + e1.dex + e1.intel;

    // }
}
