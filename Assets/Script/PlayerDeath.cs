using Player;
using UnityEngine;

public class PlayerDeath : MonoBehaviour
{
    [SerializeField] GameObject gameOverUI;

    private void Awake()
    {
        gameOverUI.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.GetComponent<PlayerView>() != null)
        {
            gameOverUI.SetActive(true);
        }
    }
}
