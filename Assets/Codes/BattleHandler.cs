using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleHandler : MonoBehaviour
{
    public GameObject gamehandler;
    public GameObject player;
    public GameObject healthbar_fg;

    public GameObject enemy;

    public int playerDmg;
    public int enemyDmg;

    // Start is called before the first frame update
    void Start()
    {
        
        HealthbarController hbController = healthbar_fg.GetComponent<HealthbarController>();
        
        Character character = player.GetComponent<Character>();
        hbController.maxHP = character.maxHP;
        hbController.Health = character.curHP;
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AgarrarseAVergazos()
    {
        Monsters monster = enemy.GetComponent<Monsters>();
        Character character = player.GetComponent<Character>();
        
        HealthbarController hbController = healthbar_fg.GetComponent<HealthbarController>();
        CalculateDmgEnemy(monster);
        CalculateDmgPlayer(character);
        if (character.curHP - enemyDmg <= 0 || monster.curHP - playerDmg <= 0)
        {
            GameHandler gh = gamehandler.GetComponent<GameHandler>();
            gh.actions = gh.maxturns;

        }
        else
        {
            Debug.Log("player:" + playerDmg.ToString());
            Debug.Log("enemy:" + enemyDmg.ToString());
            //GameObject HB = character.GetComponent<GameObject>();
            
            character.curHP -= enemyDmg;
            monster.curHP -= playerDmg;
            hbController.onTakeDmg(enemyDmg);
            Debug.Log("player HP:" + character.curHP.ToString());
            Debug.Log("enemy HP:" + monster.curHP.ToString());
        }


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
}
