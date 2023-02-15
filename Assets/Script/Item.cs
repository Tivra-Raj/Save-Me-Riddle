using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Item : MonoBehaviour
{
    public InteractionSystem interactionSystem;
    public enum ItemType
    {
        None,
        PickUp,
        Examine
    }
    public ItemType type;

    [Header("Item Description")]
    public string descriptionText;
    public Sprite sprite;

    private void Reset()
    {
        GetComponent<Collider2D>().isTrigger = true;
        gameObject.layer = 7;
    }

    public void Interact()
    {
        switch(type)
        {
            case ItemType.PickUp:
                
                interactionSystem.PickUpItem(this.gameObject);
                gameObject.SetActive(false);
                Debug.Log("pick Up");
                break;

            case ItemType.Examine:
                interactionSystem.ExamineItem(this);
                Debug.Log("Examine");
                break;

            default:
                
                Debug.Log("None");
                break;
        }
    }
}
