﻿using UnityEngine;
using UnityEngine.Playables;

namespace RPG.Cinematics
{
    public class CinematicTrigger : MonoBehaviour
    {
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == "Player")
            {
                GetComponent<PlayableDirector>().Play();
                gameObject.GetComponent<BoxCollider>().isTrigger = false;
            }
        }
    }
}