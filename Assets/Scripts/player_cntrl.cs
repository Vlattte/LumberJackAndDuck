using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class player_cntrl : MonoBehaviour
{
    private Rigidbody2D rb;
    public pocket_inventory pocket;
    //private Animator anim;

    //current state of duck
    public float speed;
    public bool facingRight = true;

    private RaycastHit2D hit;
    public float box_distance;
    Vector2 box_size;
    public bool isTreeAtFront;

    //INVENTORY MANAGMENT
    //public GameObject INVENT_FULL_CAN;
    //public int invent_idx;

    //items
    public LayerMask itemLayer;
    public LayerMask obstLayer;
    public LayerMask colLayer;

    Vector2 player_pos;


    void Start()
    {
        box_distance = 0.01f;
        box_size = new Vector2(0.01f, 0.01f);
        rb = GetComponent<Rigidbody2D>();
        isTreeAtFront = false;
    }


    private void Sprint(bool is_run)
    {
        if (is_run)
            speed /= 1.3f;
        else
            speed *= 1.3f;
    }

    private void Movement()
    {
        Vector2 vec = new Vector2(0, 0);
        //anim.SetBool("is_walking", true);
        if (Input.GetButton("Horizontal"))
        {
            if (facingRight == false && Input.GetAxis("Horizontal") > 0)
            {
                Rotate();
            }
            else if (facingRight == true && Input.GetAxis("Horizontal") < 0)
            {
                Rotate();
            }

            vec = vec + Vector2.right * Input.GetAxis("Horizontal");
            //rb.MovePosition(rb.position + Vector2.right * Input.GetAxis("Horizontal") * speed);
        }

        if (Input.GetButton("Vertical"))
        {
            vec = vec + Vector2.up * Input.GetAxis("Vertical");
            //rb.MovePosition(rb.position + Vector2.up * Input.GetAxis("Vertical") * speed);
        }

        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            rb.MovePosition(rb.position + vec * speed);
            Debug.Log(vec);
        }
    }

    void Update()
    {
        //sprint
        if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            Sprint(false);
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetKeyUp(KeyCode.RightShift))
        {
            Sprint(true);
        }

        //TODO::sprint animation
        //TODO::walk animation
        //player movement
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            Movement();
        }

        /*if (Input.GetButtonUp("Horizontal"))
        {
            anim.SetBool("is_walking", false);
        }*/


        
        string tag_name = WhatIsInFrontOfPlayer();

        //***TREE SECTOR***\\
        //if tree in front, you can cut it
        if (tag_name == "tree" && Input.GetKeyUp(KeyCode.E))
        {
            ChopChop();    
        }
        //*****************\\

        //***DROP ITEMS TO BASE***\\
        if (tag_name == "storage" && Input.GetKeyUp(KeyCode.E))
        {
            pocket.SendItemsToStorage();
        }
        //*************************\\


        if (Input.GetKeyUp(KeyCode.R))
            Rotate();
    }



    public string WhatIsInFrontOfPlayer()
    {
        string tag_name = "default";
        
        player_pos = transform.position;
        player_pos.x += 0.15f * transform.localScale.x;
        
        hit = Physics2D.BoxCast(player_pos, box_size, 0, Vector2.right * transform.localScale.x, box_distance, colLayer);

        if (hit.collider != null)
        {
            if (hit.collider.tag == "tree" && !isTreeAtFront)
            {
                isTreeAtFront = true;
            }
            tag_name = hit.collider.tag;
        }
        return tag_name;
    }

    public Vector3 ChopChop()
    {
        Vector3 pos = new Vector3(47,47,47);
        if (hit.collider != null)
        {
            pos = hit.collider.GetComponent<tree_script>().DropWood();
            isTreeAtFront = false;
        }    
        return pos;
    }

    void Rotate()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}