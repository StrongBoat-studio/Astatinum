using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private int _inventorySize;
    private Inventory _inventory;
    [SerializeField] private UI_Inventory _uiInventory;

    private void Awake()
    {
        //Crate new inventory and reference it to UI
        _inventory = new Inventory(_inventorySize);
        _uiInventory.SetInventory(_inventory);
    }

    public bool TakeWorldItem(Item item)
    {
        return _inventory.AddItem(item);
        
    }

    public bool DropWorldItem(Item item)
    {
        //Check if item can be removed
        bool canDrop = _inventory.RemoveItem(item);
        if (canDrop)
        {
            //Spawn new item if it can be removed
            GameObject itemGO = Instantiate(ItemAssets.Instance.worldItemPrefab, transform.position, Quaternion.identity);
            //Update item information
            itemGO.GetComponent<WorldItem>().SetItem(item);
        }

        return canDrop;
    }
}
