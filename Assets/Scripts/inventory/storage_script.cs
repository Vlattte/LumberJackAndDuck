using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class storage_script : MonoBehaviour
{
    public base_inventory inventory;
    public GameObject hintText;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.tag == "Player")
            {
                hintText.SetActive(true);
            }

        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.tag == "Player")
                hintText.SetActive(false);
        }
    }
}