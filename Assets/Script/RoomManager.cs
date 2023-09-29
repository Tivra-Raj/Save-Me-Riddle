using UnityEngine;

public class RoomService : MonoBehaviour
{
    [SerializeField] Camera roomCamera;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        roomCamera.transform.position = transform.position;
    }
}
