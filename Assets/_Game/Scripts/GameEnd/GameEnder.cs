using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnder : Interactable
{
    [SerializeField] private List<string> _interactConditions;
    [SerializeField] private string _interactionDescriptionFail;

    public override string GetInteractionDescription()
    {
        if (CanInteract())
            return _interactionDescription.Replace("key", GameManager.Instance.playerControls.Interactions.Interact.controls[0].name.ToUpper());
        else
            return _interactionDescriptionFail;
    }

    public override void Interact()
    {
        if(CanInteract())
            SceneManager.LoadSceneAsync((int)SceneIndexer.SceneType.GameEnd);
    }

    private bool CanInteract()
    {
        if (_interactConditions == null || _interactConditions.Count == 0) return true;

        bool canInteract = true;

        foreach (string condition in _interactConditions)
        {
            string prefix = condition.Split(':')[0];
            string param = condition.Split(':')[1];

            switch (prefix)
            {
                case "dialoguefinished":
                    if (GameManager.Instance.player.GetComponent<Player>().hadDialogueWith.Contains(param)) { }
                    else return false;
                    break;
                default:
                    break;
            }
        }

        return canInteract;
    }
}
