using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUIService : MonoBehaviour
{
    [Header("Level Complete Buttons")]
    [SerializeField] Button play;
    [SerializeField] Button quit;

    private void Awake()
    {
        play.onClick.AddListener(Play);
        quit.onClick.AddListener(Quit);
    }

    public void Play()
    {
        SceneManager.LoadScene(1);
    }

    public void Quit()
    {
        Application.Quit();
    }
}
