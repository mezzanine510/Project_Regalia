using System;
using UnityEngine;

namespace RPG.Cinematics
{
    public class FakePlayableDirector : MonoBehaviour
    {
        public event Action<float> onFinish;
        
        private void Awake()
        {
            Invoke("OnFinish", 1f);
        }

        public void OnFinish()
        {
            onFinish(4f);
        }
    }
}