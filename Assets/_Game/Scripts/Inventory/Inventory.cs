using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class Inventory
{
    private List<Item> _items;
    public List<Item> items { get => _items; }
    private int _size;

    public event EventHandler OnInventoryItemsChange;

    public Inventory(int inventorySize)
    {
        _items = new List<Item>();
        _size = inventorySize;
    }

    public bool AddItem(Item item)
    {
        if (_items.Count < _size)
        {
            _items.Add(item);
            OnInventoryItemsChange?.Invoke(this, EventArgs.Empty);
            return true;
        }
        else
        {
            Debug.Log("Inventory full, can't add item");
            return false;
        }
    }

    public bool RemoveItem(Item item)
    {
        if (_items.Contains(item))
        {
            _items.Remove(item);
            OnInventoryItemsChange?.Invoke(this, EventArgs.Empty);
            return true;
        }
        else
        {
            Debug.Log("Item not in inventory, can't remove");
            return false;
        }
    }

    //Debug
    public void ConsoleLogInventory()
    {
        foreach(Item item in _items)
        {
            Debug.Log(item.itemType.ToString());
        }
    }
}
