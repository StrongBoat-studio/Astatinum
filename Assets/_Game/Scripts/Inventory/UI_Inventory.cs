using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Inventory : MonoBehaviour
{
    private Inventory _inventory;
    [SerializeField] private GameObject _slotPrefab;
    [SerializeField] private Transform _slotContainer;

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
        //Clear slots parent
        if(_slotContainer.childCount > 0)
            foreach(Transform slot in _slotContainer)
                Destroy(slot.gameObject);

        //Generate new inventory slots
        float inventorySlotOffset = (_slotContainer.GetComponent<RectTransform>().rect.width / 2) - (_slotPrefab.GetComponent<RectTransform>().rect.width / 2);

        int i = 0;
        foreach (Item item in _inventory.items)
        {
            GameObject slot = Instantiate(_slotPrefab, _slotContainer);
            slot.transform.localPosition = new Vector2(-inventorySlotOffset + _slotPrefab.GetComponent<RectTransform>().rect.width * i, 0);
            slot.transform.Find("itemimg").GetComponent<Image>().sprite = item.GetSprite();
            slot.transform.Find("itemimg").gameObject.SetActive(true);

            //Add method for dropping items
            slot.GetComponent<Button>().onClick.AddListener(() => {
                GameManager.Instance.player.GetComponent<Player>().DropWorldItem(item);
            });

            i++;
        }
    }

    private void Inventory_OnInventoryItemsChange(object sender, System.EventArgs e)
    {
        RefreshInventoryItems();
    }
}
