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

    //Na sztywno :)
    public List<string> hadDialogueWith;

    private void Awake()
    {
        hadDialogueWith = new List<string>();

        //Get UI references
        _uiInventory = GameManager.Instance.mainCanvas.GetComponentInChildren<UI_Inventory>(true);
        _uiJournal = GameManager.Instance.mainCanvas.GetComponentInChildren<UI_Journal>(true);
        _uiQuestSystem = GameManager.Instance.mainCanvas.GetComponentInChildren<UI_QuestSystem>(true);

        //Crate new inventory and reference it to UI
        _inventory = new Inventory(_inventorySize);
        _uiInventory.SetInventory(_inventory);

        //Quest system
        _questSystem = new QuestSystem();
        _uiQuestSystem.SetQuestSystem(_questSystem);
        _questSystem.SetPlayer(this);
        _questSystem.SetInventoryEvent(_inventory);

        //Scene change event
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
        //transform.position = PlayerAssets.Instance.GetSpawnLocationBySceneIndex(GameManager.Instance.currentLevelSceneIndex);
        transform.position = new Vector3(-39.2f, 0.38f, 7.6f);
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        if (arg0.buildIndex == (int)SceneIndexer.SceneType.OptionsMenu) return;

        if (arg0.buildIndex == (int)SceneIndexer.SceneType.BathroomCutscene)
        {
            transform.position = new Vector3(-39.2f, 0.38f, 7.6f);
            return;
        }
        transform.position = PlayerAssets.Instance.GetSpawnLocationBySceneIndex(GameManager.Instance.currentLevelSceneIndex);
    }

    private void OnSceneUnloaded(Scene arg0)
    {
        if (arg0.buildIndex == (int)SceneIndexer.SceneType.BathroomCutscene)
        {
            questSystem.AddQuest(QuestAssets.Instance.CreateQuest(QuestData.QuestType.Talk, 400));
        }
    }

    private void Update()
    {
        
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

    private void OnDestroy()
    {
        //Unsubscribe events
        SceneManager.sceneLoaded -= OnSceneLoaded;
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
        if (_journal != null) return;

        _journal = new Journal();
        _uiJournal.SetJournal(_journal);
        GameManager.Instance.playerControls.Journal.OpenJournal.performed += On_OpenJournal;
        _questSystem.SetJournalEvent(_journal);
    }
}
