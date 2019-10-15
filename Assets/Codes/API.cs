using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Networking;
using UnityEngine.UI;
using System.Text;
using System;


public class API : MonoBehaviour
{
    public Text text;
    // Start is called before the first frame update
    void Start()
    {

    }

    public void startCalling()
    {
        StartCoroutine(GetRequest("https://afternoon-spire-83789.herokuapp.com/books"));
    }

    IEnumerator GetRequest(string uri)
    {
        using (UnityWebRequest webRequest = UnityWebRequest.Get(uri))
        {
            // Request and wait for the desired page.
            yield return webRequest.SendWebRequest();

            string[] pages = uri.Split('/');
            int page = pages.Length - 1;

            if (webRequest.isNetworkError)
            {
                Debug.Log(pages[page] + ": Error: " + webRequest.error);
            }
            else
            {
                Debug.Log(pages[page] + ":\nReceived: " + webRequest.downloadHandler.text);
                text.text = webRequest.downloadHandler.text;
            }
        }
    }
}
