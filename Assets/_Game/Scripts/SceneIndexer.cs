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
        Cutscenes = 12,
        SceneLoader = 13,
        BathroomCutscene = 14,
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

}
