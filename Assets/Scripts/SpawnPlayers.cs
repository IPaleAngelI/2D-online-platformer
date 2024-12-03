using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.TextCore.Text;
using Photon.Realtime;

public class SpawnPlayers : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject camera;
    int v = 1;
    public GameObject player;
    public GameObject player2;
    public float minX, minY, maxX, maxY;

    public GameObject cost;

    void Start()
    {
        Vector2 randomPosition = new Vector2(Random.Range(minX, minY), Random.Range(maxX, maxY));
        int n = Random.Range(1, 2);
        if (n ==1)
        {
            GameObject char1 = PhotonNetwork.Instantiate(player.name, randomPosition, Quaternion.identity);
            char1.name = "Character";
            var a = char1.GetComponent<Col>();
            var b = camera.GetComponent<DataBase>();
            a.data = b;
        }
        if (n == 2)
        {
            GameObject char2 = PhotonNetwork.Instantiate(player2.name, randomPosition, Quaternion.identity);
            char2.name = "Character2";
            var a1 = char2.GetComponent<Col>();
            var b1 = camera.GetComponent<DataBase>();
            a1.data = b1;
        }

        

        
        
       
        
        
        
    }

    // Update is called once per frame

}
