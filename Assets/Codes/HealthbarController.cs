using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarController : MonoBehaviour
{
    public Image HealthBar;
    public int Health;
    public int maxHP;

    public void Start()
    {
        HealthBar.fillAmount = (float)Health / maxHP;
    }

    public void onTakeDmg(int dmg)
    {
        Health -= dmg;
        HealthBar.fillAmount = (float)Health / maxHP;
    }



    //print(Player.GetComponent<Character>().maxHP);

    

    //Player.OnTakeDmg(24);

}
