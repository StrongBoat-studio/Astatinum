using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UI_Inventory : MonoBehaviour, IItemHolder
{
    private Inventory _inventory;
    [SerializeField] private GameObject _slotPrefab;
    [SerializeField] private GameObject _itemPrefab;
    [SerializeField] private Transform _slotContainer;
    [SerializeField] private Transform _itemContainer;
    [SerializeField] private Canvas _canvas;

    public void SetInventory(Inventory inventory)
    {
        //Set inventory reference
        _inventory = inventory;
        _inventory.OnInventoryItemsChange += Inventory_OnInventoryItemsChange;

        //Generate inventory;
        RefreshInventoryItems();
    }

    public void RefreshInventoryItems()
    {
        //clear containers
        foreach (Transform slot in _slotContainer)
            Destroy(slot.gameObject);

        foreach(Transform item in _itemContainer)
            Destroy(item.gameObject);

        //Generate inventory slots
        float slotSize = _slotPrefab.GetComponent<RectTransform>().rect.width;
        int i = 0;
        foreach (Inventory.InventorySlot inventorySlot in _inventory.inventorySlots)
        {
            //Get item from item slot
            Item item = inventorySlot.GetItem();

            //Recreate slot
            RectTransform uiItemSlotObject = Instantiate(_slotPrefab, _slotContainer).GetComponent<RectTransform>();
            Vector2 uiItemSlotPosition = new Vector2(_slotContainer.GetComponent<RectTransform>().rect.xMin + i * uiItemSlotObject.rect.width + uiItemSlotObject.rect.width / 2, 0);
            uiItemSlotObject.anchoredPosition = uiItemSlotPosition;
            uiItemSlotObject.name = ("Slot" + inventorySlot.GetIndex());

            //Create item in slot
            if(!inventorySlot.IsEmpty())
            {
                Transform uiItem = Instantiate(_itemPrefab, _itemContainer).transform;
                uiItem.name = item.GetItemName();
                uiItem.localPosition = uiItemSlotPosition;
                uiItem.GetComponent<UI_Item>().SetItem(inventorySlot.GetItem());
                uiItem.GetComponent<UI_Item>().SetSprite(inventorySlot.GetItem().GetItemSprite());
            }

            //Set onDropAction for slot
            Inventory.InventorySlot tempSlot = new Inventory.InventorySlot(i);

            UI_ItemSlot uiItemSlot = uiItemSlotObject.GetComponent<UI_ItemSlot>();
            uiItemSlot.SetOnDropAction(() => {
                //Get currently dragging item
                Item draggedItem = UI_ItemDrag.Instance.GetItem();
                
                //Set it to temp slot
                tempSlot.SetItem(draggedItem);
                
                //Remove item from inventory
                UI_ItemDrag.Instance.GetItemHolder().RemoveItemFromHolder(draggedItem);
                
                //Add item to inventory to new slot posion
                _inventory.AddItem(tempSlot);
            });

            i++;
        }
    }

    //Inventory event
    private void Inventory_OnInventoryItemsChange(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }

    public void RemoveItemFromHolder(Item item)
    {
        _inventory.RemoveItem(item);
    }
}
