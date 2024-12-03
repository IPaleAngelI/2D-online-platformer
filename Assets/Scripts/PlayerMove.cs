using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerMove : MonoBehaviour
{
    public Rigidbody2D rb;
    public Vector2 moveVector;
    public Animator anim;
    public SpriteRenderer sr;
    public float speed = 5f;

    PhotonView view;


    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        sr = GetComponent<SpriteRenderer>();

        view = GetComponent<PhotonView>();
    }

    void Update()
    {
        if (view.IsMine)
        {
            walk();
            Reflect();
            Jump();
            ChekingGround();
        }
    }

    //ױמהüבא
    void walk()
    {
        moveVector.x = Input.GetAxis("Horizontal");
        anim.SetFloat("moveX", Mathf.Abs(moveVector.x));
        rb.velocity = new Vector2(moveVector.x * speed, rb.velocity.y);
    }


    public bool faceRight = true;
    void Reflect()
    {
        if ((moveVector.x > 0 && !faceRight) || (moveVector.x < 0 && faceRight))
        {
            transform.localScale *= new Vector2(-1, 1);
            faceRight = !faceRight;
        }
    }

    public float jumpForce = 7f;
    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.W) && onGround)
        {
            rb.AddForce(Vector2.up * jumpForce);
        }
        else if (Input.GetKeyDown(KeyCode.W) && onGround)
        {
            if (Input.GetKeyDown(KeyCode.W) && onGround)
            {
                rb.AddForce(Vector2.up * jumpForce*2);
            }
        }
    }

    public bool onGround;
    public Transform GroundCheck;
    public float checkRadius = 0.5f;
    public LayerMask Ground;
    void ChekingGround()
    {
        onGround = Physics2D.OverlapCircle(GroundCheck.position, checkRadius, Ground);
        anim.SetBool("onGround" , onGround); 
    } 
}
