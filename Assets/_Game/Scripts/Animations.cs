using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    [SerializeField] private Animator _Anim;
    private bool _Player;

    void Start()
    {
        _Anim.SetTrigger("Idle");
    }

    void Update()
    {
        if(GameManager.Instance.player.GetComponent<Rigidbody>().velocity != Vector3.zero)
        {
            
        }
    }
}
