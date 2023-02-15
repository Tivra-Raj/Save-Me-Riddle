using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Game_Over_Manager : MonoBehaviour
{
    [SerializeField] GameObject gameOverScreen;

    [Header("GameOver Menu Buttons")]
    [SerializeField] Button restart;
    [SerializeField] Button mainmenu;
    [SerializeField] Button quit;

    private void Awake()
    {
        gameOverScreen.SetActive(false);


        restart.onClick.AddListener(Restart);
        mainmenu.onClick.AddListener(MainMenu);
        quit.onClick.AddListener(Quit);

    }
    public void GameOver()
    {
        gameOverScreen.SetActive(true);
    }

    public void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    public void MainMenu()
    { 
        SceneManager.LoadScene(0);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
