using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelUI : MonoBehaviour
{


    public GameObject First_Panel;

    public GameObject First_FleeBtn;
    public GameObject First_AtkBtn;
    public GameObject First_ItemBtn;
    public GameObject First_SkillBtn;



    public GameObject Skill_Panel;
    public GameObject Skill_Btn1;
    public GameObject Skill_Btn2;
    public GameObject Skill_Btn3;
    public GameObject Skill_BackBtn;






    // Start is called before the first frame update
    void Start()
    {
        First_Panel.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        HandleVisibility();
    }

    public void Skill()
    {
        First_Panel.SetActive(false);
        Skill_Panel.SetActive(true);
    }

    public void Atras()
    {
        First_Panel.SetActive(true);
        Skill_Panel.SetActive(false);
    }

    public void HandleVisibility()
    {
        if (First_Panel.activeSelf)
        {
            First_FleeBtn.SetActive(true);
            First_AtkBtn.SetActive(true);
            First_ItemBtn.SetActive(true);
            First_SkillBtn.SetActive(true);

            Skill_Panel.SetActive(false);
            Skill_Btn1.SetActive(false);
            Skill_Btn2.SetActive(false);
            Skill_Btn3.SetActive(false);
            Skill_BackBtn.SetActive(false);
        }
        else if (Skill_Panel.activeSelf)
        {
            Skill_Btn1.SetActive(true);
            Skill_Btn2.SetActive(true);
            Skill_Btn3.SetActive(true);
            Skill_BackBtn.SetActive(true);

            First_Panel.SetActive(false);
            First_FleeBtn.SetActive(false);
            First_AtkBtn.SetActive(false);
            First_ItemBtn.SetActive(false);
            First_SkillBtn.SetActive(false);


        }
    }
}