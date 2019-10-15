using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthbarController : MonoBehaviour
{
    public Image HealthBar;
    public float Health;
    public float maxHP;

    public void OnTakeDmg(int dmg)
    {
        Health = Health - dmg;
        HealthBar.fillAmount = Health/maxHP;

    }

}
