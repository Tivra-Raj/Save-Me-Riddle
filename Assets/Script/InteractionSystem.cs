using System.Collections;
using System.Collections.Generic;
using System.Threading;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InteractionSystem : MonoBehaviour
{
    [Header("Item Detection Parameters")]
    public Transform detectionPoint;
    public LayerMask detectionLayer;
    public GameObject detectedObject;

    private const float detectionRadius = 0.3f;

    [Header("Examine Parameters")]
    public GameObject examineWindow;
    public Image examineImage;
    public TextMeshProUGUI examineText;
    public bool isExamaning;


    [Header("Item Showcase")]
    public TextMeshProUGUI countText;

    [Header("Picked Item List")]
    public List<GameObject> pickedItems = new List<GameObject>();



    private void Update()
    {
        if(DetectObject())
        {
            if(InteractInput())
            {
                detectedObject.GetComponent<Item>().Interact();
            }
        }
    }

    bool InteractInput()
    {
        return Input.GetKeyDown(KeyCode.E);
    }

    bool DetectObject()
    {
        Collider2D collider = Physics2D.OverlapCircle(detectionPoint.position, detectionRadius, detectionLayer);
        
        if(collider != null)
        {
            detectedObject = collider.gameObject;
            return true;
        }
        else
        {
            detectedObject = null;
            return false;
        }
    }

    public void PickUpItem(GameObject item)
    {
        pickedItems.Add(item);
        countText.text = "" + pickedItems.Count;
    }

    public void ExamineItem(Item item)
    {
        if(isExamaning)
        {
            Time.timeScale = 1f;
            examineWindow.SetActive(false);
            isExamaning = false;
        }
        else
        {
            examineImage.sprite = item.sprite;
            examineText.text = item.descriptionText;
            examineWindow.SetActive(true);
            Time.timeScale = 0f;
            isExamaning = true;
        }
    }
}
