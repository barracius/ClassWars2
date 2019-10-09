using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuHandling : MonoBehaviour
{
    public GameObject cat;

    void Start()
    {
        cat.SetActive(false);
    }
    public void Profile_picPressed()
    {
        cat.SetActive(true);
    }

    public void Profile_close()
    {
        cat.SetActive(false);
    }

    public void Add_Friend()
    {

    }

    public void Logout_btn()
    {

    }

}
