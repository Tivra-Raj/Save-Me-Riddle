using Events;
using InstructionSystem;
using Player;
using Service;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace UI
{
    public class GameUIView : MonoBehaviour
    {
        [Header("Examine Parameters")]
        [SerializeField] private GameObject examineWindow;
        [SerializeField] private Image examineImage;
        [SerializeField] private TextMeshProUGUI examineText;

        [Header("Item Showcase")]
        [SerializeField] private TextMeshProUGUI KeyCountText;

        [Header("Game Over / Game Pause Parameters")]
        [SerializeField] private GameObject gameMenu;
        [SerializeField] private TextMeshProUGUI gameMenuText;

        [Header("Game Complete Parameters")]
        [SerializeField] private GameObject gameCompleteMenu;

        [Header("Game Menu Buttons")]
        [SerializeField] private Button restartButton;
        [SerializeField] private Button mainmenuButton;
        [SerializeField] private Button quitButton;

        [Header("Pause Menu Buttons")]
        [SerializeField] private Button resume;

        public static bool GameIsPaused = false;

        private void Start()
        {
            ConfigExamineWindow(null, null, false);
            SetGameMenuUIActive(false);
            gameCompleteMenu.SetActive(false);

            resume.onClick.AddListener(Resume);
            restartButton.onClick.AddListener(Restart);
            mainmenuButton.onClick.AddListener(MainMenu);
            quitButton.onClick.AddListener(Quit);
        }

        private void OnEnable()
        {
            EventService.Instance.OnPlayerEscapeEvent.AddListener(SetGameCompleteUIActive);
        }

        private void OnDisable()
        {
            EventService.Instance.OnPlayerEscapeEvent.RemoveListener(SetGameCompleteUIActive);
        }

        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                GameService.Instance.GetSoundView().PlaySoundEffects(Sound.SoundType.ButtonClick, false);
                if (GameIsPaused)
                {
                    Resume();
                }
                else
                {
                    Pause();
                }
            }
        }

        public void ConfigExamineWindow(Sprite itemSprite, string ItemDescription, bool examineWindowActiveStatus)
        {
            examineImage.sprite = itemSprite;
            examineText.text = ItemDescription;
            examineWindow.SetActive(examineWindowActiveStatus);
        }

        public void SetTotalKeyFoundedText(int keyFoundedCount)
        {
            KeyCountText.SetText("" + keyFoundedCount);
        }

        public void SetGameMenuUIActive(bool value)
        {
            gameMenu.SetActive(value);
        }

        public void SetGameCompleteUIActive()
        {
            PlayerService.Instance.PlayerController.playerDead = true;
            gameCompleteMenu.SetActive(true);
            Invoke(nameof(LoadCreditScene), 2.5f);
        }

        private void LoadCreditScene()
        {
            SceneManager.LoadScene(2);
        }

        private void Pause()
        {
            gameMenuText.SetText("Game Paused");
            SetGameMenuUIActive(true);
            restartButton.gameObject.SetActive(false);
            Time.timeScale = 0f;             //setting the speed of time passing to 0 to freeze the time
            GameIsPaused = true;
        }

        public void Resume()
        {
            gameMenuText.SetText("YOU GOT EATEN!");
            GameService.Instance.GetSoundView().PlaySoundEffects(Sound.SoundType.ButtonClick, false);
            SetGameMenuUIActive(false);
            restartButton.gameObject.SetActive(true);
            Time.timeScale = 1f;           //setting the speed of time passing to normal to free the time
            GameIsPaused = false;
        }

        public void Restart()
        {
            Time.timeScale = 1f;
            GameService.Instance.GetSoundView().PlaySoundEffects(Sound.SoundType.ButtonClick, false);
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }

        public void MainMenu()
        {
            Time.timeScale = 1f;
            GameService.Instance.GetSoundView().PlaySoundEffects(Sound.SoundType.ButtonClick, false);
            SceneManager.LoadScene(0);
        }

        public void Quit()
        {
            GameService.Instance.GetSoundView().PlaySoundEffects(Sound.SoundType.ButtonClick, false);
            Application.Quit();
        }
    }
}