using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using System.Collections;
using RPG.Saving;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        [SerializeField] int sceneToLoad;
        [SerializeField] Transform spawnPoint;
        [SerializeField] DestinationIdentifier destination;
        [SerializeField] float fadeOutTime = 0.5f;
        [SerializeField] float fadeInTime = 0.5f;
        [SerializeField] float fadeWaitTime = 0.5f;

        enum DestinationIdentifier
        {
            A, B, C, D, E
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.tag == "Player")
            {
                StartCoroutine(Transition());
            }
        }

        private IEnumerator Transition()
        {
            if(sceneToLoad < 0)
            {
                Debug.LogError("Scene to load is not set, use the Editor to set it.");
            }

            DontDestroyOnLoad(gameObject);

            Fader fader = FindObjectOfType<Fader>();

            yield return StartCoroutine(fader.FadeOut(fadeOutTime));

            SavingWrapper savingWrapper = FindObjectOfType<SavingWrapper>();
            savingWrapper.Save();

            yield return SceneManager.LoadSceneAsync(sceneToLoad);
            
            savingWrapper.Load();

            Portal targetPortal = GetOtherPortal();
            UpdatePlayer(targetPortal);
            yield return new WaitForSeconds(fadeWaitTime); // wait to make sure everything loads

            yield return StartCoroutine(fader.FadeIn(fadeInTime));

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