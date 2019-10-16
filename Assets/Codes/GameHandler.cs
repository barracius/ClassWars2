using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Notifications.Android;


public class GameHandler : MonoBehaviour
{
    public GameObject battlehandler;
    public int globalRound;
    public int playerturn;
    public bool isplaying;
    public GameObject canvas;
    public GameObject pauseScreen;

    public int maxturns = 3;

    public int actions;
    void Start()
    {
        var c = new AndroidNotificationChannel()
        {
            Id = "channel_id",
            Name = "Default Channel",
            Importance = Importance.High,
            Description = "Generic notifications",
        };
        AndroidNotificationCenter.RegisterNotificationChannel(c);
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

            }
        }

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

            pauseScreen.SetActive(false);
            //oye ctm es tu turno
            cellnotify();
            isplaying = true;
        }
        else
        {

            //Pause Game
            pauseScreen.SetActive(true);

            canvas.SetActive(false);
            isplaying = false;
        }
    }

    void cellnotify(){
        
      var notification = new AndroidNotification();
        notification.Title = "SomeTitle";
        notification.Text = "SomeText";
        notification.FireTime = System.DateTime.Now.AddSeconds(30);

        AndroidNotificationCenter.SendNotification(notification, "channel_id");}

}
