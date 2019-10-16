using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadPlayers : MonoBehaviour
{
    public Vector3 playereSpawn;
    public GameObject player2;
    // Start is called before the first frame update
    void Start()
    {
        playereSpawn = new Vector3(0, 12, 0);
        Instantiate(Resources.Load("Player"), playereSpawn, Quaternion.identity).name = "Player2";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
