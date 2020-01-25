using UnityEngine;
using UnityEngine.SceneManagement;

namespace RPG.SceneManagement
{
    public class Portal : MonoBehaviour
    {
        Collider playerCollider;

        private void Awake()
        {
            playerCollider = GameObject.FindGameObjectWithTag("Player").GetComponent<Collider>();
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other == playerCollider)
            {
                SceneManager.LoadSceneAsync(1);
            }
        }
    }
}