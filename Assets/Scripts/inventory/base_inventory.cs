using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class base_inventory : MonoBehaviour
{
    public bool[] is_filled;
    public TextMeshProUGUI[] slotsNumbers;
    public int[] amounts;
    public bool is_invent_full = false;
    private int INVENTORY_SIZE;

    private bool[] whatItems;

    //increase text amount
    public void AddElement(string obj_name, int amount)
    {
        int idx = CheckOrAddObject(obj_name);
        IncreaseItem(idx, amount);
    }

    public void GetItemsFromPocket(int idx, int amount)
    {
        IncreaseItem(idx, amount);
    }

    public void IncreaseItem(int idx, int amount)
    {
        if (IsElementFull(idx))
            return;

        amounts[idx] += amount;
        if (slotsNumbers != null)
            slotsNumbers[idx].text = (amounts[idx]).ToString();
    }

    public bool IsElementFull(int idx)
    {
        return (amounts[idx] > 47);
    }

    private int CheckOrAddObject(string obj_name)
    {
        int return_val = 0;
        switch (obj_name)
        {
            case "logs":
                return_val = 0;
                break;
        }
        return return_val;
    }

    //[0] == logs
    private void Start()
    {
        INVENTORY_SIZE = 1;
        amounts = new int[INVENTORY_SIZE];
        whatItems = new bool[INVENTORY_SIZE];

        for (int i = 0; i < INVENTORY_SIZE; i++)
        {
            whatItems[i] = false;
            amounts[i] = 0;
        }
    }
}
