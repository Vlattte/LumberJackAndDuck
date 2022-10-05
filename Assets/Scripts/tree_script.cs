using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tree_script : MonoBehaviour
{
    public GameObject woodLogs;
    public int woodDropAmount;
    private bool isPlayerNear;

    public void Start()
    {
        isPlayerNear = false;
    }

    /*private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            if (collision.GetComponent<player_cntrl>().isTreeAtFront)
            {
                isPlayerNear = true;
            }
        }
    }*/

    private void OnTriggerExit2D(Collider2D collision)
    {
        collision.GetComponent<player_cntrl>().isTreeAtFront = false;
    }

    public Vector3 DropWood()
    {
        Instantiate(woodLogs, gameObject.transform.position, Quaternion.identity);
        isPlayerNear = false;
        Destroy(gameObject);
        return gameObject.transform.position;
    }

    private void Update()
    {
        /*if(isPlayerNear && Input.GetKeyUp(KeyCode.E))
        {
            DropWood();
        }*/
    }
}
