using UnityEngine;

namespace InteractionSystems
{
    [CreateAssetMenu(fileName = "ItemScriptableObject", menuName = "ScriptableObject/CreateNewItemScriptableObject")]
    public class ItemScriptableObject : ScriptableObject
    {
        [Header("Item Description")]
        public ItemType Type;
        public string DescriptionText;
        public Sprite NoteSprite;
    }
}