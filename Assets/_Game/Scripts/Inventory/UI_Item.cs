using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

//UI item
//Class represents item displayed on UI
public class UI_Item : MonoBehaviour, IPointerDownHandler, IBeginDragHandler, IEndDragHandler, IDragHandler, IPointerClickHandler
{
    private Canvas _canvas;
    private CanvasGroup _canvasGroup;
    private Image _image;
    private Item _item;

    private void Awake()
    {
        //Get references for canvas, canvas group and image
        _canvas = GetComponentInParent<Canvas>();
        _canvasGroup = GetComponentInParent<CanvasGroup>();
        _image = GetComponent<Image>();
    }

    public void OnBeginDrag(PointerEventData eventData)
    {
        _canvasGroup.blocksRaycasts = false;
        _canvasGroup.alpha = .5f;
        
        //Set currently dragged item
        UI_ItemDrag.Instance.Show(_item, eventData.pointerDrag.transform.localPosition, GetComponentInParent<IItemHolder>());
    }

    public void OnDrag(PointerEventData eventData)
    {
        //Move item with mouse
        transform.GetComponent<RectTransform>().anchoredPosition += eventData.delta / _canvas.scaleFactor;
    }

    public void OnEndDrag(PointerEventData eventData)
    {
        if (!eventData.eligibleForClick) transform.localPosition = UI_ItemDrag.Instance.GetOriginPosition();
        _canvasGroup.blocksRaycasts = true;
        _canvasGroup.alpha = 1f;

        //Remove currently dragged item
        UI_ItemDrag.Instance.Hide();
    }

    public void OnPointerDown(PointerEventData eventData)
    {
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        //Check if item has been clicked with rmb or lmb
        if(eventData.button == PointerEventData.InputButton.Left)
        {
            //lmb = use item if item has a use action assigned
            if (GameManager.Instance.player.GetComponent<Player>().inventory.GetFirstInventorySlotWithItemOfItemData(_item.itemData) != null) { }
            else return;   
            
            if(_item.itemData.itemUseAction != null)
                _item.itemData.itemUseAction.Do(_item);
        }
        else if(eventData.button == PointerEventData.InputButton.Right)
        {
            //rmb = drop item if it can be removed (spawn item prefab and remove from inventory)
            if(GameManager.Instance.player.GetComponent<Player>().inventory.RemoveItem(_item))
            {
                Instantiate(ItemAssets.Instance.worldItemPrefab, GameManager.Instance.player.position, Quaternion.identity).GetComponent<WorldItem>().SetItem(_item);
            }
        }
    }

    public Item GetItem()
    {
        return _item;
    }

    public void SetItem(Item item)
    {
        _item = item;
    }

    public void SetSprite(Sprite sprite)
    {
        _image.sprite = sprite;
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
