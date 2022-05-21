using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldItem : Interactable
{
    [SerializeField] private Item _item;
    [SerializeField] private SpriteRenderer _spriteRenderer;

    private void Start()
    {
        RefreshItemSprite();
    }

    public override string GetInteractionDescription()
    {
        //Get key name from action map and replace 'key' with it
        //Get item name and replace 'item' with it
        PlayerControls pc = GameManager.Instance.playerControls;
        string description = _interactionDescription.Replace("key", pc.Interactions.Interact.controls[0].name.ToUpper()).Replace("item", _item.GetItemName());
        return description;
    }

    public override void Interact()
    {
        if (GameManager.Instance.player.GetComponent<Player>().TakeWorldItem(_item))
        {
            _canBeInteractedWith = false;
            Destroy(gameObject);
            GameManager.Instance.player.GetComponent<PlayerInteraction>().ForceRemoveInteraction(GetComponent<Interactable>());
        }
    }

    private void RefreshItemSprite()
    {
        _spriteRenderer.sprite = _item.GetItemSprite();
    }

    public void SetItem(Item item)
    {
        _item = item;
        RefreshItemSprite();
    }
}
