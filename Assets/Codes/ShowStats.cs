using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowStats : MonoBehaviour
{
    public GameObject Panel;
    public GameObject Player;

    public GameObject classname;
    public GameObject maxHP;
    public GameObject maxMP;
    public GameObject curHP;
    public GameObject curMP;
    public GameObject str;
    public GameObject intel;
    public GameObject dex;
    public int hp;

    public bool SetScript = false;

    public void OpenPanel()
    {
        if (Panel != null)
        {
            bool isActive = Panel.activeSelf;

            Panel.SetActive(!isActive);
        }
        
        Character stats = Player.GetComponent<Character>();
        classname.GetComponent<UnityEngine.UI.Text>().text = stats.classname.ToString();
        maxHP.GetComponent<UnityEngine.UI.Text>().text = "Max HP: " + stats.maxHP.ToString();
        maxMP.GetComponent<UnityEngine.UI.Text>().text = "Max MP: " + stats.maxMP.ToString();
        curHP.GetComponent<UnityEngine.UI.Text>().text = "Current HP: " + stats.curHP.ToString();
        curMP.GetComponent<UnityEngine.UI.Text>().text = "Current HP: " + stats.curMP.ToString();
        str.GetComponent<UnityEngine.UI.Text>().text = "Strength: " + stats.str.ToString();
        intel.GetComponent<UnityEngine.UI.Text>().text = "Inteligence: " + stats.intel.ToString();
        dex.GetComponent<UnityEngine.UI.Text>().text = "Dexterity: " + stats.dex.ToString();

        
    }
}