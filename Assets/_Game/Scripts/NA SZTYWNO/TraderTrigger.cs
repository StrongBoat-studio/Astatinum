using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TraderTrigger : MonoBehaviour
{
    private bool canTrigger = true;
    [SerializeField] private TextAsset _dialogue;

    private void OnTriggerEnter(Collider other)
    {
        if (canTrigger)
        {
            canTrigger = false;
            DialogueManager.Instance.EnterDialogueMode(_dialogue);
        }
    }
}
