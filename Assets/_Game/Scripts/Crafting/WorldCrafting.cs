using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//World crafting
//Class reposponsible form connection crafting system to crafting UI
//This class in added to obcject which will be a crafting table
public class WorldCrafting : Interactable
{
    [SerializeField] private Transform craftingUI;
    private CraftingSystem _craftingSystem;
    [SerializeField] private UI_Crafting _uiCrafting;
    [SerializeField] private List<RecipeScriptableObject> _recipes;

    //Crate new crafing system on Awake
    //Connect crafting system to crafting ui
    public void Awake()
    {
        _craftingSystem = new CraftingSystem();
        _uiCrafting.SetCraftingSystem(_craftingSystem);
    }

    public override string GetInteractionDescription()
    {
        //Get key name from action map and replace 'key' with it
        PlayerControls pc = new PlayerControls();
        string description = _interactionDescription.Replace("key", pc.Player.Interact.controls[0].name.ToUpper());
        return description;
    }

    public override void Interact()
    {
        craftingUI.gameObject.SetActive(!craftingUI.gameObject.activeSelf);
        _craftingSystem.SetRecipes(GameManager.Instance.player.GetComponent<Player>().craftingRecipes);
    }

    //If player exited craftings range, turn off UI
    private void OnTriggerExit(Collider other)
    {
        if(other.tag == "Player")
        {
            craftingUI.gameObject.SetActive(false);
        }
    }
}
