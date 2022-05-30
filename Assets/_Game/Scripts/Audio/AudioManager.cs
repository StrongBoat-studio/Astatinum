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
    [SerializeField] private EventReference _musicAmbienceWind1;
    [SerializeField] private EventReference _musicAmbienceWind2;
    [SerializeField] private EventReference _musicAmbienceWind3;
    [SerializeField] private EventReference _musicAmbienceWind4;

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
    [SerializeField] private EventReference _gameJournalNextPage1;
    [SerializeField] private EventReference _gameJournalNextPage2;
    [SerializeField] private EventReference _gameMar3KHappy;
    [SerializeField] private EventReference _gameMar3KStartup;
    [SerializeField] private EventReference _gameQuestBegin;
    [SerializeField] private EventReference _gameKey1;
    [SerializeField] private EventReference _gameKey2;
    [SerializeField] private EventReference _gameKey3;
    [SerializeField] private EventReference _gameMetal1;
    [SerializeField] private EventReference _gameMetal2;
    [SerializeField] private EventReference _gameMetal3;
    [SerializeField] private EventReference _gameMetal4;
    [SerializeField] private EventReference _gameAstaWalkGrass1;
    [SerializeField] private EventReference _gameAstaWalkGrass2;
    [SerializeField] private EventReference _gameAstaWalkGrass3;
    [SerializeField] private EventReference _gameAstaWalkGrass4;
    [SerializeField] private EventReference _gameAstaWalkGrass5;
    [SerializeField] private EventReference _gameAstaWalkGrass6;
    [SerializeField] private EventReference _gameAstaWalkSolid1;
    [SerializeField] private EventReference _gameAstaWalkSolid2;
    [SerializeField] private EventReference _gameAstaWalkSolid3;
    [SerializeField] private EventReference _gameAstaWalkSolid4;
    [SerializeField] private EventReference _gameAstaWalkSolid5;
    [SerializeField] private EventReference _gameAstaWalkSolid6;

    [Header("Footsteps")]
    public EventReference gameAstaFootsteps;

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
    public void PlayMusicAmbience1()    { if (!_musicAmbience1.IsNull) RuntimeManager.PlayOneShot(_musicAmbience1); }
    public void PlayMusicAmbience2()    { if (!_musicAmbience2.IsNull) RuntimeManager.PlayOneShot(_musicAmbience2); }
    public void PlayMusicAmbienceWind1()    { if (!_musicAmbienceWind1.IsNull) RuntimeManager.PlayOneShot(_musicAmbienceWind1); }
    public void PlayMusicAmbienceWind2()    { if (!_musicAmbienceWind2.IsNull) RuntimeManager.PlayOneShot(_musicAmbienceWind2); }
    public void PlayMusicAmbienceWind3()    { if (!_musicAmbienceWind3.IsNull) RuntimeManager.PlayOneShot(_musicAmbienceWind3); }
    public void PlayMusicAmbienceWind4()    { if (!_musicAmbienceWind4.IsNull) RuntimeManager.PlayOneShot(_musicAmbienceWind4); }

    //UI
    public void PlayUIButtonHover()     { if (!_uiButtonHover.IsNull) RuntimeManager.PlayOneShot(_uiButtonHover); }
    public void PlayUIButtonClick()     { if (!_uiButtonClick.IsNull) RuntimeManager.PlayOneShot(_uiButtonClick); }

    //Game
    public void PlayGameCollect1()      { if (!_gameCollect1.IsNull) RuntimeManager.PlayOneShot(_gameCollect1); }
    public void PlayGameCollect2()      { if (!_gameCollect2.IsNull) RuntimeManager.PlayOneShot(_gameCollect2); }
    public void PlayGameCollect3()      { if (!_gameCollect3.IsNull) RuntimeManager.PlayOneShot(_gameCollect3); }
    public void PlayGameCollect4()      { if (!_gameCollect4.IsNull) RuntimeManager.PlayOneShot(_gameCollect4); }
    public void PlayGameCraftingOpen()  { if (!_gameCraftingOpen.IsNull) RuntimeManager.PlayOneShot(_gameCraftingOpen); }
    public void PlayGameCraftingDone()  { if (!_gameCraftingDone.IsNull) RuntimeManager.PlayOneShot(_gameCraftingDone); }
    public void PlayGameDoor1()         { if (!_gameDoor1.IsNull) RuntimeManager.PlayOneShot(_gameDoor1); }
    public void PlayGameDoor2()         { if (!_gameDoor2.IsNull) RuntimeManager.PlayOneShot(_gameDoor2); }
    public void PlayGameJournalOpen1()  { if (!_gameJournalOpen1.IsNull) RuntimeManager.PlayOneShot(_gameJournalOpen1); }
    public void PlayGameJournalOpen2()  { if (!_gameJournalOpen2.IsNull) RuntimeManager.PlayOneShot(_gameJournalOpen2); }
    public void PlayGameJournalNextPage1()  { if (!_gameJournalNextPage1.IsNull) RuntimeManager.PlayOneShot(_gameJournalNextPage1); }
    public void PlayGameJournalNextPage2()  { if (!_gameJournalNextPage2.IsNull) RuntimeManager.PlayOneShot(_gameJournalNextPage2); }
    public void PlayGameMar3KHappy()    { if (!_gameMar3KHappy.IsNull) RuntimeManager.PlayOneShot(_gameMar3KHappy); }
    public void PlayGameMar3KStartup()  { if (!_gameMar3KStartup.IsNull) RuntimeManager.PlayOneShot(_gameMar3KStartup); }
    public void PlayGameQuestBegin()    { if (!_gameQuestBegin.IsNull) RuntimeManager.PlayOneShot(_gameQuestBegin); }
    public void PlayGameKey1()    { if (!_gameKey1.IsNull) RuntimeManager.PlayOneShot(_gameKey1); }
    public void PlayGameKey2()    { if (!_gameKey2.IsNull) RuntimeManager.PlayOneShot(_gameKey2); }
    public void PlayGameKey3()    { if (!_gameKey3.IsNull) RuntimeManager.PlayOneShot(_gameKey3); }
    public void PlayGameMetal1()    { if (!_gameMetal1.IsNull) RuntimeManager.PlayOneShot(_gameMetal1); }
    public void PlayGameMetal2()    { if (!_gameMetal2.IsNull) RuntimeManager.PlayOneShot(_gameMetal2); }
    public void PlayGameMetal3()    { if (!_gameMetal3.IsNull) RuntimeManager.PlayOneShot(_gameMetal3); }
    public void PlayGameMetal4()    { if (!_gameMetal4.IsNull) RuntimeManager.PlayOneShot(_gameMetal4); }
    public void PlayGameAstaWalkGrass1()    { if (!_gameAstaWalkGrass1.IsNull) RuntimeManager.PlayOneShot(_gameAstaWalkGrass1); }
    public void PlayGameAstaWalkGrass2()    { if (!_gameAstaWalkGrass2.IsNull) RuntimeManager.PlayOneShot(_gameAstaWalkGrass2); }
    public void PlayGameAstaWalkGrass3()    { if (!_gameAstaWalkGrass3.IsNull) RuntimeManager.PlayOneShot(_gameAstaWalkGrass3); }
    public void PlayGameAstaWalkGrass4()    { if (!_gameAstaWalkGrass4.IsNull) RuntimeManager.PlayOneShot(_gameAstaWalkGrass4); }
    public void PlayGameAstaWalkGrass5()    { if (!_gameAstaWalkGrass5.IsNull) RuntimeManager.PlayOneShot(_gameAstaWalkGrass5); }
    public void PlayGameAstaWalkGrass6()    { if (!_gameAstaWalkGrass6.IsNull) RuntimeManager.PlayOneShot(_gameAstaWalkGrass6); }
    public void PlayGameAstaWalkSolid1() { if (!_gameAstaWalkSolid1.IsNull) RuntimeManager.PlayOneShot(_gameAstaWalkSolid1); }
    public void PlayGameAstaWalkSolid2() { if (!_gameAstaWalkSolid2.IsNull) RuntimeManager.PlayOneShot(_gameAstaWalkSolid2); }
    public void PlayGameAstaWalkSolid3() { if (!_gameAstaWalkSolid3.IsNull) RuntimeManager.PlayOneShot(_gameAstaWalkSolid3); }
    public void PlayGameAstaWalkSolid4() { if (!_gameAstaWalkSolid4.IsNull) RuntimeManager.PlayOneShot(_gameAstaWalkSolid4); }
    public void PlayGameAstaWalkSolid5() { if (!_gameAstaWalkSolid5.IsNull) RuntimeManager.PlayOneShot(_gameAstaWalkSolid5); }
    public void PlayGameAstaWalkSolid6() { if (!_gameAstaWalkSolid6.IsNull) RuntimeManager.PlayOneShot(_gameAstaWalkSolid6); }

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

    public void PlayGameAstaWalkGrassRandom()
    {
        int r = Random.Range(0, 6);
        switch (r)
        {
            case 0: PlayGameAstaWalkGrass1(); break;
            case 1: PlayGameAstaWalkGrass2(); break;
            case 2: PlayGameAstaWalkGrass3(); break;
            case 3: PlayGameAstaWalkGrass4(); break;
            case 4: PlayGameAstaWalkGrass5(); break;
            case 5: PlayGameAstaWalkGrass6(); break;
        }
    }

    public void PlayGameAstaWalkSoildRandom()
    {
        int r = Random.Range(0, 6);
        switch (r)
        {
            case 0: PlayGameAstaWalkSolid1(); break;
            case 1: PlayGameAstaWalkSolid2(); break;
            case 2: PlayGameAstaWalkSolid3(); break;
            case 3: PlayGameAstaWalkSolid4(); break;
            case 4: PlayGameAstaWalkSolid5(); break;
            case 5: PlayGameAstaWalkSolid6(); break;
        }
    }

    public void PlayGameKeyRandom()
    {
        int r = Random.Range(0, 3);
        switch (r)
        {
            case 0: PlayGameKey1(); break;
            case 1: PlayGameKey2(); break;
            case 2: PlayGameKey3(); break;
        }
    }

    public void PlayGameMetalRandom()
    {
        int r = Random.Range(0, 4);
        switch (r)
        {
            case 0: PlayGameMetal1(); break;
            case 1: PlayGameMetal2(); break;
            case 2: PlayGameMetal3(); break;
            case 3: PlayGameMetal4(); break;
        }
    }
}
