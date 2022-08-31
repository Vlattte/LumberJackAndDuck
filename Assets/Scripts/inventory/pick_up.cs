using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class pick_up : MonoBehaviour
{
    private inventory Inventory;
    public GameObject inventory_object;
    private int pos;
    public int item_price;

    private void Start()
    {
        Inventory = GameObject.FindGameObjectWithTag("Player").GetComponent<inventory>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            if (Inventory.)
            {
                Destroy(gameObject);
                break;
            }
        }
    }
}
