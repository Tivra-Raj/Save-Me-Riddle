using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    [SerializeField] InteractionSystem interactionSystem;
    [SerializeField] GameObject Level_Complete_Menu;

    private void Start()
    {
        Level_Complete_Menu.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<Player_Controller>() != null)
        {
            if (interactionSystem.pickedItems.Count > 0)
            {
                Level_Complete_Menu.SetActive(true);
                Debug.Log("level complete");
            }
        }
    }
}
