using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloudMove : MonoBehaviour
{
    public GameObject cloud;
    public GameObject cloud2;
    public GameObject cloud3;
    public GameObject cloud4;
    public GameObject cloud5;
    public GameObject cloud6;
    public GameObject cloud7;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var a = cloud.transform.position.x;
        Vector3 move = new Vector3(cloud.transform.position.x-0.1f, cloud.transform.position.y);
        Vector3 move1 = new Vector3(cloud2.transform.position.x - 0.1f, cloud.transform.position.y-2);
        Vector3 move2 = new Vector3(cloud3.transform.position.x + 0.1f, cloud.transform.position.y-0.5f);
        Vector3 move3 = new Vector3(cloud4.transform.position.x + 0.1f, cloud.transform.position.y - 4);
        Vector3 move4 = new Vector3(cloud5.transform.position.x - 0.1f, cloud.transform.position.y - 3);
        Vector3 move5 = new Vector3(cloud6.transform.position.x - 0.1f, cloud.transform.position.y - 4);
        Vector3 move6 = new Vector3(cloud7.transform.position.x + 0.1f, cloud.transform.position.y+1);
        cloud.transform.position = move;
        cloud2.transform.position = move1;
        cloud3.transform.position = move2;
        cloud4.transform.position = move3;
        cloud5.transform.position = move4;
        cloud6.transform.position = move5;
        cloud7.transform.position = move6;
    }
}
