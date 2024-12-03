using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Photon.Pun;

public class Col : MonoBehaviour
{
    public int c = 0;
    private Inventory inv;

    public DataBase data;

    private SpawnWeapon guns;
    PhotonView view;
    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
        guns = FindObjectOfType<SpawnWeapon>().GetComponent<SpawnWeapon>();
        inv = FindObjectOfType<Inventory>().GetComponent<Inventory>();
    }
    public void OnCollisionEnter2D(Collision2D collision)
    {


        if (collision.gameObject.name == "Shotgun")
        {
            if (!view.IsMine) return;
            inv.AddItem(0, data.items[1], inv.items[0].count + 1);
            guns.count--;
            inv.UpdateInventory();
            PhotonNetwork.Destroy(collision.gameObject);
        }
        
        
        
    }

    // Update is called once per frame
    void Update()
    {
        inv = FindObjectOfType<Inventory>().GetComponent<Inventory>();
        

    }
}
