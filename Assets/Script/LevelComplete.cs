using Player;
using UnityEngine;

public class LevelComplete : MonoBehaviour
{
    [SerializeField] GameObject Level_Complete_Menu;

    private void Start()
    {
        Level_Complete_Menu.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<PlayerView>() != null)
        {
            if (PlayerService.Instance.PlayerController.PlayerView.pickedItems.Count > 0)
            {
                Level_Complete_Menu.SetActive(true);
                Debug.Log("level complete");
            }
        }
    }
}
