using ScriptableObjects;
using UnityEngine;
using Utilities;

namespace Player
{
    public class PlayerService : MonoSingletonGeneric<PlayerService>
    {
        [SerializeField] private PlayerScriptableObject ConfigPlayer;
        [SerializeField] private Camera mainCamera;

        public PlayerController PlayerController { get; private set; }

        private void Start()
        {
            CreateNewPlayer();
        }

        private PlayerController CreateNewPlayer()
        {
            PlayerScriptableObject playerScriptableObject = ConfigPlayer;
            PlayerModel playerModel = new PlayerModel(playerScriptableObject);
            PlayerController = new PlayerController(playerScriptableObject.PlayerPrefab, playerModel);
            PlayerController.SetPlayerLookingDirection(mainCamera);

            return PlayerController;
        }
    }
}