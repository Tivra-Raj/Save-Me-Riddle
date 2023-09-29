using InteractionSystems;
using Service;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerView : MonoBehaviour
    {
        private Rigidbody2D playerRigidBody;

        public Transform ObjectDetectionPoint;
        public float ObjectDetectionRadius = 0.3f;
        public GameObject DetectedObject;
        public GameObject DetectedArea;

        [Header("Picked Item List")]
        public List<GameObject> pickedItems = new List<GameObject>();

        public PlayerController PlayerController { get; private set; } 

        private void Awake() => playerRigidBody = GetComponent<Rigidbody2D>();

        public void SetPlayerController(PlayerController playerController) => PlayerController = playerController;

        public Rigidbody2D GetPlayerRigidbody() => playerRigidBody;

        void Update()
        {
            PlayerController.HandlePlayerMovementInput();
            PlayerController.HandlePlayerInteractInput();
        }

        private void FixedUpdate()
        {
            PlayerController.HandlePlayerMovement();
        }

        private void OnCollisionEnter2D(Collision2D collision) => PlayerController?.OnPlayerCollidedWithGhost(collision.gameObject);

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.GetComponent<IItemInteractable>() != null)
            {
                GameService.Instance.GetInstructionView().ShowInstruction(InstructionSystem.InstructionType.InteractableItem, DetectedObject);
            }

            if(collision.GetComponent<IAreaInteractable>() != null)
            {
                GameService.Instance.GetInstructionView().ShowInstruction(InstructionSystem.InstructionType.InteractableArea, DetectedArea);
            }
        }

        private void OnTriggerExit2D(Collider2D collision)
        {
            if (collision.GetComponent<IItemInteractable>() != null)
            {
                GameService.Instance.GetInstructionView().HideInstruction();
            }

            if(collision.GetComponent <IAreaInteractable>() != null)
            {
                GameService.Instance.GetInstructionView().HideInstruction();
            }
        }

        private void OnDrawGizmos()
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(this.transform.position, ObjectDetectionRadius);
        }
    }
}