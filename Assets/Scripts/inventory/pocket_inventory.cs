using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class pocket_inventory : MonoBehaviour
{
    private bool[] is_filled;
    public TextMeshProUGUI[] slotsNumbers;
    public GameObject[] items;
    private int[] amounts;
    private int INVENTORY_SIZE;

    private bool[] whatItemsActive;

    public base_inventory storage;

    //[0] == logs
    private void Start()
    {
        Init();
    }

    public void Init()
    {
        INVENTORY_SIZE = 1;
        amounts = new int[INVENTORY_SIZE];
        whatItemsActive = new bool[INVENTORY_SIZE];

        for (int i = 0; i < INVENTORY_SIZE; i++)
        {
            whatItemsActive[i] = false;
            amounts[i] = 0;
        }
    }

    public int GetAmounts(int idx)
    {
        return amounts[idx];
    }

    //increase text amount
    public void AddElement(string obj_name, int amount)
    {
        int idx = CheckOrAddObject(obj_name);
        IncreaseItem(idx, amount);
    }

    public void IncreaseItem(int idx, int amount)
    {
        if (IsElementFull(idx))
            return;

        amounts[idx] += amount;
        if(slotsNumbers!=null)
            slotsNumbers[idx].text = (amounts[idx]).ToString();
    }

    //check, is this element type full
    public bool IsElementFull(int idx)
    {
        return (amounts[idx] > 47);
    }

    //if there is this type of object, do nothing
    //else set this type of object active
    private int CheckOrAddObject(string obj_name)
    {
        int return_val = 0;
        switch (obj_name)
        {
            case "logs":
                if (whatItemsActive[0] == false)
                {
                    if(items!=null)
                    {
                        items[0].SetActive(true);
                        whatItemsActive[0] = true;
                    }
                }
                return_val = 0;
                break;
        }
        return return_val;
    }

    public void SendItemsToStorage()
    {
        if (items!=null)
        {
            for(int i = 0; i < INVENTORY_SIZE; i++)
            {
                if (!whatItemsActive[i])
                    continue;

                storage.GetItemsFromPocket(i, amounts[i]);
                ClearItem(i);
            }  
            
        }
    }

    private void ClearItem(int idx)
    {
        whatItemsActive[idx] = false;
        slotsNumbers[idx].text = "";
        amounts[idx] = 0;
        items[idx].SetActive(false);
    }
}
