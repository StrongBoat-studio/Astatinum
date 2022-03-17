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
        PlayerControls pc = new PlayerControls();
        string description = _interactionDescription.Replace("key", pc.Player.Interact.controls[0].name.ToUpper()).Replace("item", _item.GetName());
        return description;
    }

    public override void Interact()
    {
        bool tookItem = GameObject.Find("Player").GetComponent<Player>().TakeWorldItem(_item);
        if (tookItem)
        {
            Destroy(gameObject);

            //PlayerInteraction.OnTriggerExit won't fire - object deleted, hide hint text
            GameManager.Instance.player.GetComponent<PlayerInteraction>().ForceRemoveInteraction();
        }
    }

    private void RefreshItemSprite()
    {
        _spriteRenderer.sprite = _item.GetSprite();
    }

    public void SetItem(Item item)
    {
        _item = item;
        RefreshItemSprite();
    }
}
