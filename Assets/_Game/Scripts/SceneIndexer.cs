using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SceneIndexer : MonoBehaviour
{
    //Scenes with indexes from unity build window
    public enum SceneType
    {
        None = -1,
        MainMenu = 0,
        AuthorsMenu = 2,
        OptionsMenu = 3,
        TutorialScene = 1,
        LocationOneScene = 5,
        DialogueScene = 4,
        DomEzila = 6,   
        Junkyard = 7,
        AstaRoom = 8,
        Bathroom = 9,
        PlayerObjects = 10,
        SettingsScene = 11,
        GameStartCutscene = 12,
        SceneLoader = 13,
        BathroomCutscene = 14,
        RoadScene = 15,
        TutorialScene2 = 16,
        Mar3KCutscene = 17,
        AstaRoom2 = 18,
        Bathroom2 = 19,
        DomEzila2 = 20,
        Podworko2 = 21,
        GameEnd = 22,
    }

    public static SceneIndexer Instance { get; private set; }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else Destroy(this);
    }

    public string GetSceneNameByType(SceneType type)
    {
        switch(type)
        {
            case SceneType.MainMenu:
                return _mainMenuSceneName;
            case SceneType.AuthorsMenu:
                return _authorsMenuSceneName;
            case SceneType.OptionsMenu: 
                return _optionsMenuSceneName;
            case SceneType.TutorialScene:
                return _tutorialSceneName;
            case SceneType.LocationOneScene:
                return _locationOneSceneName;
            case SceneType.DialogueScene:
                return _dialogueSceneName;
            case SceneType.DomEzila:
                return _domEzilaName;
            case SceneType.Junkyard:
                return _jankyardName;
            case SceneType.AstaRoom:
                return _astaRoom;
            case SceneType.Bathroom:
                return _bathroom;
            case SceneType.RoadScene:
                return _roadScene;
            case SceneType.TutorialScene2:
                return _tutorialScene2;
            case SceneType.AstaRoom2:
                return _astaRoom2;
            case SceneType.Bathroom2:
                return _bathroom2;
            case SceneType.DomEzila2:
                return _domEzila2;
            case SceneType.Podworko2:
                return _podworko2;
            default:
                return "Invalid scene";
        }
    }

    //Location names
    private string _tutorialSceneName = "Warsztat";
    private string _locationOneSceneName = "Location one";
    private string _mainMenuSceneName = "Menu g³ówne";
    private string _authorsMenuSceneName = "Autorzy";
    private string _optionsMenuSceneName = "Opcje";
    private string _dialogueSceneName = "Dialogue scene";
    private string _domEzilaName = "Dom Ezila";
    private string _jankyardName = "Z³omowisko";
    private string _astaRoom = "Pokój Asty";
    private string _bathroom = "£azienka";
    private string _roadScene = "Droga";
    private string _tutorialScene2 = "Warsztat";
    private string _astaRoom2 = "Pokój Asty";
    private string _bathroom2 = "£azienka";
    private string _domEzila2 = "Dom Ezila";
    private string _podworko2 = "Podwórko 2";

}
