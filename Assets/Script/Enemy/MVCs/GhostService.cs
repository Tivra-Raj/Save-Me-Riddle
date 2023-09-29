using Player;
using ScriptableObjects;
using Service;
using System.Collections;
using TMPro;
using UnityEngine;
using Utilities;

namespace Enemy
{
    public class GhostService : MonoSingletonGeneric<GhostService>
    {
        [SerializeField] private GhostScriptableObject ConfigGhost;

        [SerializeField] public BoxCollider2D GhostPatrolArea;
        [SerializeField] private TextMeshProUGUI timerDisplay;
        [SerializeField] private float ghostArrivalTime = 20;
        [SerializeField] private float ghostDepartureTime = 10;

        private float totalTime;
        private float currenttime;
        private bool isGhostSpwaned;
        private Coroutine countDown;

        public GhostController GhostController { get; private set; }

        private void Start()
        {
            CreateNewGhost();
            totalTime = ghostArrivalTime + ghostDepartureTime;
            currenttime = totalTime;    
        }

        private void Update()
        {
            GhostVisibilty();
        }

        private GhostController CreateNewGhost()
        {
            GhostScriptableObject ghostScriptableObject = ConfigGhost;
            GhostModel ghostModel = new GhostModel(ghostScriptableObject);
            GhostController = new GhostController(ghostScriptableObject.GhostPrefab, ghostModel);
            GhostController.GhostView.gameObject.SetActive(false);

            return GhostController;
        }

        private void GhostVisibilty()
        {
            if(PlayerService.Instance.PlayerController.playerDead) { return; }

            currenttime -= 1 * Time.deltaTime;

            if (currenttime < totalTime && currenttime > ghostDepartureTime)
            {
                timerDisplay.text = "Ghost Arrival : " + (currenttime - ghostDepartureTime).ToString("0");
            }
            else if (currenttime < ghostDepartureTime)
            {
                timerDisplay.text = "Ghost Departure : " + (currenttime).ToString("0");
            }

            if (currenttime <= (totalTime - ghostArrivalTime) && !isGhostSpwaned)
            {
                stopCoroutine(countDown);
                isGhostSpwaned = true;
                GameService.Instance.GetSoundView().PlayBackgroundMusic(Sound.SoundType.GhostPresenceMusic, true);
                countDown = StartCoroutine(SpwanGhost());
            }
            if (currenttime <= 0)
            {
                stopCoroutine(countDown);
                isGhostSpwaned = false;
                GameService.Instance.GetSoundView().PlaySoundEffects(Sound.SoundType.GhostOutgoing, false);
                countDown = StartCoroutine(DeSpwanGhost());
                currenttime = totalTime;
            }
        }

        private IEnumerator SpwanGhost()
        {
            yield return new WaitForSeconds(2);
            GhostController.GhostView.gameObject.SetActive(true);
        }

        private IEnumerator DeSpwanGhost()
        {
            GhostController.GhostView.gameObject.SetActive(false);
            yield return new WaitForSeconds(2);
            GameService.Instance.GetSoundView().PlayBackgroundMusic(Sound.SoundType.BackgroundMusic, true);
        }

        private void stopCoroutine(Coroutine coroutine)
        {
            if (coroutine != null)
            {
                StopCoroutine(coroutine);
                coroutine = null;
            }
        }
    }
}