using ScriptableObjects;
using UnityEngine;

namespace Enemy
{
    public class GhostModel
    {
        public float Speed;
        public float StartPatrolWaitTime;
        public BoxCollider2D GhostPatrolArea;
        public LayerMask PlayerLayerMask;

        public GhostController GhostController { get; private set; }

        public void SetGhostController(GhostController ghostController) => GhostController = ghostController;
        
        public GhostModel(GhostScriptableObject ghostScriptableObject)
        {
            Speed = ghostScriptableObject.Speed;
            StartPatrolWaitTime = ghostScriptableObject.StartPatrolWaitTime;
            GhostPatrolArea = ghostScriptableObject.GhostPatrolArea;
            PlayerLayerMask = ghostScriptableObject.PlayerLayerMask;
        }
    }
}