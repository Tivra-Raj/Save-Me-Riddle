using ScriptableObjects;
using UnityEngine;

namespace Enemy
{
    public class GhostModel
    {
        public float Speed;
        public float StartPatrolWaitTime;
        public LayerMask PlayerLayerMask;

        public GhostController GhostController { get; private set; }

        public void SetGhostController(GhostController ghostController) => GhostController = ghostController;
        
        public GhostModel(GhostScriptableObject ghostScriptableObject)
        {
            Speed = ghostScriptableObject.Speed;
            StartPatrolWaitTime = ghostScriptableObject.StartPatrolWaitTime;
            PlayerLayerMask = ghostScriptableObject.PlayerLayerMask;
        }
    }
}