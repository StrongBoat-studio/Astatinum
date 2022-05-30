using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEnder : Interactable
{
    public override string GetInteractionDescription()
    {
        return _interactionDescription.Replace("key", GameManager.Instance.playerControls.Interactions.Interact.controls[0].name.ToUpper());
    }

    public override void Interact()
    {
        SceneManager.LoadSceneAsync((int)SceneIndexer.SceneType.GameEnd);
    }
}
