using Events;
using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;

namespace Player
{
    public class TorchLightView : MonoBehaviour
    {
        [SerializeField] private Light2D playerLight;
        [SerializeField] private Light2D torchLight;
        [SerializeField] private float flickerDuration = 2f;
        [SerializeField] private float minIntensity = 0.5f;
        [SerializeField] private float maxIntensity = 1f;
        [SerializeField] private float setTorchMaxIntensity = 1f;
        [SerializeField] private float setPlayerLightIntensity = 0.2f;

        private bool isFlickering = false;
        private float flickerTimer;
        private float torchOriginalIntensity;
        private float playerLightOriginalIntensity = 1f;

        private TorchState currentState;
        private Coroutine startTimer;

        private void OnEnable()
        {
            EventService.Instance.OnGhostSpawnedEvent.AddListener(OnTorchLightToggled);
            EventService.Instance.OnGhostDeSpawnedEvent.AddListener(OnTorchLightToggled);
        }

        private void OnDisable()
        {
            EventService.Instance.OnGhostSpawnedEvent.RemoveListener(OnTorchLightToggled);
            EventService.Instance.OnGhostDeSpawnedEvent.RemoveListener(OnTorchLightToggled);
        }

        private void Start()
        {
            currentState = TorchState.On;
            flickerTimer = flickerDuration;
            torchOriginalIntensity = setTorchMaxIntensity;
        }

        private void Update()
        {
            if (isFlickering)
            {
                flickerTimer -= Time.deltaTime;

                if (flickerTimer <= 0)
                    isFlickering = false;
                else
                    torchLight.intensity = Random.Range(minIntensity, maxIntensity);
            }
            else
                torchLight.intensity = torchOriginalIntensity;
        }

        private void FlickerTorchLight()
        {
            isFlickering = true;
            flickerTimer = flickerDuration;
        }

        private void ToggleTorchLight()
        {
            bool light = !isFlickering;

            switch (currentState)
            {
                case TorchState.On:
                    stopCoroutine(startTimer);
                    FlickerTorchLight();
                    startTimer = StartCoroutine(WaitForFlickerCompleteThenTurnOffTorch(light));
                    break;

                case TorchState.Off:
                    FlickerTorchLight();
                    currentState = TorchState.On;
                    playerLight.intensity = playerLightOriginalIntensity;
                    light = true;
                    break;
            }

            torchLight.enabled = light;
        }

        private void OnTorchLightToggled()
        {
            ToggleTorchLight();
        }

        private IEnumerator WaitForFlickerCompleteThenTurnOffTorch(bool light)
        {
            yield return new WaitForSeconds(flickerDuration);
            currentState = TorchState.Off;
            light = false;
            torchLight.enabled = light;
            playerLight.intensity = setPlayerLightIntensity;
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

    public enum TorchState
    {
        None,
        On,
        Off,
    }
}