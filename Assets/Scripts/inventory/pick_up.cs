using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class pick_up : MonoBehaviour
{
    [HideInInspector]
    public pocket_inventory PocketInventory;
    public int item_amount;
    public string obj_name;

    private void Start()
    {
        PocketInventory = GameObject.FindGameObjectWithTag("Player").GetComponent<pocket_inventory>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if(col.CompareTag("Player"))
        {
            PocketInventory.AddElement(obj_name, item_amount);
            Destroy(gameObject);
        }
    }

    
}
