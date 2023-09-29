using ScriptableObjects;
using UnityEngine;

namespace Player
{
    public class PlayerModel
    {
        public float Speed;

        public PlayerController PlayerController { get; private set; }
        public LayerMask ItemDetectionLayer { get; private set; }

        public void SetPlayerController(PlayerController playerController) => PlayerController = playerController;

        public PlayerModel(PlayerScriptableObject playerScriptableObject)
        {
            Speed = playerScriptableObject.Speed;
            ItemDetectionLayer = playerScriptableObject.ItemDetectionLayer;
        }
    }
}