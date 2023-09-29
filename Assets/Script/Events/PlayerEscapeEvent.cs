using InteractionSystems;
using Player;
using Service;
using UnityEngine;

namespace Events
{
    public class PlayerEscapeEvent : MonoBehaviour, IAreaInteractable
    {
        [SerializeField] private GameObject EscapeKey;

        public void Interact()
        {
            throw new System.NotImplementedException();
        }

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
                    else
                    {
                        GameService.Instance.GetSoundView().PlaySoundEffects(Sound.SoundType.LockedDoor, false);
                    }
                }
                else
                {
                    GameService.Instance.GetSoundView().PlaySoundEffects(Sound.SoundType.LockedDoor, false);
                }
            }
        }
    }

}