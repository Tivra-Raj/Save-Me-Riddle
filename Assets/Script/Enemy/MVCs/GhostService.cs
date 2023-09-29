using ScriptableObjects;
using TMPro;
using UnityEngine;
using Utilities;

namespace Enemy
{
    public class GhostService : MonoSingletonGeneric<GhostService>
    {
        [SerializeField] private GhostScriptableObject ConfigGhost;

        [SerializeField] public BoxCollider2D GhostPatrolArea;
        [SerializeField] TextMeshProUGUI timerDisplay;
        [SerializeField] float ghostArrivalTime = 20;
        [SerializeField] float ghostDepartureTime = 10;

        float totalTime;
        float currenttime;

        bool isGhostSpwaned;

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
                isGhostSpwaned = true;
                GhostController.GhostView.gameObject.SetActive(true);
            }
            if (currenttime <= 0)
            {
                isGhostSpwaned = false;
                GhostController.GhostView.gameObject.SetActive(false);
                currenttime = totalTime;
            }
        }
    }
}