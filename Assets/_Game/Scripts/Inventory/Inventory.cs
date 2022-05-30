using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.SceneManagement;

//Inventory
//Main inventory class for storing data
[System.Serializable]
public class Inventory
{
    //Inventory slot
    //Class representing one inventory slot
    [System.Serializable]
    public class InventorySlot
    {
        private int _index;
        [SerializeField] private Item _item;

        public InventorySlot(int index)
        {
            _index = index;
        }

        public Item GetItem()
        {
            return _item;
        }

        public void SetItem(Item item)
        {
            _item = item;
        }

        public void RemoveItem()
        {
            _item = null;
        }

        public bool IsEmpty()
        {
            return (_item == null);
        }

        public int GetIndex()
        {
            return _index;
        }
    }

    [SerializeField] private InventorySlot[] _inventorySlots;
    public InventorySlot[] inventorySlots { get => _inventorySlots; }

    private int _size;
    public int size { get => _size; }

    public event EventHandler OnInventoryItemsChange;

    public class InventoryItemEventArgs : EventArgs
    {
        public Item item;
    }
    public event EventHandler<InventoryItemEventArgs> OnInventoryItemAdd;
    public event EventHandler<InventoryItemEventArgs> OnInventoryItemRemove;

    public Inventory(int inventorySize)
    {
        //Set inventory size, create new slot array of given size
        _size = inventorySize;
        _inventorySlots = new InventorySlot[inventorySize];

        //Fill slot array with new slots
        for (int i = 0; i < inventorySize; i++)
        { 
            _inventorySlots[i] = new InventorySlot(i);
        }
    }

    //Add item to item array
    public bool AddItem(Item item)
    {
        //Get empty slot
        InventorySlot emptySlot = GetEmptyInventorySlot();
        if (emptySlot != null)
        {
            //If there is an empty slot, set its item and fire envent
            emptySlot.SetItem(item);
            OnInventoryItemsChange?.Invoke(this, EventArgs.Empty);

            //Additional Events
            OnInventoryItemAdd?.Invoke(this, new InventoryItemEventArgs { item = item });
            return true;
        }
        return false;
    }

    //Add item by inventory slot
    public bool AddItem(InventorySlot tempSlot)
    {
        foreach (InventorySlot inventorySlot in _inventorySlots)
        {
            //Find slot by index 
            if (inventorySlot.GetIndex() != tempSlot.GetIndex()) continue;

            //Check if slot is empty
            if (!inventorySlot.IsEmpty()) continue;

            //Set item to inventory slot
            inventorySlot.SetItem(tempSlot.GetItem());

            //Update visuals
            OnInventoryItemsChange?.Invoke(this, EventArgs.Empty);

            //Additional Events
            OnInventoryItemRemove?.Invoke(this, new InventoryItemEventArgs { item = tempSlot.GetItem() });
            
            return true;
        }
        return false;
    }

    public bool RemoveItem(Item item)
    { 
        InventorySlot slotWithItem = GetInventorySlotWithItem(item);
        if (slotWithItem != null)
        {
            //If item is in inventory, remove it and update visuals
            slotWithItem.RemoveItem();
            OnInventoryItemsChange?.Invoke(this, EventArgs.Empty);

            //Additional Events
            OnInventoryItemRemove?.Invoke(this, new InventoryItemEventArgs { item = item });
            return true;
        }
        return false;
    }

    //Return empty slot or null
    public InventorySlot GetEmptyInventorySlot()
    {
        foreach(InventorySlot slot in _inventorySlots)
        {
            if (slot.IsEmpty()) return slot;
        }
        Debug.Log("No empty slots found");
        return null;
    }

    //Return slot with item or null
    public InventorySlot GetInventorySlotWithItem(Item item)
    {
        foreach(InventorySlot slot in _inventorySlots)
        {
            if(slot.GetItem() == item) return slot;
        }
        Debug.Log("Item is not in inventory");
        return null;
    }

    //Return first slot with matching ItemData
    public InventorySlot GetFirstInventorySlotWithItemOfItemData(ItemData itemData)
    {
        foreach (InventorySlot slot in _inventorySlots)
        {
            if(!slot.IsEmpty())
                if (slot.GetItem().itemData == itemData) return slot;
        }
        Debug.Log("Not items of given ItemData");
        return null;
    }
}
