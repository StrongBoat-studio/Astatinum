using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    private Inventory _inventory;
    [SerializeField] private GameObject _slotPrefab;
    [SerializeField] private Transform _slotContainer;
    private RectTransform _slotContainerRect;

    public void Awake()
    {
        _slotContainerRect = _slotContainer.GetComponent<RectTransform>();
    }

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
        //Clear slots
        foreach(Transform slot in _slotContainer)
            Destroy(slot.gameObject);

        //Generate inventory slots
        float inventorySlotOffset = (_slotContainerRect.rect.width / 2) - (_slotPrefab.GetComponent<RectTransform>().rect.width / 2);

        for (int i = 0; i < _inventory.items.Count; i++)
        {
            GameObject slot = Instantiate(_slotPrefab, _slotContainer);
            slot.transform.localPosition = new Vector2(-inventorySlotOffset + _slotPrefab.GetComponent<RectTransform>().rect.width * i, 0);
            slot.transform.Find("itemimg").GetComponent<Image>().sprite = _inventory.items[i].GetSprite();
            slot.transform.Find("itemimg").gameObject.SetActive(true);
        }
    }

    private void Inventory_OnInventoryItemsChange(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }
}
