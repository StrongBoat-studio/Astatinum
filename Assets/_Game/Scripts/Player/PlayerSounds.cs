using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSounds : MonoBehaviour
{
    public void PlayWalkEvent()
    {
        GetComponentInParent<Movement>().PlayWalkEvenet();
    }
}
