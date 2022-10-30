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
    public Vector3 target_pos;
    int way_point_idx;

    private GameObject duckCaller;
    private bool isWalking;

    Vector3[] path;

    void Start()
    {
        isWalking = false;
        way_point_idx = 0;
        rb = GetComponent<Rigidbody2D>();
        speed = 0.05f;
        DIST_TO_PLAYER = 1.47f;
        DIST_TO_STORAGE = 0.5f;
        storage_pos = storage.transform.position;
        player_pos = player.transform.position;

        isSendToStorage = false;
    }

    void Update()
    {
        if (GameObject.FindGameObjectWithTag("duckCaller") && !isWalking)
        {
            isWalking = true;
            duckCaller = GameObject.FindGameObjectWithTag("duckCaller");
            PathManager.Requestpath(transform.position, duckCaller.transform.position, OnPathFound);
        }


        player_pos = player.transform.position;
        

        if (isSendToStorage)
        {
            isSendToStorage = false;
            PathManager.Requestpath(transform.position, storage_pos, OnPathFound);
        }     
    }

    public void MoveToStorage()
    {
        isSendToStorage = true;
        isWalking = false;
    }

    void OnPathFound(Vector3[] _path, bool isPathSuccess)
    {
        if(isPathSuccess)
        {
            path = _path;
            StopCoroutine("FollowPath");
            StartCoroutine("FollowPath");
        }
    }

    IEnumerator FollowPath()
    {
        // if we are in the needed node
        if (path.Length == 0)
            yield break;

        Vector3 currentWaypoint = path[0];

        while(true)
        {
            if (transform.position == currentWaypoint)
            {
                way_point_idx++;
                if (way_point_idx >= path.Length)
                {
                    isWalking = false;
                    Destroy(duckCaller);

                    isSendToStorage = false;
                    
                    yield break;
                }
                currentWaypoint = path[way_point_idx];
            }
            MoveTo(currentWaypoint);
            yield return null;
        }
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

    

    bool MoveTo(Vector2 dist_vec, bool isDistStrg = false)
    {
        if (checkIsDistIsNear(isDistStrg))
            return true;
            
        /*float dest_x = (dist_vec[0] - rb.position[0]) / Mathf.Pow(Mathf.Pow(dist_vec[0] - rb.position[0], 2)
                + Mathf.Pow(dist_vec[1] - rb.position[1], 2), 0.5f);
        float dest_y = (dist_vec[1] - rb.position[1]) / Mathf.Pow(Mathf.Pow(dist_vec[0] - rb.position[0], 2)
                + Mathf.Pow(dist_vec[1] - rb.position[1], 2), 0.5f);*/


        float dist = (rb.position - dist_vec).magnitude;
        transform.position = Vector2.MoveTowards(transform.position, dist_vec, speed);
        return false;
    }

    public void OnDrawGizmos()
    {
        /*if(path != null)
        {
            for (int i = way_point_idx; i < path.Length; i++)
            {
                Gizmos.color = Color.black;
                Gizmos.DrawCube(path[i], Vector2.one);

                if (i == way_point_idx)
                    Gizmos.DrawLine(transform.position, path[i]);
                else
                    Gizmos.DrawLine(path[i - 1], path[i]);
            }
        }*/
    }
}
