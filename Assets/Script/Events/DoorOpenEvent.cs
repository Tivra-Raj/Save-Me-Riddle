using InteractionSystems;
using Player;
using Service;
using UnityEngine;

namespace Events
{
    public class DoorOpenEvent : MonoBehaviour, IAreaInteractable
    {
        [SerializeField] private GameObject StudyRoomKey;
        [SerializeField] private GameObject BedRoomKey;
        [SerializeField] private BoxCollider2D DoorCollider;
        [SerializeField] private BoxCollider2D AreaInstructionCollider;

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
                    if (PlayerService.Instance.PlayerController.GetPickedUpItem().Find(item => item == StudyRoomKey || item == BedRoomKey))
                    {
                        EventService.Instance.OnPlayerOpenDoorEvent.InvokeEvent();
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

        private void OnDoorOpen()
        {
            DoorCollider.enabled = false;
            AreaInstructionCollider.enabled = false;
        }

        public void Interact()
        {
            throw new System.NotImplementedException();
        }
    }
}