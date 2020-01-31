using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using System.Collections;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        enum DestinationIdentifier
        {
            A, B, C, D, E
        }

        [SerializeField] int sceneToLoad;
        [SerializeField] Transform spawnPoint;
        [SerializeField] DestinationIdentifier destination;

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition()
        {
            DontDestroyOnLoad(gameObject);
            GameObject.FindObjectOfType<Canvas>().GetComponent<CanvasGroup>().alpha = 1;
            yield return SceneManager.LoadSceneAsync(sceneToLoad);
            Portal targetPortal = GetOtherPortal();
            UpdatePlayer(targetPortal);

            Destroy(gameObject);
        }

        private Portal GetOtherPortal()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                DestinationIdentifier otherPortal = portal.destination;
                if (portal == this || portal.destination != this.destination)
                {
                    continue;
                }
                
                return portal;
            }

            // if no portal found
            print("Could not find Portal.");
            return null;
        }

        private void UpdatePlayer(Portal targetPortal)
        {
            GameObject player = GameObject.FindWithTag("Player");
            player.GetComponent<NavMeshAgent>().Warp(targetPortal.spawnPoint.position);
            // player.transform.position = targetPortal.spawnPoint.position;
            // player.transform.rotation = targetPortal.spawnPoint.rotation;
            // player.GetComponent<NavMeshAgent>().enabled = true;
        }
    }
}