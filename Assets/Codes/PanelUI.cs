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

    public GameObject Atk_Panel;
    public GameObject Atk_Btn1;
    public GameObject Atk_Btn2;
    public GameObject Atk_Btn3;
    public GameObject Atk_BackBtn;






    // Start is called before the first frame update
    void Start()
    {
        HandleVisibilityFirst(true);
    }

    // Update is called once per frame
    void Update()
    {
        HandleVisibility();
    }

    public void Skill()
    {

        First_Panel.SetActive(false);
        Atk_Panel.SetActive(false);
        Skill_Panel.SetActive(true);
    }
    public void Atk()
    {

        First_Panel.SetActive(false);
        Skill_Panel.SetActive(false);
        Atk_Panel.SetActive(true);
    }

    public void Back_to_First()
    {

        Atk_Panel.SetActive(false);
        Skill_Panel.SetActive(false);
        First_Panel.SetActive(true);
    }


    public void HandleVisibility()
    {
        if (First_Panel.activeSelf)
        {
            HandleVisibilityFirst(true);

            HandleVisibilitySkill(false);
            HandleVisibilityAtk(false);
        }
        else if (Skill_Panel.activeSelf)
        {

            HandleVisibilitySkill(true);

            HandleVisibilityFirst(false);
            HandleVisibilityAtk(false);

        }
        else if (Atk_Panel.activeSelf)
        {
            HandleVisibilityAtk(true);

            HandleVisibilityFirst(false);
            HandleVisibilitySkill(false);

        }
    }

    void HandleVisibilityFirst(bool boolean)
    {
        First_Panel.SetActive(boolean);
        First_FleeBtn.SetActive(boolean);
        First_AtkBtn.SetActive(boolean);
        First_ItemBtn.SetActive(boolean);
        First_SkillBtn.SetActive(boolean);
    }

    void HandleVisibilitySkill(bool boolean)
    {
        Skill_Panel.SetActive(boolean);
        Skill_Btn1.SetActive(boolean);
        Skill_Btn2.SetActive(boolean);
        Skill_Btn3.SetActive(boolean);
        Skill_BackBtn.SetActive(boolean);
    }

    void HandleVisibilityAtk(bool boolean)
    {
        Atk_Panel.SetActive(boolean);
        Atk_Btn1.SetActive(boolean);
        Atk_Btn2.SetActive(boolean);
        Atk_Btn3.SetActive(boolean);
        Atk_BackBtn.SetActive(boolean);
    }
}