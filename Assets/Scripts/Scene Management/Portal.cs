using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using System.Collections;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] int sceneToLoad;
        [SerializeField] Transform spawnPoint;

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
            yield return SceneManager.LoadSceneAsync(sceneToLoad);
            print("THIS: " + this);
            print("gameObject: " + gameObject);
            
            Portal targetPortal = GetOtherPortal();
            print("targetPortal: " + targetPortal);
            print("targetPortal.spawnPoint: " + targetPortal.spawnPoint);
            UpdatePlayer(targetPortal);

            Destroy(gameObject);
        }

        private Portal GetOtherPortal()
        {
            foreach (Portal portal in FindObjectsOfType<Portal>())
            {
                if (portal == this)
                {
                    continue;
                };
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