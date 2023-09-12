using Events;
using InteractionSystems;
using UI;
using UnityEngine;

namespace Player
{
    public class PlayerController
    {
        public PlayerView PlayerView { get; private set; }
        public PlayerModel PlayerModel { get; private set; }

        private Rigidbody2D playerRigidbody;
        private Camera camera;
        private Vector2 movement;
        private Vector2 mousePos;

        private const float itemDetectionRadius = 0.3f;

        public PlayerController(PlayerView playerPrefab, PlayerModel playerModel)
        {
            PlayerView = GameObject.Instantiate<PlayerView>(playerPrefab);
            PlayerModel = playerModel;

            playerRigidbody = PlayerView.GetPlayerRigidbody();

            PlayerModel.SetPlayerController(this);
            PlayerView.SetPlayerController(this);

            SubscribeEvents();
        }

        ~PlayerController()
        {
            UnsubscribeEvents();
        }

        private void SubscribeEvents()
        {
            EventService.Instance.OnKeyPickedUpEvent.AddListener(PickUpItem);
            EventService.Instance.OnNoteExamineEvent.AddListener(ExamineItem);
        }

        private void UnsubscribeEvents()
        {
            EventService.Instance.OnKeyPickedUpEvent.RemoveListener(PickUpItem);
            EventService.Instance.OnNoteExamineEvent.RemoveListener(ExamineItem);
        }

        public void SetPlayerLookingDirection(Camera cam)
        {
            camera = cam;
        }

        public void HandlePlayerMovementInput()
        {
            movement.x = Input.GetAxisRaw("Horizontal");
            movement.y = Input.GetAxisRaw("Vertical");

            mousePos = camera.ScreenToWorldPoint(Input.mousePosition);
        }

        public void HandlePlayerMovement()
        {
            playerRigidbody.MovePosition(playerRigidbody.position + movement * PlayerModel.Speed * Time.fixedDeltaTime);

            Vector2 lookDir = mousePos - playerRigidbody.position;
            float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
            playerRigidbody.rotation = angle;
        }

        public Vector2 GetPlayerMovement()
        {
            return movement;
        }

        public void HandlePlayerInteractInput()
        {
            if (DetectObject())
            {
                if (InteractInput())
                {
                    PlayerView.detectedObject.GetComponent<IInteractable>().Interact();
                }
            }
        }

        private bool InteractInput()
        {
            return Input.GetKeyDown(KeyCode.E);
        }

        private bool DetectObject()
        {
            Collider2D collider = Physics2D.OverlapCircle(PlayerView.ItemDetectionPoint.position, itemDetectionRadius, PlayerModel.ItemDetectionLayer);

            if (collider != null)
            {
                PlayerView.detectedObject = collider.gameObject;
                return true;
            }
            else
            {
                PlayerView.detectedObject = null;
                return false;
            }
        }

        public void PickUpItem(GameObject item)
        {
            PlayerView.pickedItems.Add(item);
            UIService.Instance.countText.text = "" + PlayerView.pickedItems.Count;
        }

        public void ExamineItem(ItemScriptableObject itemScriptableObject)
        {
            if (PlayerView.isExamaning)
            {
                Time.timeScale = 1f;
                UIService.Instance.examineWindow.SetActive(false);
                PlayerView.isExamaning = false;
            }
            else
            {
                UIService.Instance.examineImage.sprite = itemScriptableObject.NoteSprite;
                UIService.Instance.examineText.text = itemScriptableObject.DescriptionText;
                UIService.Instance.examineWindow.SetActive(true);
                Time.timeScale = 0f;
                PlayerView.isExamaning = true;
            }
        }
    }
}