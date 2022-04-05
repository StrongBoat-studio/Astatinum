using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    //Inventory
    [SerializeField] private int _inventorySize;
    private Inventory _inventory;
    public Inventory inventory { get => _inventory; }
    [SerializeField] private UI_Inventory _uiInventory;

    //Crafting
    [SerializeField] private List<RecipeScriptableObject> _craftingRecipes;
    public List<RecipeScriptableObject> craftingRecipes { get => _craftingRecipes; }

    //Quests
    private QuestSystem _questSystem;

    private void Awake()
    {
        //Crate new inventory and reference it to UI
        _inventory = new Inventory(_inventorySize);
        _uiInventory.SetInventory(_inventory);

        _questSystem = new QuestSystem();
        _questSystem.AddQuest(new Quest { questData = QuestAssets.Instance.quests[0] });
    }

    public bool TakeWorldItem(Item item)
    {
        return _inventory.AddItem(item); 
    }
}
