using Player;
using UnityEngine;

public class RoomService : MonoBehaviour
{
    [SerializeField] Camera roomCamera;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerView>() != null)
            roomCamera.transform.position = transform.position;
    }
}
