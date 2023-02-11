using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class RoomManager : MonoBehaviour
{
    [SerializeField] Camera Roomcam;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Roomcam.transform.position = transform.position;
    }
}
