using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    [SerializeField] private int _inventorySize;
    private Inventory _inventory;
    public Inventory inventory { get => _inventory; }
    private UI_Inventory _uiInventory;

    private Journal _journal;
    public Journal journal { get => _journal; }
    private UI_Journal _uiJournal;
    public event EventHandler onOpenJournal;

    private QuestSystem _questSystem;
    public QuestSystem questSystem { get => _questSystem; }
    private UI_QuestSystem _uiQuestSystem;

    [SerializeField] private List<RecipeScriptableObject> _craftingRecipes;
    public List<RecipeScriptableObject> craftingRecipes { get => _craftingRecipes; }

    private void Awake()
    {
        //Init player
        if (GameManager.Instance.player != null) { }
        else GameManager.Instance.player = transform;

        //Get UI references
        _uiInventory = GameManager.Instance.mainCanvas.GetComponentInChildren<UI_Inventory>(true);
        _uiJournal = GameManager.Instance.mainCanvas.GetComponentInChildren<UI_Journal>(true);
        _uiQuestSystem = GameManager.Instance.mainCanvas.GetComponentInChildren<UI_QuestSystem>(true);

        //Crate new inventory and reference it to UI
        _inventory = new Inventory(_inventorySize);
        _uiInventory.SetInventory(_inventory);

        //_journal = new Journal();
        //_uiJournal.SetJournal(_journal);
        //GameManager.Instance.playerControls.Journal.OpenJournal.performed += On_OpenJournal;

        _questSystem = new QuestSystem();
        _uiQuestSystem.SetQuestSystem(_questSystem);
        _questSystem.SetPlayer(this);
        _questSystem.SetInventoryEvent(_inventory);

        SceneManager.activeSceneChanged += OnSceneChanged;
        transform.position = PlayerAssets.Instance.GetSpawnLocationBySceneIndex(GameManager.Instance.currentLevel);   
    }

    private void OnSceneChanged(Scene arg0, Scene arg1)
    {
        GameManager.Instance.currentLevel = arg1.buildIndex;
        transform.position = PlayerAssets.Instance.GetSpawnLocationBySceneIndex(arg1.buildIndex);
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha0))
        {
            _journal.AddPost(new Note { _noteScriptableObject = NoteAssets.Instance.notes[0] });
        }
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

    public void UnlockJournal()
    {
        _journal = new Journal();
        _uiJournal.SetJournal(_journal);
        GameManager.Instance.playerControls.Journal.OpenJournal.performed += On_OpenJournal;
        _questSystem.SetJournalEvent(_journal);
    }
}
