using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleHandler : MonoBehaviour
{
    public GameObject player;

    public GameObject enemy;

    public int playerDmg;
    public int enemyDmg;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AgarrarseAVergazos()
    {
        Monsters monster = enemy.GetComponent<Monsters>();
        Character character = player.GetComponent<Character>();
        CalculateDmgEnemy(monster);
        CalculateDmgPlayer(character);
        Debug.Log("player:" + playerDmg.ToString());
        Debug.Log("enemy:" + enemyDmg.ToString());
        character.curHP -= enemyDmg;
        monster.curHP -= playerDmg;
        Debug.Log("player HP:" + character.curHP.ToString());
        Debug.Log("enemy HP:" + monster.curHP.ToString());

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
