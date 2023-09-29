﻿using Enemy;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "GhostScriptableObject", menuName = "ScriptableObject/CreateNewGhostScriptableObject")]
    public class GhostScriptableObject : ScriptableObject
    {
        public float Speed;
        public float StartPatrolWaitTime;
        public BoxCollider2D GhostPatrolArea;
        public LayerMask PlayerLayerMask;

        public GhostView GhostPrefab; 
    }
}