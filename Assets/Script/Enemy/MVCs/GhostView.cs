using Player;
using UnityEngine;

namespace Enemy
{
    public class GhostView : MonoBehaviour
    {
        public GhostController GhostController { get; private set; }

        public void SetGhostController(GhostController ghostController) => GhostController = ghostController;

        private void Start()
        {
            GhostController.CreateNextPosition();
        }

        private void Update()
        {
            if (PlayerService.Instance.PlayerController.GetPlayerMovement().sqrMagnitude > 0)
            {
                GhostController.playerMoved = true;
            }

            GhostController.Attack();
        }

        private void FixedUpdate()
        {
            GhostController.Patrolling();
        }
    }
}