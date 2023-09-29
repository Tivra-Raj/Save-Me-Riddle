using Player;
using UnityEngine;

namespace Events
{
    public class DoorOpenEvent : MonoBehaviour
    {
        [SerializeField] private GameObject Room1Key;
        [SerializeField] private GameObject Room2Key;
        [SerializeField] private BoxCollider2D DoorCollider;

        private void OnEnable()
        {
            EventService.Instance.OnPlayerOpenDoorEvent.AddListener(OnDoorOpen);
        }

        private void OnDisable()
        {
            EventService.Instance.OnPlayerOpenDoorEvent.RemoveListener(OnDoorOpen);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.GetComponent<PlayerView>() != null)
            {
                if (PlayerService.Instance.PlayerController.GetPickedUpItem().Count > 0)
                {
                    if (PlayerService.Instance.PlayerController.GetPickedUpItem().Find(item => item == Room1Key || item == Room2Key))
                    {
                        EventService.Instance.OnPlayerOpenDoorEvent.InvokeEvent();
                    }
                }
            }
        }

        private void OnDoorOpen()
        {
            DoorCollider.enabled = false;
        }
    }
}