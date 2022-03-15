using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] private int _inventorySize;
    private Inventory _inventory;
    [SerializeField] private UI_Inventory _uiInventory;

    //Testing
    System.Random random = new System.Random();

    private void Awake()
    {
        _inventory = new Inventory(_inventorySize);
        _uiInventory.SetInventory(_inventory);
    }

    //Testing
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.I))
        {
            System.Array values = System.Enum.GetValues(typeof(Item.ItemType));
            Item.ItemType randomItem = (Item.ItemType)values.GetValue(random.Next(values.Length));

            _inventory.AddItem(new Item { itemType = randomItem });
        }

        if(Input.GetKeyDown(KeyCode.O))
        {
            if(_inventory.items.Count > 0)
                _inventory.RemoveItem(_inventory.items[Random.Range(0, _inventory.items.Count)]);
        }
    }
}
