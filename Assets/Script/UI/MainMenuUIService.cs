using Service;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuUIService : MonoBehaviour
{
    [Header("Main Menu Buttons")]
    [SerializeField] private Button playButton;
    [SerializeField] private Button creditButton;
    [SerializeField] private Button quitButton;

    [Header("Story Gameobject")]
    [SerializeField] GameObject storyGameobject;

    [SerializeField] private int creditSceneIndex;

    private void Awake()
    {
        storyGameobject.SetActive(false);

        playButton.onClick.AddListener(Play);
        creditButton.onClick.AddListener(LoadCreditScene);
        quitButton.onClick.AddListener(Quit);
    }

    private void Update()
    {
        HandleLoadGameSceneInput();
    }

    private void HandleLoadGameSceneInput()
    {
        if (Input.GetKeyDown(KeyCode.Return))
        {
            if (storyGameobject.activeInHierarchy)
            {
                LoadGameScene();
            }
        }
    }

    private void Play()
    {
        GameService.Instance.GetSoundView().PlaySoundEffects(Sound.SoundType.ButtonClick, false);
        storyGameobject.SetActive(true);
    }

    private void LoadGameScene()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadCreditScene()
    {
        GameService.Instance.GetSoundView().PlaySoundEffects(Sound.SoundType.ButtonClick, false);
        SceneManager.LoadScene(creditSceneIndex);
    }

    private void Quit()
    {
        GameService.Instance.GetSoundView().PlaySoundEffects(Sound.SoundType.ButtonClick, false);
        Application.Quit();
    }
}