using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class inventory : MonoBehaviour
{
    public bool[] is_filled;
    public TextMeshProUGUI[] slotsNumbers;
    public bool is_invent_full = false;
    /*public bool is_open = false;
    private bool opening_inventory = false;*/
    //public GameObject INVENT_FULL_CAN;

    //timers
    private float fullInventTimer = 0;
    private float openingTimer = 0;
    /*public void CloseOpenInventory()
    {
        if(!is_open)
            opening_inventory = true;
        else
            is_open = !is_open;
    }*/

    public void AddElement(int idx, int amount)
    {
        int count = Int32.Parse(slotsNumbers[idx].ToString());
        slotsNumbers[idx].text = (count++).ToString();
    }

    public bool IsElementFull(int idx)
    {
        int count = Int32.Parse(slotsNumbers[idx].ToString());
        return (count > 47);
    }

    private void Update()
    {





        //inventory full
        /*if (fullInventTimer >= 3)
        {
            //INVENT_FULL_CAN.SetActive(false);
            is_invent_full = false;
            fullInventTimer = 0;
        }*/

        /*if (is_invent_full)
        {
            INVENT_FULL_CAN.transform.position = gameObject.transform.position;
            if (fullInventTimer == 0)
                INVENT_FULL_CAN.SetActive(true);
            fullInventTimer += Time.deltaTime;
        }*/

        //is inventory closed
        /*if(opening_inventory)
        {
            openingTimer += Time.deltaTime;
            if(openingTimer > 1.25f)
            {
                Debug.Log("NASILYUT!!!!!!!!!!");
                is_open = !is_open;
                opening_inventory = false;
            }
        }*/
    }
}
