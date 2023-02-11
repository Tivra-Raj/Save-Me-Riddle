using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Level_Complete_Manager : MonoBehaviour
{
    [Header("Level Complete Buttons")]
    [SerializeField] Button restart;
    [SerializeField] Button mainmenu;

    private void Awake()
    {
        restart.onClick.AddListener(MainMenu);
        mainmenu.onClick.AddListener(Quit);
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
