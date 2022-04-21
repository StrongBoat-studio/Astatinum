using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField] private Transform _Player;
    [SerializeField] private float _Speed = 10f;
    //SerializeField] private Vector3 _Distance;


    private void Update()
    {

        transform.position= new Vector3(_Player.position.x, 0.8f, -3.9f);

    }
}
