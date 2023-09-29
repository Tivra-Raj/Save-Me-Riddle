using Enemy;
using Events;
using InteractionSystems;
using Service;
using System.Collections.Generic;
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

        private bool isExamaning = false;
        public bool playerDead = false;

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
            if(playerDead) return;

            playerRigidbody.MovePosition(playerRigidbody.position + movement * PlayerModel.Speed * Time.fixedDeltaTime);

            Vector2 lookDir = (mousePos - playerRigidbody.position).normalized;
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
                    PlayerView.DetectedObject.GetComponent<IItemInteractable>().Interact();
                }
            }

            DetectArea();
        }

        private bool InteractInput()
        {
            return Input.GetKeyDown(KeyCode.E);
        }

        private bool DetectObject()
        {
            Collider2D collider = Physics2D.OverlapCircle(PlayerView.ObjectDetectionPoint.position, PlayerView.ObjectDetectionRadius, PlayerModel.ItemDetectionLayer);            

            if (collider != null)
            {
                PlayerView.DetectedObject = collider.gameObject;
                return true;
            }
            else
            {
                PlayerView.DetectedObject = null;
                return false;
            }
        }

        private bool DetectArea()
        {
            Collider2D collider = Physics2D.OverlapCircle(PlayerView.ObjectDetectionPoint.position, PlayerView.ObjectDetectionRadius, PlayerModel.AreaDetectionLayer);

            if (collider != null)
            {
                PlayerView.DetectedArea = collider.gameObject;
                return true;
            }
            else
            {
                PlayerView.DetectedArea = null;
                return false;
            }
        }

        public List<GameObject> GetPickedUpItem()
        {
            return PlayerView.pickedItems;
        }

        public void PickUpItem(GameObject item)
        {
            GameService.Instance.GetSoundView().PlaySoundEffects(Sound.SoundType.KeyPickUp, false);
            PlayerView.pickedItems.Add(item);
            GameService.Instance.GetGameUIView().countText.text = "" + PlayerView.pickedItems.Count;
        }

        public void ExamineItem(ItemScriptableObject itemScriptableObject)
        {
            if (isExamaning)
            {
                GameService.Instance.GetSoundView().PlaySoundEffects(Sound.SoundType.NotePickUp, false);
                Time.timeScale = 1f;
                GameService.Instance.GetGameUIView().ConfigExamineWindow(null, null, false);
                isExamaning = false;
            }
            else
            {
                GameService.Instance.GetSoundView().PlaySoundEffects(Sound.SoundType.NotePickUp, false);
                GameService.Instance.GetInstructionView().HideInstruction();
                GameService.Instance.GetGameUIView().ConfigExamineWindow(itemScriptableObject.NoteSprite, itemScriptableObject.DescriptionText, true);
                Time.timeScale = 0f;
                isExamaning = true;
            }
        }

        public void OnPlayerCollidedWithGhost(GameObject collidedGameObject)
        {
            if(collidedGameObject.GetComponent<GhostView>() != null)
            {
                GameService.Instance.GetSoundView().PlaySoundEffects(Sound.SoundType.PlayerDeath, false);
                playerDead = true;
                playerRigidbody.bodyType = RigidbodyType2D.Static;
                GameService.Instance.GetGameUIView().SetGameMenuUIActive(true);
            }
        }
    }
}