using Events;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Utilities;

namespace UI
{
    public class GameUIService : MonoSingletonGeneric<GameUIService>
    {
        [Header("Examine Parameters")]
        [SerializeField] private GameObject examineWindow;
        [SerializeField] private Image examineImage;
        [SerializeField] private TextMeshProUGUI examineText;

        [Header("Item Showcase")]
        public TextMeshProUGUI countText;

        [Header("Game Over Parameters")]
        [SerializeField] private GameObject gameOverMenu;

        [Header("Game Complete Parameters")]
        [SerializeField] private GameObject gameCompleteMenu;

        [Header("Game Menu Buttons")]
        [SerializeField] private Button restartButton;
        [SerializeField] private Button mainmenuButton;
        [SerializeField] private Button quitButton;

        private void Start()
        {
            ConfigExamineWindow(null, null, false);
            SetGameOverUIActive(false);
            gameCompleteMenu.SetActive(false);

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

        public void ConfigExamineWindow(Sprite itemSprite, string ItemDescription, bool examineWindowActiveStatus)
        {
            examineImage.sprite = itemSprite;
            examineText.text = ItemDescription;
            examineWindow.SetActive(examineWindowActiveStatus);
        }

        public void SetGameOverUIActive(bool value)
        {
            gameOverMenu.SetActive(value);
        }

        public void SetGameCompleteUIActive()
        {
            gameCompleteMenu.SetActive(true);
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
}