using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using System;

public class Player : MonoBehaviour
{
    [SerializeField] private int _inventorySize;
    private Inventory _inventory;
    public Inventory inventory { get => _inventory; }
    [SerializeField] private UI_Inventory _uiInventory;

    private Journal _journal;
    public Journal journal { get => _journal; }
    [SerializeField] private UI_Journal _uiJournal;
    public event EventHandler onOpenJournal;

    private QuestSystem _questSystem;
    public QuestSystem questSystem { get => _questSystem; }
    [SerializeField] private UI_QuestSystem _uiQuestSystem;

    [SerializeField] private List<RecipeScriptableObject> _craftingRecipes;
    public List<RecipeScriptableObject> craftingRecipes { get => _craftingRecipes; } 

    private void Awake()
    {
        //Crate new inventory and reference it to UI
        _inventory = new Inventory(_inventorySize);
        _uiInventory.SetInventory(_inventory);

        _journal = new Journal();
        _uiJournal.SetJournal(_journal);
        GameManager.Instance.playerControls.Journal.OpenJournal.performed += On_OpenJournal;

        _questSystem = new QuestSystem();
        _uiQuestSystem.SetQuestSystem(_questSystem);
        _questSystem.AddQuest(new QuestFindItems(QuestAssets.Instance.questFindItem[0], QuestAssets.Instance.questFindItemGoal[0]));
        _questSystem.AddQuest(new QuestFindItems(QuestAssets.Instance.questFindItem[1], QuestAssets.Instance.questFindItemGoal[1]));
    }

    private void On_OpenJournal(InputAction.CallbackContext obj)
    {
        if (_uiJournal.gameObject.activeSelf)
            _uiJournal.gameObject.SetActive(false);
        else if (!_uiJournal.gameObject.activeSelf)
        {
            _uiJournal.gameObject.SetActive(true);
            onOpenJournal?.Invoke(this, EventArgs.Empty);
        }
    }

    public bool TakeWorldItem(Item item)
    {
        return _inventory.AddItem(item); 
    }

    public bool TakeWorldNote(Note note)
    {
        return _journal.AddPost(note);
    }
}
