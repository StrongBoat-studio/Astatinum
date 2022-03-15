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
        AddItem(new Item { itemType = Item.ItemType.Item1 });
        AddItem(new Item { itemType = Item.ItemType.Item2 });
        AddItem(new Item { itemType = Item.ItemType.Item3 });
        ConsoleLogInventory();
    }

    public void AddItem(Item item)
    {
        if (_items.Count < _size)
        {
            _items.Add(item);
            OnInventoryItemsChange?.Invoke(this, EventArgs.Empty);
        }
        else
            Debug.Log("Inventory full, can't add item");
    }

    public void RemoveItem(Item item)
    {
        if (_items.Contains(item))
        {
            _items.Remove(item);
            OnInventoryItemsChange?.Invoke(this, EventArgs.Empty);
        }
        else
            Debug.Log("Item not in inventory, can't remove");
    }

    public void ConsoleLogInventory()
    {
        foreach(Item item in _items)
        {
            Debug.Log(item.itemType.ToString());
        }
    }
}
