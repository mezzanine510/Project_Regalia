using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace RPG.Core
{
    public class DestroyAfterEffect : MonoBehaviour
    {
        [SerializeField] GameObject targetToDestroy = null;
        ParticleSystem effect;

        private void Start()
        {
            effect = GetComponent<ParticleSystem>();
        }

        void Update()
        {
            if (effect.IsAlive()) return;
            
            if (targetToDestroy) Destroy(targetToDestroy);
            else Destroy(gameObject);
        }
    }
}
