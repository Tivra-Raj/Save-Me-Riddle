using Player;
using UnityEngine;

namespace Events
{
    public class PlayerEscapeEvent : MonoBehaviour
    {
        [SerializeField] private GameObject EscapeKey;
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.GetComponent<PlayerView>() != null)
            {
                if (PlayerService.Instance.PlayerController.GetPickedUpItem().Count > 0)
                {
                    if(PlayerService.Instance.PlayerController.GetPickedUpItem().Find(item => item == EscapeKey))
                    {
                        EventService.Instance.OnPlayerEscapeEvent.InvokeEvent();
                    }
                }
            }
        }
    }

}