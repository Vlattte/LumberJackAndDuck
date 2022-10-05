using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class duck_cntrl : MonoBehaviour
{

    public GameObject storage;
    private Rigidbody2D rb;
    public player_cntrl player;

    private Vector2 storage_pos;
    private Vector2 player_pos;
    public float DIST_TO_PLAYER;
    public float DIST_TO_STORAGE;
    public float speed;

    private bool isSendToStorage;   //true, if player told to go to the base

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        speed = 2f;
        DIST_TO_PLAYER = 1.47f;
        DIST_TO_STORAGE = 0.5f;
        storage_pos = storage.transform.position;
        player_pos = player.transform.position;

        isSendToStorage = false;
    }

    void Update()
    {
        player_pos = player.transform.position;
        if (!isSendToStorage)
            MoveTo(player_pos);

        if (isSendToStorage)
            MoveTo(storage_pos, true);
    }

    bool checkIsDistIsNear(bool isDistStrg)
    {
        Vector2 dist_vec;
        float dist_const;
        if (isDistStrg)
        {
            dist_vec = storage_pos;
            dist_const = DIST_TO_STORAGE;
        }  
        else
        {
            dist_vec = player_pos;
            dist_const = DIST_TO_PLAYER;
        }


        float dist = (rb.position - dist_vec).magnitude;
        return dist <= dist_const;
    }

    

    void MoveTo(Vector2 dist_vec, bool isDistStrg = false)
    {
        if (checkIsDistIsNear(isDistStrg))
            return;
            
        /*float dest_x = (dist_vec[0] - rb.position[0]) / Mathf.Pow(Mathf.Pow(dist_vec[0] - rb.position[0], 2)
                + Mathf.Pow(dist_vec[1] - rb.position[1], 2), 0.5f);
        float dest_y = (dist_vec[1] - rb.position[1]) / Mathf.Pow(Mathf.Pow(dist_vec[0] - rb.position[0], 2)
                + Mathf.Pow(dist_vec[1] - rb.position[1], 2), 0.5f);*/

        float dist = (rb.position - dist_vec).magnitude;
        transform.position = Vector2.MoveTowards(transform.position, dist_vec, speed * Time.deltaTime);
    }
}