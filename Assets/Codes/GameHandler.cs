using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameHandler : MonoBehaviour
{
    public GameObject battlehandler;
    public int globalRound;
    public int playerturn;
    public bool isplaying;
    public GameObject canvas;
    public GameObject pauseScreen;
    public GameObject ev;



    public int maxturns = 3;

    public int actions;
    void Start()
    {
        //Time.timeScale = 1;
        setplayerTurn();
        setglobalRound();
    }

    // Update is called once per frame
    void Update()
    {
        checkisTurn();
        if (actions == maxturns)
        {
            actions = 0;
            //send to DB globalRound +1
            if (playerturn == 2)
            {
                globalRound++;
                //ev.TurnOnMenu();
                
            }
        }

    }

    public void finish(){
        actions = maxturns;
    }
    public void sumActions()
    {
        actions++;
    }
    public void passTurn()
    {
        actions = maxturns;
    }

    void setglobalRound()
    {
        globalRound = 4;
        //Call from DB GlobalRound
    }

    void setplayerTurn()
    {
        playerturn = 2;
        //Call from DB playerturn 
    }

    void checkisTurn()
    {
        if (globalRound % playerturn == 0)
        {

            canvas.SetActive(true);

            //pauseScreen.SetActive(false);
            isplaying = true;
        }
        else
        {

            //Pause Game
            //pauseScreen.SetActive(true);

            canvas.SetActive(false);
            isplaying = false;
        }
    }

}
