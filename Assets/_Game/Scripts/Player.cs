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
        inventory.AddItem(new Item { itemData = ItemAssets.Instance.itemsData[0] });
        SceneManager.sceneLoaded += OnSceneLoaded;
        SceneManager.sceneUnloaded += OnSceneUnloaded;
        transform.position = PlayerAssets.Instance.GetSpawnLocationBySceneIndex(GameManager.Instance.currentLevelSceneIndex);
    }

    private void OnSceneLoaded(Scene arg0, LoadSceneMode arg1)
    {
        Debug.Log("Player OnSceneLoaded - set pos for: " + ((SceneIndexer.SceneType)arg0.buildIndex).ToString() + " with vals: " + PlayerAssets.Instance.GetSpawnLocationBySceneIndex(GameManager.Instance.currentLevelSceneIndex));
        //GameManager.Instance.mainCanvas.Find("Debug").GetComponent<TMPro.TextMeshProUGUI>().text += "\nPlayer OnSceneLoaded - set pos";
        transform.position = PlayerAssets.Instance.GetSpawnLocationBySceneIndex(GameManager.Instance.currentLevelSceneIndex);
    }

    private void OnSceneUnloaded(Scene arg0)
    {
        Debug.Log("Player OnSceneUnloaded");
        if (arg0.buildIndex == (int)SceneIndexer.SceneType.BathroomCutscene)
        {
            questSystem.AddQuest(QuestAssets.Instance.CreateQuest(QuestData.QuestType.Talk, 400));
        }
    }

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Alpha0))
        {
            _journal.AddPost(new Note { _noteData = NoteAssets.Instance.notes[0] });
            _journal.AddPost(new Note { _noteData = NoteAssets.Instance.notes[1] });
            _journal.AddPost(new Note { _noteData = NoteAssets.Instance.notes[2] });
            _journal.AddPost(new Note { _noteData = NoteAssets.Instance.notes[3] });
            _journal.AddPost(new Note { _noteData = NoteAssets.Instance.notes[4] });
            _journal.AddPost(new Note { _noteData = NoteAssets.Instance.notes[5] });
            _journal.AddPost(new Note { _noteData = NoteAssets.Instance.notes[6] });
            _journal.AddPost(new Note { _noteData = NoteAssets.Instance.notes[7] });
            _journal.AddPost(new Note { _noteData = NoteAssets.Instance.notes[8] });
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
        _journal = new Journal();
        _uiJournal.SetJournal(_journal);
        GameManager.Instance.playerControls.Journal.OpenJournal.performed += On_OpenJournal;
        _questSystem.SetJournalEvent(_journal);
    }
}
