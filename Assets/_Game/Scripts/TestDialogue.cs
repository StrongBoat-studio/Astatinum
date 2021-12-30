using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestDialogue : MonoBehaviour
{
    [SerializeField] private TextAsset inkJson;

    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Return))
        {
            DialogueManager.Instance.EnterDialogueMode(inkJson);
        }
    }
}
