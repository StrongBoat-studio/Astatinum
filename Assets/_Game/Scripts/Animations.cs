using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Animations : MonoBehaviour
{
    [SerializeField] private Animator _Anim;
    private bool _Player;

    void Start()
    {
        _Anim.SetBool("A", true);
        _Anim.SetBool("E", false);
        _Player = true;
    }

    void Update()
    {
        

        if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
            {
                if(_Player==true)
                    {
                        _Anim.SetBool("Asta_walk_right", true);
                        _Anim.SetBool("Asta_walk_left", false);
                    }
                if (_Player==false)
                    {
                        _Anim.SetBool("Ezil_walk_right", true);
                        _Anim.SetBool("Ezil_walk_left", false);
                    }

        }
        if (Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D))
            {
                _Anim.SetBool("Asta_walk_right", false);
                _Anim.SetBool("Asta_walk_left", false);
                _Anim.SetBool("Ezil_walk_right", false);
                _Anim.SetBool("Ezil_walk_left", false);
            }
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
            {
                if (_Player == true)
                    {
                        _Anim.SetBool("Asta_walk_right", false);
                        _Anim.SetBool("Asta_walk_left", true);
                    }
                if (_Player == false)
                    {
                        _Anim.SetBool("Ezil_walk_right", false);
                        _Anim.SetBool("Ezil_walk_left", true);
                    }

        }
        if (Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A))
            {
                _Anim.SetBool("Asta_walk_right", false);
                _Anim.SetBool("Asta_walk_left", false);
                _Anim.SetBool("Ezil_walk_right", false);
                _Anim.SetBool("Ezil_walk_left", false);
        } 

        if (Input.GetKeyDown(KeyCode.R))
            {
                _Anim.SetBool("A", true);
                _Anim.SetBool("E", false);
                _Player = true;
                _Anim.SetBool("Asta_walk_right", false);
                _Anim.SetBool("Asta_walk_left", false);
            }

        if (Input.GetKeyDown(KeyCode.E))
            {
                _Anim.SetBool("A", false);
                _Anim.SetBool("E", true);
                _Player = false;
                _Anim.SetBool("Asta_walk_right", false);
                _Anim.SetBool("Asta_walk_left", false);
            }

    }
}
