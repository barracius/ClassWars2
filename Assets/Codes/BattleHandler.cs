using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleHandler : MonoBehaviour
{
    public GameObject player;

    public GameObject enemy;

    private float playerDmg;
    private float enemyDmg;

    // Start is called before the first frame update
    void Start()
    {
        Character character = player.GetComponent<Character>();
        Monsters monster = enemy.GetComponent<Monsters>();
        CalculateDmgEnemy(monster);
        CalculateDmgPlayer(character);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void AgarrarseAVergazos()
    {
        Debug.Log("player:" + playerDmg.ToString());
        Debug.Log("enemy:" + enemyDmg.ToString());
    }
    void CalculateDmgPlayer(Character p1)
    {
        if (p1.CharClass == "Mage")
        {
            playerDmg = p1.intel * 2 + p1.dex;
        }
        else if (p1.CharClass == "Warrior")
        {
            playerDmg = p1.str * 2 + p1.dex;
        }
        else if (p1.CharClass == "Hunter")
        {
            playerDmg = p1.dex * 3;
        }
    }
    void CalculateDmgEnemy(Monsters e1)
    {
        enemyDmg = e1.str + e1.dex + e1.intel;

    }
}
