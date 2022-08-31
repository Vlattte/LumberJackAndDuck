using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class drop_item : MonoBehaviour
{
    public GameObject dropItem;
    private inventory Inventory;
    private Vector3 forward_pos;
    private GameObject Player;
    public int inventory_position;

    private void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
        Inventory = Player.GetComponent<inventory>();
        //inventory_position = Player.GetComponent<player_cntrl>().invent_idx;
    }
    private void OnMouseDown()
    {
        if (Inventory.is_open)
        {
            forward_pos = Player.transform.position;
            if (Player.GetComponent<player_cntrl>().facingRight)
            {
                forward_pos.x += 0.7f;
            }
            else
            {
                forward_pos.x -= 0.7f;
            }


            Player.GetComponent<inventory>().is_filled[inventory_position] = false;
            Instantiate<GameObject>(dropItem, forward_pos, Quaternion.identity);
            Destroy(gameObject);
        }
    }
}
