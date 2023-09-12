using Events;
using UnityEngine;

namespace InteractionSystems
{
    [RequireComponent(typeof(BoxCollider2D))]
    public class ItemView : MonoBehaviour, IInteractable
    {
        [SerializeField] private ItemScriptableObject Notes;

        public void Interact()
        {
            switch (Notes.Type)
            {
                case ItemType.PickUp:

                    EventService.Instance.OnKeyPickedUpEvent.InvokeEvent(this.gameObject);
                    gameObject.SetActive(false);
                    break;

                case ItemType.Examine:

                    EventService.Instance.OnNoteExamineEvent.InvokeEvent(Notes);
                    break;

                default:

                    break;
            }
        }
    }
}