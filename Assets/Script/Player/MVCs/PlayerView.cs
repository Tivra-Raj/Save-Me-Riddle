using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerView : MonoBehaviour
    {
        private Rigidbody2D playerRigidBody;
        public GameObject detectedObject;
        public Transform ItemDetectionPoint;

        
        public bool isExamaning = false;

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

        public void stopCoroutine(Coroutine coroutine)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
                coroutine = null;
            }
        }


    }
}