using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Online : MonoBehaviourPunCallbacks
{
    // Start is called before the first frame update
    public GameObject bar;

    void Start()
    {
        InvokeRepeating("loading", 1, 0.045f);
    }
    private void Connect()
    {
        PhotonNetwork.ConnectUsingSettings();
    }
    void loading()
    {
        var b = bar.GetComponent<Slider>();
        b.value += 1.5f;
        Connect();
    }


    public override void OnConnectedToMaster()
    {
        SceneManager.LoadScene("Menu");
    }



// Update is called once per frame
void Update()
    {
        
    }
}
