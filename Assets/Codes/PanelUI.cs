using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PanelUI : MonoBehaviour
{

    public GameObject BotHuir;
    public GameObject BotAtacar;
    public GameObject BotUsar;
    public GameObject BotP1;
    public GameObject BotP2;
    public GameObject BotP3;
    public GameObject Volver;



    // Start is called before the first frame update
    void Start()
    {
        BotP1.SetActive(false);
        BotP2.SetActive(false);
        BotP3.SetActive(false);
        Volver.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Attack()
    {
        BotHuir.SetActive(false);
        BotUsar.SetActive(false);
        BotAtacar.SetActive(false);
        BotP1.SetActive(true);
        BotP2.SetActive(true);
        BotP3.SetActive(true);
        Volver.SetActive(true);
    }

    public void Atras()
    {
        BotHuir.SetActive(true);
        BotUsar.SetActive(true);
        BotAtacar.SetActive(true);
        BotP1.SetActive(false);
        BotP2.SetActive(false);
        BotP3.SetActive(false);
        Volver.SetActive(false);
    }
}
