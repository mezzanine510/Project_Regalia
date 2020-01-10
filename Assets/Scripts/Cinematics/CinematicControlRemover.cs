using UnityEngine;
using UnityEngine.Playables;
using RPG.Control;
// using RPG.Movement;
using RPG.Core;

namespace RPG.Cinematics
{
    public class CinematicControlRemover : MonoBehaviour
    {
        GameObject player;
        PlayerController playerController;
        PlayableDirector playableDirector;

        private void Awake()
        {
            player = GameObject.FindWithTag("Player");
            playerController = player.GetComponent<PlayerController>();
            playableDirector = GetComponent<PlayableDirector>();
            playableDirector.played += DisableControl;
            playableDirector.stopped += EnableControl;
        }

        // pb is just a placeholder, since the Delegate event needs to take a PlayableDirector argument
        public void DisableControl(PlayableDirector pb)
        {
            player.GetComponent<ActionScheduler>().CancelCurrentAction();
            playerController.enabled = false;
        }

        public void EnableControl(PlayableDirector pb)
        {
            playerController.enabled = true;
        }
    }
}