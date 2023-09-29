using Player;
using Service;
using Sound;
using UnityEngine;

namespace InteractionSystems
{
    public class JumpScareView : MonoBehaviour
    {
        [SerializeField] private int keysRequiredToTrigger;
        [SerializeField] private SoundType soundToPlay;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.GetComponent<PlayerView>() != null && PlayerService.Instance.PlayerController.GetPickedUpItem().Count == keysRequiredToTrigger)
            {
                GameService.Instance.GetSoundView().PlaySoundEffects(soundToPlay);
                GetComponent<BoxCollider2D>().enabled = false;
            }
        }
    }
}