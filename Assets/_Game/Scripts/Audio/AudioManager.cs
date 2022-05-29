using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("Music")]
    [SerializeField] private EventReference _musicAmbience1;
    [SerializeField] private EventReference _musicAmbience2;

    [Header("UI")]
    [SerializeField] private EventReference _uiButtonHover;
    [SerializeField] private EventReference _uiButtonClick;

    [Header("Game")]
    [SerializeField] private EventReference _gameCollect1;
    [SerializeField] private EventReference _gameCollect2;
    [SerializeField] private EventReference _gameCollect3;
    [SerializeField] private EventReference _gameCollect4;
    [SerializeField] private EventReference _gameCraftingOpen;
    [SerializeField] private EventReference _gameCraftingDone;
    [SerializeField] private EventReference _gameDoor1;
    [SerializeField] private EventReference _gameDoor2;
    [SerializeField] private EventReference _gameJournalOpen1;
    [SerializeField] private EventReference _gameJournalOpen2;
    [SerializeField] private EventReference _gameMar3KHappy;
    [SerializeField] private EventReference _gameMar3KStartup;
    [SerializeField] private EventReference _gameQuestBegin;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(this);

        DontDestroyOnLoad(this);
    }

    //Music
    public void PlayMusicAmbience1() { if (_musicAmbience1.Path != null) RuntimeManager.PlayOneShot(_musicAmbience1); }
    public void PlayMusicAmbience2() { if (_musicAmbience2.Path != null) RuntimeManager.PlayOneShot(_musicAmbience2); }

    //UI
    public void PlayUIButtonHover() { if (_uiButtonHover.Path != null) RuntimeManager.PlayOneShot(_uiButtonHover); }
    public void PlayUIButtonClick() { if (_uiButtonClick.Path != null) RuntimeManager.PlayOneShot(_uiButtonClick); }

    //Game
    public void PlayGameCollect1() { if (_gameCollect1.Path != null) RuntimeManager.PlayOneShot(_gameCollect1); }
    public void PlayGameCollect2() { if (_gameCollect2.Path != null) RuntimeManager.PlayOneShot(_gameCollect2); }
    public void PlayGameCollect3() { if (_gameCollect3.Path != null) RuntimeManager.PlayOneShot(_gameCollect3); }
    public void PlayGameCollect4() { if (_gameCollect4.Path != null) RuntimeManager.PlayOneShot(_gameCollect4); }
    public void PlayGameCraftingOpen() { if (_gameCraftingOpen.Path != null) RuntimeManager.PlayOneShot(_gameCraftingOpen); }
    public void PlayGameCraftingDone() { if (_gameCraftingDone.Path != null) RuntimeManager.PlayOneShot(_gameCraftingDone); }
    public void PlayGameDoor1() { if (_gameDoor1.Path != null) RuntimeManager.PlayOneShot(_gameDoor1); }
    public void PlayGameDoor2() { if (_gameDoor2.Path != null) RuntimeManager.PlayOneShot(_gameDoor2); }
    public void PlayGameJournalOpen1() { if (_gameJournalOpen1.Path != null) RuntimeManager.PlayOneShot(_gameJournalOpen1); }
    public void PlayGameJournalOpen2() { if (_gameJournalOpen2.Path != null) RuntimeManager.PlayOneShot(_gameJournalOpen2); }
    public void PlayGameMar3KHappy() { if (_gameMar3KHappy.Path != null) RuntimeManager.PlayOneShot(_gameMar3KHappy); }
    public void PlayGameMar3KStartup() { if (_gameMar3KStartup.Path != null) RuntimeManager.PlayOneShot(_gameMar3KStartup); }
    public void PlayGameQuestBegin() { if (_gameQuestBegin.Path != null) RuntimeManager.PlayOneShot(_gameQuestBegin); }

    //Play random
    public void PlayGameCollectRandom()
    {
        int r = Random.Range(0, 4);
        switch(r)
        {
            case 0: PlayGameCollect1(); break;
            case 1: PlayGameCollect2(); break;
            case 2: PlayGameCollect3(); break;
            case 3: PlayGameCollect4(); break;
        }
    }

    public void PlayGameDoorRandom()
    {
        int r = Random.Range(0, 2);
        switch (r)
        {
            case 0: PlayGameDoor1(); break;
            case 1: PlayGameDoor2(); break;
        }
    }
}
