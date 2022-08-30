using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class player_cntrl : MonoBehaviour
{
    private Rigidbody2D rb;
    //private Animator anim;

    //current state of duck
    public float speed;
    public bool facingRight = true;

    public float ray_length;

    //INVENTORY MANAGMENT
    //public GameObject INVENT_FULL_CAN;
    //public int invent_idx;

    //items
    public LayerMask itemLayer;
    public LayerMask obstLayer;


    void Start()
    {
        ray_length = 3;
        rb = GetComponent<Rigidbody2D>();
        //anim = GetComponent<Animator>();
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

            rb.MovePosition(rb.position + Vector2.right * Input.GetAxis("Horizontal") * speed);
        }

        if (Input.GetButton("Vertical"))
        {
            rb.MovePosition(rb.position + Vector2.up * Input.GetAxis("Vertical") * speed);
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
    }

    void Rotate()
    {
        facingRight = !facingRight;
        Vector2 Scaler = transform.localScale;
        Scaler.x *= -1;
        transform.localScale = Scaler;
    }
}