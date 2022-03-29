using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UI_ItemDrag : MonoBehaviour
{
    public static UI_ItemDrag Instance { get; private set; }

    private Canvas _canvas;
    private CanvasGroup _canvasGroup;
    private Item _item;
    private Vector2 _originPostion;
    private IItemHolder _itemHolder;

    private void Awake()
    {
        Instance = this;

        _canvas = GetComponentInParent<Canvas>();
        _canvasGroup = GetComponent<CanvasGroup>();
    }
    
    public Item GetItem()
    {
        return _item;
    }

    public void SetItem(Item item)
    {
        _item = item;
    }

    public void SetItemHolder(IItemHolder itemHolder)
    {
        _itemHolder = itemHolder;
    }

    public IItemHolder GetItemHolder()
    {
        return _itemHolder;
    }

    public void Hide()
    {
        gameObject.SetActive(false);
    }

    public void Show(Item item, Vector2 originPosition, IItemHolder itemHolder)
    {
        gameObject.SetActive(true);

        SetItem(item);
        SetOriginPosition(originPosition);
        SetItemHolder(itemHolder);
    }

    public void SetOriginPosition(Vector2 pos)
    {
        _originPostion = pos;
    }

    public Vector2 GetOriginPosition()
    {
        return _originPostion;
    }
}
