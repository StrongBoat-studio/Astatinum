using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CamZone : MonoBehaviour
{
    [SerializeField] private CinemachineVirtualCamera _vCam = null;

    private void Start()
    {
        _vCam.enabled = false;
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _vCam.Follow = other.transform;
            _vCam.enabled = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if(other.CompareTag("Player"))
        {
            _vCam.enabled = false;
        }
    }
}
