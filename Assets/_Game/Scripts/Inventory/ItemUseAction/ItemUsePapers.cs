using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Items/Item use action - Papers")]
public class ItemUsePapers : ItemUseAction
{
    public Transform prefab;

    public override void Do(Item item)
    {
        //Spawn prefab in canvas
        RectTransform rt = Instantiate(prefab, GameManager.Instance.mainCanvas).GetComponent<RectTransform>();
    }
}
