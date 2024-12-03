using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Timers;
using Unity.VisualScripting;
using Photon.Pun;
using System.Numerics;
using Vector3 = UnityEngine.Vector3;

public class Inventory : MonoBehaviour

{
    public DataBase data;

    public List<ItemInventory> items = new List<ItemInventory>();

    public GameObject gameObjShow;
    public GameObject InventoryMainObject;
    public int maxCount;

    public Camera cam;
    public EventSystem es;

    public int currentID;
    public ItemInventory currentItem;

    public RectTransform movingObject;
    public Vector3 offset;

    public GameObject Canvas;

    public GameObject Character;

    public Sprite beb;
    public GameObject shot;
    int ficount = 0;
    PhotonView view;
    public void Start()
    {
        
        Character = GameObject.Find("Character");
        if (items.Count == 0)
        {
            AddGraphics();
        }
        for(int i = 0; i<maxCount; i++)
        {
            //AddItem(i, data.items[Random.Range(0, data.items.Count)], Random.Range(1,99));
        }
        UpdateInventory();


        view = GetComponent<PhotonView>();
        



    }



    public void Update()
    {


        if (movingObject.gameObject.active == true)
        {
            movingObject.gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 255);
        }
        else
        {
            movingObject.gameObject.GetComponent<Image>().color = new Color(0, 0, 0, 0);
        }
       // if (view.IsMine) 
       // {
            
            var FirstItem = GameObject.Find("0");   
            var ImageFI = FirstItem.GetComponent<Image>();

            if (ficount == 0)
            {
                if (ImageFI.sprite.name != "beb")
                {
                    if (ImageFI.sprite.name == "weaponR3")
                    {
                        Vector3 MyVector = new Vector3(0, 30, 0);
                        GameObject gun = PhotonNetwork.Instantiate("Main gun", MyVector, UnityEngine.Quaternion.identity);
                        gun.name = "Main gun";
                        ficount = 1;
                    }
                }
                       
            }
            else
            {
                if (ImageFI.sprite.name != "beb")
                {
                    if (ImageFI.sprite.name == "weaponR3")
                    {

                        var gun = GameObject.Find("Main gun");
                        var a = Character.GetComponent<PlayerMove>();

                        Vector3 rotate = transform.eulerAngles;

                        if (a.faceRight == true)
                        {
                            rotate.y = 0;
                            gun.transform.rotation = UnityEngine.Quaternion.Euler(rotate);
                            Vector3 posiy = new Vector3(Character.transform.position.x + 1.2f, Character.transform.position.y - 0.94f, 0);
                            gun.transform.position = posiy;
                        }
                        else
                        {
                            Vector3 posiy = new Vector3(Character.transform.position.x - 1.2f, Character.transform.position.y - 0.94f, 0);
                            gun.transform.position = posiy;
                            rotate.y = 190;
                            gun.transform.rotation = UnityEngine.Quaternion.Euler(rotate);

                        }
                        var b = cam.GetComponent<Inventory>();
                        b.Character = Character;

                    }

                }
                else
                {
                    var gun = GameObject.Find("Main gun");
                    if (gun != null)
                    {
                        PhotonNetwork.Destroy(gun);
                        ficount = 0;
                    }

                }
            }

                


        //}
        //else
        //{
        //    return;
        //}
        
        
        

        


        if (currentID != -1)
        {
            MoveObject();
        }
        if(Input.GetKeyDown(KeyCode.I)) 
        {
            
            if (Canvas.activeSelf == true)
            {
                Canvas.SetActive(false);
            }
            else
            {
                Canvas.SetActive(true);
                UpdateInventory();
            }


        }

        
    }

    public void SearchForSameItem(Item item, int count)
    {
        for(int i = 0;i<maxCount;i++)
        {
                if (items[i].id == item.id)
                {

            
                if (items[i].count < 128)
                {
                    items[i].count += count;
                    if (items[i].count > 128)
                    {
                        count = items[i].count-128;
                        items[i].count = 64;

                    }
                    else
                    {
                        count = 0;
                        i = maxCount;
                    }
                }
            }
        }
        if(count > 0)
        {
            for (int i=0;i<maxCount;i++)
            {
                if (items[i].id == 0)
                {
                    AddItem(i, item, count);
                    i =maxCount;
                }
            } 
        }
    }


    public void AddItem( int id, Item item, int count)
    {
        items[id].id = item.id;
        items[id].count = count;
        items[id].itemGameObj.GetComponent<Image>().sprite = item.img;
        if(count > 0 && item.id != 0)
        {
            
            Color mirr = new Color(255, 255, 255, 255);
            items[id].itemGameObj.GetComponent<Image>().color = mirr;
            items[id].itemGameObj.GetComponentInChildren<Text>().text = count.ToString();
        }
        else
        {
            items[id].itemGameObj.GetComponentInChildren<Text>().text = " ";
        }
        
    }

    public void AddInventoryItem(int id, ItemInventory invItem)
    {
        items[id].id = invItem.id;
        items[id].count = invItem.count;
        items[id].itemGameObj.GetComponent<Image>().sprite = data.items[invItem.id].img;
        if (invItem.count >= 1 && invItem.id != 0)
        {
            Color mirr = new Color(255, 255, 255, 255);
            items[id].itemGameObj.GetComponent<Image>().color = mirr;
            items[id].itemGameObj.GetComponentInChildren<Text>().text = invItem.count.ToString();
        }
        else
        {
            
            items[id].itemGameObj.GetComponentInChildren<Text>().text = " ";
        }

    }

    public void AddGraphics()
    {
        for (int i = 0; i<maxCount; i++)
        {
            GameObject newItem = Instantiate(gameObjShow, InventoryMainObject.transform) as GameObject;
            newItem.name = i.ToString();

            ItemInventory ii = new ItemInventory();
            ii.itemGameObj = newItem;

            var a = ii.itemGameObj.GetComponent<Image>();
            a.sprite = beb;
            ii.itemGameObj.GetComponentInChildren<Text>().text = " ";


            RectTransform rt = newItem.GetComponent<RectTransform>();


            rt.localPosition = new Vector3(0,0,0);
            rt.localScale = new Vector3(1,1,1);
            newItem.GetComponentInChildren<RectTransform>().localScale = new Vector3(1,1,1);

            
            Button tempButton = newItem.GetComponent<Button>();

            tempButton.onClick.AddListener(delegate { SelectObject(); });
            
            items.Add(ii);
        }

    }

    public void UpdateInventory()
    {
        

        for (int i = 0; i<maxCount;i++)
        {
            if (items[i].id != 0 && items[i].count >= 1)
            {
               
                items[i].itemGameObj.GetComponentInChildren<Text>().text = items[i].count.ToString();
            }
            else
            {
                Color mirr = new Color(0,0,0,0);
                items[i].itemGameObj.GetComponent<Image>().sprite = data.items[items[i].id].img;
                var a = items[i].itemGameObj.GetComponent<Image>();
                a.sprite = beb;
            }
        }
    }

    public void SelectObject()
    {
        if(currentID == -1)
        {
            currentID = int.Parse(es.currentSelectedGameObject.name);
                currentItem = CopyInventoryItem(items[currentID]);
            movingObject.gameObject.SetActive(true);
            movingObject.GetComponent<Image>().sprite = data.items[currentItem.id].img;

            AddItem(currentID, data.items[0], 0 );
        }
        else
        {
            ItemInventory II = items[int.Parse(es.currentSelectedGameObject.name)];

            if (currentItem.id !=II.id)
            {
                AddInventoryItem(currentID, II);
                AddInventoryItem(int.Parse(es.currentSelectedGameObject.name), currentItem);
                UpdateInventory();
            }
            else
            {
                if(II.count+currentItem.count <= 128)
                {
                II.count += currentItem.count;
                    
                }
                else
                {
                    AddItem(currentID, data.items[II.id], II.count+currentItem.count-128);
                    II.count = 128;
                    
                }
                UpdateInventory();
            }
           
            currentID = -1;

            movingObject.gameObject.SetActive(false);

        }
    }

    public void MoveObject()
    {
        Vector3 pos = Input.mousePosition + offset;
        pos.z = InventoryMainObject.GetComponent<RectTransform>().position.z;
        movingObject.position = cam.ScreenToWorldPoint(pos);
        if(movingObject.GetComponent <Image>().sprite == null)
        {
            movingObject.GetComponent<Image>().sprite = beb; 
        }


    }

    public ItemInventory CopyInventoryItem(ItemInventory old)
    {
        ItemInventory New = new ItemInventory();

        New.id = old.id;
        New.itemGameObj = old.itemGameObj;
        New.count = old.count;

        return New;
    }
        
    
}
[System.Serializable]

public class ItemInventory
{
    public int id;
    public GameObject itemGameObj;

    public int count;
}
