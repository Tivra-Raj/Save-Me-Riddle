using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class CameraManager : MonoBehaviour
{
    [SerializeField] Camera Roomcam;
    //[SerializeField] List<Transform> roomLocation;

    public void MoveCamera(int roomIndex = 0)
    {
        //Roomcam.transform.position = roomLocation[roomIndex].position;
        Roomcam.transform.position = transform.position;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        MoveCamera();
    }
}
