using Player;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "PlayerScriptableObject", menuName = "ScriptableObject/CreateNewPlayerScriptableObject")]
    public class PlayerScriptableObject : ScriptableObject
    {
        public float Speed = 4f;
        public PlayerView PlayerPrefab;

        [Header("Item Detection Parameters")]
        public LayerMask ItemDetectionLayer;
    }
}