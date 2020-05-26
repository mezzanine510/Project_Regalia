using UnityEngine;
using UnityEngine.Playables;
using RPG.Control;
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
        public void DisableControl(PlayableDirector pd)
        {
            player.GetComponent<ActionScheduler>().CancelCurrentAction();
            playerController.enabled = false;
        }

        public void EnableControl(PlayableDirector pd)
        {
            playerController.enabled = true;
        }
    }
}