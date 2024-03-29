﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour, IGameManager
{
    public ManagerStatus status { get; private set; }
    public string equippedItem { get; private set; }

    private Dictionary<string, int> _items; // type of key and type of value
    private NetworkService _network;

    public void Startup(NetworkService service)
    {
        Debug.Log("Inventory manager starting...");

        _network = service;

        UpdateData(new Dictionary<string, int>());

        status = ManagerStatus.Started;
    }

    public void UpdateData(Dictionary<string, int> items)
    {
        _items = items;
    }

    public Dictionary<string, int> GetData()
    {
        return _items;
    }

    private void DisplayItems()
    {
        string itemsDisplay = "Items: ";
        foreach (KeyValuePair<string, int> itemName in _items)
        {
            itemsDisplay += itemName.Key + "(" + itemName.Value +") ";
        }
        Debug.Log(itemsDisplay);
    }

    public void AddItems(string name)
    {
        if (_items.ContainsKey(name))
        {
            _items[name] += 1;
        }
        else
        {
            _items[name] = 1;
        }


        DisplayItems();
    } 

    public List<string> GetItemList()
    {
        List<string> list = new List<string>(_items.Keys); // return list of all dictionary keys
        return list;
    }

    public int GetItemCound(string itemName)
    {
        if (_items.ContainsKey(itemName))
        {
            return _items[itemName];
        }
        return 0;
    }

    public bool EquipeItem(string name)
    {
        if(_items.ContainsKey(name) && equippedItem != name)
        {
            equippedItem = name;
            Debug.Log("Equipped "+name);
            return true;
        }
        equippedItem = null;
        Debug.Log("Unequipped");
        return false;
    }

    public bool ConsumeItems(string name)
    {
        if (_items.ContainsKey(name)){

            _items[name]--;
            if (_items[name] == 0)
            {
                _items.Remove(name);
            }

        } else
        {
            Debug.Log("No items in inventory "+name);
            return false;
        }
        DisplayItems();
        return true;
    }
}
