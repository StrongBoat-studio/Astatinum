using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Crafting UI
//Class responsible for displaying crafting UI
public class UI_Crafting : MonoBehaviour, IItemHolder
{
    [SerializeField] private Canvas _canvas;
    [SerializeField] private GameObject _slotPrefab;
    [SerializeField] private GameObject _itemPrefab;
    [SerializeField] private Transform _slotContainer;
    [SerializeField] private Transform _itemContainer;
    private RectTransform _slotContainerRect;
    private RectTransform _slotPrefabRect;
    private RectTransform _outputSlotRect;
    private CraftingSystem _craftingSystem;

    private void Awake()
    {
        
    }

    //Set crafting system and subscribe to crafting grid changed event
    //Set references to RectTransform of slot container
    //Set references to RectTransform of slot prefab
    //Initialy refresh crafting UI
    public void SetCraftingSystem(CraftingSystem craftingSystem)
    {
        _craftingSystem = craftingSystem;
        _craftingSystem.OnCraftingItemsChange += CraftingSystem_OnCraftingItemsChange;

        _slotContainerRect = _slotContainer.GetComponent<RectTransform>();
        _slotPrefabRect = _slotPrefab.GetComponent<RectTransform>();

        RefreshCrafting();
    }

    //Event from CraftingSystem
    private void CraftingSystem_OnCraftingItemsChange(object sender, EventArgs e)
    {
        RefreshCrafting();
    }

    //Refreshing crafting UI
    private void RefreshCrafting()
    {
        //clear slot container
        foreach (Transform slot in _slotContainer)
            Destroy(slot.gameObject);

        //clear item container
        foreach (Transform item in _itemContainer)
            Destroy(item.gameObject);

        //Get slot size
        float slotSize = _slotPrefab.GetComponent<RectTransform>().rect.width;
        
        int i = 0;
        foreach (Inventory.InventorySlot craftingSlot in _craftingSystem.craftingSlots)
        {
            //Get item from item slot
            Item item = craftingSlot.GetItem();

            //Recreate slot
            RectTransform uiItemSlotObject = Instantiate(_slotPrefab, _slotContainer).GetComponent<RectTransform>();

            //Get [x, y] positions for UI slot elements
            int y = Mathf.FloorToInt(_slotContainerRect.rect.yMin + ((i % _craftingSystem.gridSize) * _slotPrefab.GetComponent<RectTransform>().rect.width) + _slotPrefabRect.rect.width / 2);
            int x = Mathf.FloorToInt(_slotContainerRect.rect.xMin + Mathf.FloorToInt(i / _craftingSystem.gridSize) * _slotPrefab.GetComponent<RectTransform>().rect.height + _slotPrefabRect.rect.height / 2);
            Vector2 uiItemSlotPosition = new Vector2(x, y);
            
            //Set slot position and name
            uiItemSlotObject.anchoredPosition = uiItemSlotPosition;
            uiItemSlotObject.name = ("Slot" + craftingSlot.GetIndex());

            //Create item in slot
            if (!craftingSlot.IsEmpty())
            {
                //If crafting slot is not empty
                //Create UI item and set its name, posiotn
                Transform uiItem = Instantiate(_itemPrefab, _itemContainer).transform;
                uiItem.name = item.GetItemName();
                uiItem.localPosition = uiItemSlotPosition;
                
                //Get UI_Item class reference and set item, sprite and onClick event
                uiItem.GetComponent<UI_Item>().SetItem(craftingSlot.GetItem());
                uiItem.GetComponent<UI_Item>().SetSprite(craftingSlot.GetItem().GetItemSprite());
                uiItem.GetComponent<Button>().onClick.AddListener(() =>
                {
                    //If item can be added to players inventory, remove item from crafting grid
                    if (GameManager.Instance.player.GetComponent<Player>().inventory.AddItem(item))
                        _craftingSystem.RemoveItem(item);
                });
            }

            
            //Calculate temporary slot index
            int tempSlotIndex = (i % _craftingSystem.gridSize) * _craftingSystem.gridSize + Mathf.FloorToInt(i / _craftingSystem.gridSize);
            //Create temporary slot
            Inventory.InventorySlot tempSlot = new Inventory.InventorySlot(tempSlotIndex);

            //Get UI_ItemSlot reference
            UI_ItemSlot uiItemSlot = uiItemSlotObject.GetComponent<UI_ItemSlot>();
            
            //Set action for dropping items on slot
            uiItemSlot.SetOnDropAction(() =>
            {
                //Get currently draging item from item drag class
                Item draggedItem = UI_ItemDrag.Instance.GetItem();
                
                //Set item in temp slot
                tempSlot.SetItem(draggedItem);

                //Remove item from item drag class
                UI_ItemDrag.Instance.GetItemHolder().RemoveItemFromHolder(draggedItem);

                //Add item to crafting system by temp slot
                _craftingSystem.AddItem(tempSlot);
            });

            i++;
        }

        //Try creating item in CraftingSystem
        _craftingSystem.CreateOutput();

        //Create Output slot, set its position and name
        _outputSlotRect = Instantiate(_slotPrefab, _slotContainer).GetComponent<RectTransform>();
        _outputSlotRect.localPosition = new Vector2(_slotContainerRect.rect.xMax - _slotPrefabRect.rect.width / 2, 0);
        _outputSlotRect.name = "OutputSlot";
        
        if (_craftingSystem.itemOutput.GetItem() != null)
        {
            //If item output is not null
            //Create UI item in ui output slot, set its position, name
            Transform outputItem = Instantiate(_itemPrefab, _itemContainer).transform;
            outputItem.localPosition = _outputSlotRect.localPosition;
            
            //Set UI_Item item, sprite and on click event
            outputItem.GetComponent<UI_Item>().SetItem(_craftingSystem.itemOutput.GetItem());
            outputItem.GetComponent<UI_Item>().SetSprite(_craftingSystem.itemOutput.GetItem().GetItemSprite());
            outputItem.GetComponent<Button>().onClick.AddListener(() =>
            {
                //If crafted item can be added to inventory, consume crafting items
                if(GameManager.Instance.player.GetComponent<Player>().inventory.AddItem(_craftingSystem.itemOutput))
                {
                    _craftingSystem.ConsumeCraftingItems();
                    AudioManager.Instance.PlayGameCraftingDone();
                }
            });
        }
    }

    public void RemoveItemFromHolder(Item item)
    {
        _craftingSystem.RemoveItem(item);
    }
}
