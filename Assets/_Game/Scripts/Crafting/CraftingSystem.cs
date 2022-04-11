using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

//Crafting system
//Crafting class responsible for storing and crafting items

public class CraftingSystem
{
    private Inventory.InventorySlot[,] _craftingSlots;
    public Inventory.InventorySlot[,] craftingSlots { get => _craftingSlots; }

    private Inventory.InventorySlot _itemOutput;
    public Inventory.InventorySlot itemOutput { get => _itemOutput; }

    private int _gridSize = 3;
    public int gridSize { get => _gridSize;}

    public event EventHandler OnCraftingItemsChange;

    private List<RecipeScriptableObject> _recipes;

    public CraftingSystem()
    {
        _craftingSlots = new Inventory.InventorySlot[_gridSize, _gridSize];
        _itemOutput = new Inventory.InventorySlot(0);
        _recipes = new List<RecipeScriptableObject>();

        for(int x = 0; x < _gridSize; x++)
        {
            for(int y = 0; y < _gridSize; y++)
            {
                _craftingSlots[x, y] = new Inventory.InventorySlot(y * _gridSize + x);
            }
        }
    }

    //Setting craftings recipes list
    public void SetRecipes(List<RecipeScriptableObject> recipes)
    {
        _recipes = recipes;
    }

    //Adding items by [x, y] coordinate to crafting grid slots
    public bool AddItem(Item item, int x, int y)
    {
        if (_craftingSlots[x, y].GetItem() == null)
        {
            //Slot on [x, y] is empty
            SetItem(item, x, y);
            OnCraftingItemsChange?.Invoke(this, new EventArgs());
            return true;
        }
        else
        {
            //Slot on [x, y] is not empty
            Debug.Log("Can't add item, slot " + x + " " + y + " alrady has item");
            OnCraftingItemsChange?.Invoke(this, new EventArgs());
            return false;
        }
    }

    public bool AddItem(Inventory.InventorySlot tempSlot)
    {
        //TODO: Moving items inside crafing

        foreach(Inventory.InventorySlot craftingSlot in _craftingSlots)
        {
            //Find slot by index 
            if (craftingSlot.GetIndex() != tempSlot.GetIndex()) continue;

            //Check if slot is empty
            if (!craftingSlot.IsEmpty()) continue;

            //Set item to inventory slot
            craftingSlot.SetItem(tempSlot.GetItem());

            //Update visuals
            OnCraftingItemsChange?.Invoke(this, EventArgs.Empty);

            return true;
        }
        return false;
    }

    //Remove item from [x, y] and return the removed item
    public Item RemoveItem(int x, int y)
    {
        if (_craftingSlots[x, y].GetItem() != null)
        {
            //Crafing slot on [x, y] is not empty and item can be removed
            Item item = _craftingSlots[x, y].GetItem();
            SetItem(null, x, y); 
            OnCraftingItemsChange?.Invoke(this, new EventArgs());
            return item;
        }
        else
        {
            //Crafing slot on [x, y] is empty, no item can be removed
            Debug.Log("Can't remove item from slot " + x + " " + y + ", slot is empty");
            return null;
        }
    }

    //Remove item by its reference
    public Item RemoveItem(Item item)
    {
        //Check if item is in any of crafting grid slots
        foreach(Inventory.InventorySlot slot in _craftingSlots)
        {
            //Find slot with item given to function
            if(slot.GetItem() == item)
            {
                //Item found, can be removed
                Item tempItem = slot.GetItem();
                slot.RemoveItem();
                OnCraftingItemsChange?.Invoke(this, new EventArgs());
                return tempItem;
            }
        }

        //Check if item is in output slot
        if(_itemOutput.GetItem() == item)
        {
            //Remove item and consume crafting items
            Item tempItem = _itemOutput.GetItem();
            _itemOutput.RemoveItem();
            ConsumeCraftingItems();
            OnCraftingItemsChange?.Invoke(this, new EventArgs());
            return tempItem;
        }
        return null;
    }

    //Set item in inventory slot on [x, y] position
    private void SetItem(Item item, int x, int y)
    {
        _craftingSlots[x, y].SetItem(item);
    }

    //Get item from inventory slot form [x, y] position
    private Item GetItem(int x, int y)
    {
        return _craftingSlots[x, y].GetItem();
    }

    //Check if inventory on [x, y] is empty
    private bool IsEmpty(int x, int y)
    {
        return _craftingSlots[x, y].IsEmpty();
    }

    //Try crafting item by checking all recipes
    private ItemData TryCraft()
    {
        foreach (RecipeScriptableObject recipe in _recipes)
        {
            bool isValidCrafting = true;

            //Loop for [x, y]
            for(int x = 0; x < gridSize; x++)
            {
                for(int y = 0; y < gridSize; y++)
                {
                    if(recipe.GetItem(x, y) != null)
                    {
                        //Check if recipe has an item on [x, y] position
                        if(IsEmpty(x, y) || GetItem(x, y).itemData != recipe.GetItem(x, y))
                        {
                            //Check if crafting grid on [x, y] has an item
                            //Check if item in crafing grid on [x, y] has the same item
                            //If not, crafting is invalid
                            isValidCrafting = false;
                        }
                    }
                }
            }

            //If item can be crafted return recipe output item
            if (isValidCrafting) return recipe.output;
        }
        //If all recipes are invalid for items put in crafing grid, return null
        return null;
    }

    //Setting an _itemOutput to be crafted item or null
    public void CreateOutput()
    {
        //Try to craft item
        ItemData output = TryCraft();
        if (output != null)
        {
            //if crafted item is not null, set _itemOutput
            Item outputItem = new Item { itemData = output };
            _itemOutput.SetItem(outputItem);
        }
        //If not, _itemOutput is null
        else _itemOutput.RemoveItem();
    }

    //Clear _itemOutput after taking it from crafting slot
    //Clear crafting slot from item => removing used items
    //Update crafting grid
    public void ConsumeCraftingItems()
    {
        foreach(Inventory.InventorySlot slot in _craftingSlots)
        {
            slot.RemoveItem();
        }

        OnCraftingItemsChange?.Invoke(this, EventArgs.Empty);
    }
}
