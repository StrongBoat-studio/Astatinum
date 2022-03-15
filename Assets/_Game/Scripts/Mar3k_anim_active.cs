using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mar3k_anim_active : MonoBehaviour
{
    [SerializeField] private GameObject _Mar3k;
    [SerializeField] private Animator _Anim;

    void Start()
    {
        _Mar3k.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.R))
        {
            _Mar3k.SetActive(true);
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            _Mar3k.SetActive(false);
        }

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
        {
            _Anim.SetBool("Mar3k_Walk_Right", true);
            _Anim.SetBool("Mar3k_Walk_Left", false);
        }

        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
        {
            _Anim.SetBool("Mar3k_Walk_Right", false);
            _Anim.SetBool("Mar3k_Walk_Left", false);
        }

        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
        {
            _Anim.SetBool("Mar3k_Walk_Right", false);
            _Anim.SetBool("Mar3k_Walk_Left", true);
        }


        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
        {
            _Anim.SetBool("Mar3k_Walk_Right", false);
            _Anim.SetBool("Mar3k_Walk_Left", false);
        }
    }
}
