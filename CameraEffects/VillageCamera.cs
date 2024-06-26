using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class VillageCamera : MonoBehaviour
{
    private CinemachineVirtualCamera cam;

    private void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();

        cam.Follow = FindObjectOfType<PlayerMovement>().transform;
    }
}
