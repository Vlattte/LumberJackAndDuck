using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tree_script : MonoBehaviour
{
    public GameObject woodLogs;
    public int woodDropAmount;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        {
            DropWood();
        }
    }

    public void DropWood()
    {
        Instantiate(woodLogs, gameObject.transform.position, Quaternion.identity);
        Destroy(gameObject);
    }
}
