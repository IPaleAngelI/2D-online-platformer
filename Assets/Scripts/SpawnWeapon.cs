using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Timers;
using Unity.VisualScripting;
using Photon.Pun;
using Photon.Realtime;
public class SpawnWeapon : MonoBehaviour
{
    public float minX, minY, maxX, maxY;

    public GameObject Shotgun;
    public Collider2D Collider;
    public Collider2D Collider2;
    public Collider2D ColliderShotgun;
    public int count = 1;
    
    // Start is called before the first frame update
    public void Start()
    {
        InvokeRepeating("SpawnGun", 3, UnityEngine.Random.Range(5, 15));
        

    }
    public void SpawnGun()
    {

        Vector3 randomPosition = new Vector3(UnityEngine.Random.Range(minX, maxX), UnityEngine.Random.Range(minY, maxY), 5);

        if (Collider.gameObject.name != "Platforms2" || Collider2.gameObject.name != "Platforms2")
        {
            if (Collider.gameObject.name != "Platforms" || Collider2.gameObject.name != "Platforms")
            {
                GameObject gun = PhotonNetwork.Instantiate(Shotgun.name, randomPosition, Quaternion.identity);
                
                gun.name = "Shotgun";
                count += 1;
                gun.tag = "Weapon";
                Update();
            }
        }
        else
        {
            randomPosition = new Vector3(UnityEngine.Random.Range(minX, maxX), UnityEngine.Random.Range(minY, maxY),5);
            SpawnGun();
        }
    }


    // Update is called once per frame
    void Update()
    {

       
    }
}
