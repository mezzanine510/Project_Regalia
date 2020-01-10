using UnityEngine;
using UnityEngine.Playables;
using RPG.Control;
using RPG.Movement;

namespace RPG.Cinematics
{
    public class CinematicControlRemover : MonoBehaviour
    {
        GameObject player;
        PlayerController playerController;
        Mover mover;
        PlayableDirector playableDirector;

        private void Awake()
        {
            player = GameObject.FindGameObjectWithTag("Player");
            playerController = player.GetComponent<PlayerController>();
            mover = player.GetComponent<Mover>();
            playableDirector = GetComponent<PlayableDirector>();
            playableDirector.played += DisableControl;
            playableDirector.stopped += EnableControl;
        }

        public void DisableControl(PlayableDirector playableDirector)
        {
            mover.Cancel();
            playerController.enabled = false;
            // mover.enabled = false;
        }

        public void EnableControl(PlayableDirector playableDirector)
        {
            playerController.enabled = true;
        }
    }
}